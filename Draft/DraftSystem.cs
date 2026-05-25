using System.Net.Http;
using System;
using System.Text.Json;
using System.Linq;
using MewTour.Utility;

namespace MewTour.Draft;

public class DraftSystem
{
    private const string endpoint = "https://mewgenics-draft-system.hulore.workers.dev/api/player-results";
    private HttpClient client = new HttpClient() { BaseAddress = new Uri(endpoint) };
    private string name;


    public DraftSystem(string playerName) { this.name = playerName; }

    public string[] getPickedClasses()
    {
        string url = $"{endpoint}?name={Uri.EscapeDataString(name)}";
        HttpResponseMessage response = client.GetAsync(url).Result;

        if (!response.IsSuccessStatusCode)
        {
            MewTourLogger.Log($"Draft system: failed to get classes for '{name}' - status code {response.StatusCode}");
            return Array.Empty<string>();
        }

        string jsonString = response.Content.ReadAsStringAsync().Result;

        using JsonDocument doc = JsonDocument.Parse(jsonString);

        if (doc.RootElement.TryGetProperty("classes", out var classes))
        {
            MewTourLogger.Log($"Draft system: successfully retrieved {classes.GetArrayLength()} classes for player '{name}'");
            return classes.EnumerateArray().Select(c => c.GetString()!).ToArray();
        }
        MewTourLogger.Log($"Draft system: 'classes' property not found in response for player '{name}'");
        return Array.Empty<string>();
    }
}