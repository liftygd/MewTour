using System;
using System.Collections.Generic;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Scene;
using MewTour.Server;
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
    private ServerManager _serverManager;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        RunActive = config.GetBool(ConfigVariables.RUN_ACTIVE);
        
        GameEvents.OnAdventureStart += OnAdventureStart;
        GameEvents.OnAdventureReturn += OnAdventureReturn;
        GameEvents.OnFightStart += OnFightStart;
        GameEvents.OnFightEnd += OnFightEnd;
        GameEvents.OnHouseUpdate += OnHouseUpdate;

        _config = config;
    }

    public override void LoadDependencies(ILoader loader, ModConfig config)
    {
        _serverManager = loader.Get<ServerManager>();
    }

    private void OnHouseUpdate(HouseUpdateEvent @event)
    {
        if (!MewTour.Instance.IsActive) return;
        if (!RunActive) return;

        EndRun();
    }
    
    private void OnFightStart(FightStartEvent @event)
    {
        if (!MewTour.Instance.IsActive) return;
        MewTourLogger.Log("OnFightStart");
        
        StartRun(false);
        OnFightStarted?.Invoke();
    }
    
    private void OnFightEnd(FightEndEvent @event)
    {
        if (!MewTour.Instance.IsActive) return;
        
        MewTourLogger.Log("OnFightEnd");
        OnFightEnded?.Invoke();

        if (@event.Result != FightResult.Lose) 
            return;
        
        EndRun();
    }

    private void OnAdventureReturn(AdventureReturnEvent @event)
    {
        if (!MewTour.Instance.IsActive) return;
        MewTourLogger.Log("OnAdventureReturn");
        
        EndRun();
    }

    private void OnAdventureStart(AdventureStartEvent @event)
    {
        if (!MewTour.Instance.IsActive) return;
        
        MewTourLogger.Log("OnAdventureStart");
        StartRun();
    }
    
    private void StartRun(bool callEvent = true)
    {
        if (RunActive) return;
        
        MewTourLogger.Log("Started run");
        RunActive = true;
        
        if (callEvent)
            OnRunStarted?.Invoke();
        
        _config.Set(ConfigVariables.RUN_ACTIVE, RunActive);
    }
    
    private void EndRun()
    {
        if (!RunActive) return;
        
        MewTourLogger.Log("Ended run");
        RunActive = false;
        OnRunEnded?.Invoke();
        
        _config.Set(ConfigVariables.RUN_ACTIVE, RunActive);
        
        _serverManager.ActivateClient(_config);
        _serverManager.EndRun();
    }
    
    public List<GameChar> GetAdventureCats()
    {
        List<GameChar> cats = GameWorld.Current.GetCats();
        for (int i = cats.Count - 1; i >= 0; i--)
            if (!cats[i].IsInAdventureParty) cats.RemoveAt(i);

        return cats;
    }
}