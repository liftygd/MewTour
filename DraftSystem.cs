using System.Net.Http;
using System;
using System.Text.Json;
using System.Linq;

namespace RerollMod;

public class DraftSystem
{
    private const string endpoint = "https://mewgenics-draft-system.hulore.workers.dev/api/lobbies/";

    private HttpClient client = new HttpClient() { BaseAddress = new Uri(endpoint) };
    private string name;

    public DraftSystem(string playerName) { this.name = playerName; }

    /**
     * Возвращает id лобби по нику одного из игроков
     */
    private string findLobby()
    {
        return "S77Q28";
    }

    /**
     * Возвращает массив пикнутых классов. При ошибке возвращает пустой массив.
     * БЕЗКЛАССОВЫЙ НАЗЫВАЕТСЯ Collarless, А НЕ colorless!!!
     * 
     * <returns>
     * Пример: ["Butcher", "Hunter", "Necromancer", "Collarless", "Cleric"]
     * </returns>
     */
    public string?[] getPickedClasses()
    {
        string url = $"{findLobby()}/results";
        HttpResponseMessage response = client.GetAsync(url).Result;
        response.EnsureSuccessStatusCode();

        string jsonString = response.Content.ReadAsStringAsync().Result;

        using JsonDocument doc = JsonDocument.Parse(jsonString);

        var players = doc.RootElement.GetProperty("players");

        foreach (var player in players.EnumerateArray())
        {
            if (player.GetProperty("name").GetString() == name)
            {
                var classes = player.GetProperty("classes");
                return classes.EnumerateArray().Select(c => c.GetString()).ToArray();
            }
        }

        return Array.Empty<string>();
    }
}