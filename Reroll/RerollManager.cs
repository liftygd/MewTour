using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Run;
using MewTour.Utility;

namespace MewTour.Reroll;

public class RerollManager : Manager
{
    private Random _random = new Random();
    
    private Dictionary<string, List<string>> _abilities = new();
    private Dictionary<string, List<string>> _passives = new();

    private RunManager _runManager;
    private Server.ServerManager _serverManager;
    private ModConfig _config;

    private RerollUI _rerollUi;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _config = config;
        
        var rerollConfig = new RerollConfig();
        rerollConfig.LoadConfig(_abilities, _passives);

        _rerollUi = new RerollUI();
        _rerollUi.Initialize(main);
    }

    public override void LoadDependencies(ILoader loader)
    {
        _runManager = loader.Get<RunManager>();
        _serverManager = loader.Get<Server.ServerManager>();
        _rerollUi.LoadDependencies(loader);
        
        _runManager.OnRunStarted += UpdateCats;
        _runManager.OnFightStarted += UpdateCats;
    }

    private string RandomSpell(string catClass)
    {
        MewTourLogger.Log("RandomSpell triggered");
        MewTourLogger.Log($"CatClass: {catClass}");
        string abil = _abilities[catClass][_random.Next(_abilities[catClass].Count)];
        MewTourLogger.Log(abil);
        return abil;
    }

    private string RandomPassive(string catClass)
    {
        MewTourLogger.Log("RandomPassive triggered");
        MewTourLogger.Log($"CatClass: {catClass}");
        string passive = _passives[catClass][_random.Next(_passives[catClass].Count)];
        MewTourLogger.Log(passive);
        return passive;
    }
    
    public void RollCat(GameChar cat)
    {
        var catNameComposite = cat.Name.Split(" | ");
        int rollCount = 0;
        bool rollLock = catNameComposite is [_, _, "L"];
            
        if (rollLock)
            return;
        
        string sp = cat.Spell1;
        string pa = cat.Passive0;

        string sp_n = RandomSpell(cat.ClassName.ToLower());
        string pa_n = RandomPassive(cat.ClassName.ToLower());

        while (sp == sp_n) sp_n = RandomSpell(cat.ClassName.ToLower());
        while (pa == pa_n) pa_n = RandomPassive(cat.ClassName.ToLower());

        cat.Spell1 = sp_n;
        cat.Passive0 = pa_n;
        
        if (catNameComposite.Length >= 2 && Int32.TryParse(catNameComposite[1], out var roll))
            rollCount = roll + 1;
        
        cat.Name = $"{catNameComposite[0]} | {rollCount}";
        
        _serverManager.ActivateClient(_config);
        _serverManager.RollCat(
            Guid.Parse(_config.GetString("playerId")),
            cat);
    }
    
    private void UpdateCatOnServer(GameChar cat)
    {
        string call = _serverManager.CreateCatState(
            Guid.Parse(_config.GetString("playerId")),
            cat);
        
        MewTourLogger.Log(call);
        
        _serverManager.ActivateClient(_config);
        _serverManager.UpdateCat(call);
    }
    
    private void UpdateCats()
    {
        List<GameChar> cats = _runManager.GetAdventureCats();
        
        for (int i = 0; i < cats.Count; i++)
        {
            var cat = cats[i];
            var catNameComposite = cat.Name.Split(" | ");
            StringBuilder catName = new StringBuilder();
            
            int rollCount = 0;
            if (catNameComposite.Length >= 2 && Int32.TryParse(catNameComposite[1], out var roll))
                rollCount = roll;

            catName.Append($"{catNameComposite[0]} | {rollCount}");
            
            cat.Name = catName.ToString();
            UpdateCatOnServer(cat);
        }
    }
}