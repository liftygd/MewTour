using System;
using System.Collections.Generic;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour.Run;

public class RunManager : Manager
{
    public Action OnRunStarted;
    public Action OnRunEnded;
    public Action OnFightStarted;
    public Action OnFightEnded;
    
    public bool RunActive { get; private set; }

    private ModConfig _config;
    private Server.ServerManager _serverManager;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        RunActive = config.GetBool("runActive");
        
        GameEvents.OnAdventureStart += OnAdventureStart;
        GameEvents.OnAdventureReturn += OnAdventureReturn;
        GameEvents.OnFightStart += OnFightStart;
        GameEvents.OnFightEnd += OnFightEnd;
        GameEvents.OnHouseUpdate += OnHouseUpdate;

        _config = config;
    }

    public override void LoadDependencies(ILoader loader)
    {
        _serverManager = loader.Get<Server.ServerManager>();
    }

    private void OnHouseUpdate(HouseUpdateEvent @event)
    {
        if (!MewTour.IsActive) return;
        if (!RunActive) return;

        EndRun();
    }
    
    private void OnFightStart(FightStartEvent @event)
    {
        if (!MewTour.IsActive) return;
        Logger.Log("OnFightStart");
        
        StartRun();
        OnFightStarted?.Invoke();
    }
    
    private void OnFightEnd(FightEndEvent @event)
    {
        if (!MewTour.IsActive) return;
        
        Logger.Log("OnFightEnd");
        OnFightEnded?.Invoke();

        if (@event.Result != FightResult.Lose) 
            return;
        
        EndRun();
    }

    private void OnAdventureReturn(AdventureReturnEvent @event)
    {
        if (!MewTour.IsActive) return;
        Logger.Log("OnAdventureReturn");
        
        EndRun();
    }

    private void OnAdventureStart(AdventureStartEvent @event)
    {
        if (!MewTour.IsActive) return;
        
        Logger.Log("OnAdventureStart");
        StartRun();
    }
    
    private void StartRun()
    {
        if (RunActive) return;
        
        Logger.Log("Started run");
        RunActive = true;
        OnRunStarted?.Invoke();
        
        _config.Set("runActive", RunActive);
    }
    
    private void EndRun()
    {
        if (!RunActive) return;
        
        Logger.Log("Ended run");
        RunActive = false;
        OnRunEnded?.Invoke();
        
        _config.Set("runActive", RunActive);
        
        _serverManager.ActivateClient(_config);
        _serverManager.EndRun(Guid.Parse(_config.GetString("playerId")));
    }
    
    public List<GameChar> GetAdventureCats()
    {
        List<GameChar> cats = GameWorld.Current.GetCats();
        for (int i = cats.Count - 1; i >= 0; i--)
            if (!cats[i].IsInAdventureParty) cats.RemoveAt(i);

        return cats;
    }
}