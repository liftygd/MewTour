using System.Net.Http;
using System;
using System.Text.Json;
using System.Linq;

namespace RerollMod;

public class DraftSystem
{
    private const string endpoint = "https://mewgenics-draft-system.hulore.workers.dev/api/player-results";

    private HttpClient client = new HttpClient() { BaseAddress = new Uri(endpoint) };
    private string name;

    public DraftSystem(string playerName) { this.name = playerName; }

     /// Возвращает массив пикнутых классов. При ошибке возвращает пустой массив.
     /// БЕЗКЛАССОВЫЙ НАЗЫВАЕТСЯ Collarless, А НЕ colorless!!!
     /// 
     /// <returns>
     /// e.g. ["Butcher", "Hunter", "Necromancer", "Collarless", "Cleric"]
     /// </returns>
    public string[] getPickedClasses()
    {
        string url = $"{endpoint}?name={Uri.EscapeDataString(name)}";
        HttpResponseMessage response = client.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
            return Array.Empty<string>();
        }

        string jsonString = response.Content.ReadAsStringAsync().Result;

        using JsonDocument doc = JsonDocument.Parse(jsonString);

        if (doc.RootElement.TryGetProperty("classes", out var classes))
        {
            return classes.EnumerateArray().Select(c => c.GetString()).ToArray();
        }

        return Array.Empty<string>();
    }
}