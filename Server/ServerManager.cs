using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour.Server;

public class ServerManager : Manager
{
    public Action OnAuthStarted;
    public Action<string?> OnAuthCompleted;
    
    private HttpClient? _client = null;
    private ServerConfig? _serverConfig;
    
    public string? Username { get; private set; }
    public string? AuthError { get; private set; }

    public override void Configure(MewTour main, ModConfig config)
    {
        config.GetString(ConfigVariables.SERVER, string.Empty);
        config.GetString(ConfigVariables.KEY, string.Empty);
        
        ActivateClient(config);
    }
    
    public void ActivateClient(ModConfig config, bool reset = false)
    {
        if (_client != null &&
            !reset)
            return;

        Username = null;
        AuthError = null;
        _client = null;
        
        OnAuthStarted?.Invoke();
        
        var server = config.GetString(ConfigVariables.SERVER, string.Empty);
        var key = config.GetString(ConfigVariables.KEY, string.Empty);

        if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(key))
        {
            AuthError = "Не заданы параметры.";
            OnAuthCompleted?.Invoke(AuthError);
            return;
        }
        
        byte[] bytes = Convert.FromBase64String(server);
        using var stream = new MemoryStream(bytes);

        _serverConfig = JsonSerializer.Deserialize(
            stream,
            SourceGenerationContext.Default.ServerConfig);

        if (_serverConfig == null ||
            string.IsNullOrEmpty(_serverConfig.Address))
        {
            AuthError = "Передан некорректный ключ игрока.";
            OnAuthCompleted?.Invoke(AuthError);
            return;
        }
        
        _client = new HttpClient
        {
            BaseAddress = new Uri(_serverConfig.Address)
        };
        
        Task.Run(() => TryAuth(key));
    }

    private async Task TryAuth(string password)
    {
        if (_serverConfig == null ||
            _client == null)
        {
            OnAuthCompleted?.Invoke("Не найден конфиг сервера.");
            return;
        }
        
        var payload = new
        {
            username = _serverConfig.Username,
            password = password
        };
        
        var content = new StringContent($"{{\"username\":\"{payload.username}\", " +
                                        $"\"password\":\"{payload.password}\"}}", Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await SendServerRequest(HttpMethod.Post, "api/auth/login", content);

        if (response.IsSuccessStatusCode)
        {
            Username = payload.username;
            AuthError = null;

            try
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize(
                    responseBody,
                    SourceGenerationContext.Default.AuthModel);

                if (result != null)
                {
                    _client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", result.Token);

                    MewTourLogger.Log($"Authorized successfully, token: {result.Token}, username: {result.Username}");

                    OnAuthCompleted?.Invoke(null);
                }
                else
                {
                    AuthError = "Не удалось десериализовать данные.";
                    OnAuthCompleted?.Invoke(AuthError);
                }
            }
            catch (Exception ex)
            {
                MewTourLogger.Log($"Auth error: {ex.Message}");
                
                AuthError = ex.Message;
                OnAuthCompleted?.Invoke(AuthError);
            }
        }
        else
        {
            string errorBody = await response.Content.ReadAsStringAsync();
            var errorResult = JsonSerializer.Deserialize(
                errorBody,
                SourceGenerationContext.Default.ErrorModel);

            Username = null;
            AuthError = errorResult?.Message;
            
            OnAuthCompleted?.Invoke(AuthError);
        }
    }

    private string CreateCatStateCall(bool isLiveAndValid, string catName, long catId, string className,
        string spell0, string spell1, string spell2, string spell3,
        string passive0, string passive1, string disorder0, string disorder1)
    {
        string? ToJsonValue(string? value) => string.IsNullOrEmpty(value) ? null : $"\"{value}\"";

        string call = $"{{\"isLiveAndValid\":{isLiveAndValid.ToString().ToLower()},\"catName\":\"{catName}\",\"catId\":{catId},\"className\":\"{className}\"," +
                      $"\"abilities\":[{{\"id\":1,\"name\":{ToJsonValue(spell0)}}},{{\"id\":2,\"name\":{ToJsonValue(spell1)}}}," +
                      $"{{\"id\":3,\"name\":{ToJsonValue(spell2)}}},{{\"id\":4,\"name\":{ToJsonValue(spell3)}}}]," +
                      $"\"passives\":[{{\"id\":1,\"name\":{ToJsonValue(passive0)}}},{{\"id\":2,\"name\":{ToJsonValue(passive1)}}}]," +
                      $"\"disorders\":[{{\"id\":1,\"name\":{ToJsonValue(disorder0)}}},{{\"id\":2,\"name\":{ToJsonValue(disorder1)}}}]}}";

        return call;
    }

    public string CreateCatState(GameChar cat)
    {
        return CreateCatStateCall(cat.IsLiveAndValid, cat.Name, cat.CatId, cat.ClassName, cat.Spell0, cat.Spell1, cat.Spell2, cat.Spell3,
            cat.Passive0, cat.Passive1, cat.Disorder0, cat.Disorder1);
    }

    public void UpdateCat(string json)
    {
        if (_client == null)
            return;
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        _ = SendServerRequest(HttpMethod.Post, "api/cat/update", content);
    }

    public void EndRun()
    {
        if (_client == null)
            return;
        
        var content = new StringContent("", Encoding.UTF8, "application/json");
        _ = SendServerRequest(HttpMethod.Post, "api/run/end", content);
    }

    public void RollCat(GameChar cat)
    {
        if (_client == null)
            return;
        
        var content = new StringContent($"{{\"catName\":\"{cat.Name}\"," +
                                        $"\"catId\":\"{cat.CatId}\"," +
                                        $"\"className\":\"{cat.ClassName}\"}}", Encoding.UTF8, "application/json");
        
        _ = SendServerRequest(HttpMethod.Post, "api/cat/roll", content);
    }

    public async Task<List<string>> GetDraftResults()
    {
        var draftResults = new List<string>();

        if (_client == null)
            return draftResults;
        
        var content = new StringContent("", Encoding.UTF8, "application/json");
        var response = await SendServerRequest(HttpMethod.Get, "api/match/draft", content);

        if (response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize(
                responseBody,
                SourceGenerationContext.Default.ListString);

            draftResults = result ?? draftResults;
            return draftResults;
        }
        else
            return draftResults;
    }

    private async Task<HttpResponseMessage> SendServerRequest(HttpMethod method, string requestUri, StringContent content)
    {
        if (_client == null)
            return new HttpResponseMessage();

        MewTourLogger.Log($"Sending request to server: {method} {requestUri}");
        
        try
        {
            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(_client.BaseAddress + requestUri),
                Content = content
            };
            
            HttpResponseMessage response = await _client.SendAsync(request);
            
            MewTourLogger.Log($"Request status: {(int) response.StatusCode} {response.StatusCode}");
            
            if (response.IsSuccessStatusCode)
            {
                MewTourLogger.Log("Request sent successfully.");
            }
            else
            {
                string errorBody = await response.Content.ReadAsStringAsync();
                MewTourLogger.Log($"Request error: {errorBody}");
            }

            return response;
        }
        catch (Exception ex)
        {
            MewTourLogger.Log($"Request exception: {ex.Message}");
            return new HttpResponseMessage();
        }
    }
}