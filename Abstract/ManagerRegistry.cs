using System;
using System.Collections.Generic;
using System.Linq;
using MewgenicsModSdk;
using MewTour.Game.Class;
using MewTour.Game.Director;
using MewTour.Game.World;
using MewTour.Menu;
using MewTour.Reroll;
using MewTour.Run;
using MewTour.Scene;
using MewTour.Server;
using MewTour.UI;
using MewTour.Utility;

namespace MewTour.Abstract;

public class ManagerRegistry : ILoader
{
    private readonly Dictionary<Type, Manager> _managers = new();
    
    // Have to specify all of them, since they are being trimmed in AOT because of using reflection
    private List<Manager> managers = new ()
    {
        new SceneManager(),
        new UIManager(),
        new RunManager(),
        new ServerManager(),
        new RerollManager(),
        new MewDirector(),
        new ClassManager(),
        new WorldManager()
    };

    private List<IInjectable> injectables = new ()
    {
        new MenuUI(),
        new ClassUnlocker(),
        new WorldUnlocker()
    };

    public void InitializeAll(MewTour main, ModConfig config)
    {
        MewTourLogger.Log($"Initializing Manager registry. Managers found: {managers.Count}");

        var elementsToInject = managers.Select(m => (IInjectable)m).ToList();
        elementsToInject.AddRange(injectables);
        
        // Initialize
        foreach (var manager in managers)
        {
            manager.Configure(main, config);
            _managers.Add(manager.GetType(), manager);
            
            MewTourLogger.Log($"Initialized manager: {manager.GetType().Name}");
        }
        
        MewTourLogger.Log("Manager registry initialized");
        
        // Allow to get dependencies
        foreach (var injectable in elementsToInject)
        {
            injectable.LoadDependencies(this, config);
        }
        
        MewTourLogger.Log("Dependencies injected");
    }

    public T Get<T>() where T : Manager
    {
        return (T)_managers[typeof(T)];
    }
}