using System.Collections.Generic;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Scene;
using MewTour.Utility;

namespace MewTour.Game.Class;

public class ClassUnlocker : IInjectable
{
    private ClassManager _classManager;
    private ModConfig _config;
    
    private bool _refresh = true;

    private readonly List<string> _classes = new List<string>
    {
        "Fighter", "Hunter", "Mage", "Tank",
        "Medic", "Thief", "Necromancer", "Tinkerer",
        "Butcher", "Druid", "Psychic", "Monk",
        "Jester"
    };
    
    public void LoadDependencies(ILoader loader, ModConfig config)
    {
        var sceneManager = loader.Get<SceneManager>();
        sceneManager.OnSceneChanged += () => _refresh = true;
        
        _classManager = loader.Get<ClassManager>();
        _config = config;

        GameEvents.OnHouseUpdate += HouseUpdate;
    }

    private void HouseUpdate(HouseUpdateEvent houseUpdateEvent)
    {
        if (!MewTour.Instance.IsActive)
            return;
        
        if (!_refresh)
            return;
        
        RefreshClasses();
    }

    private void RefreshClasses()
    {
        _refresh = false;

        if (!_config.GetBool(ConfigVariables.UNLOCK_CLASSES))
            return;
        
        _classManager.ClearAllClasses();
        foreach (var className in _classes)
            _classManager.UnlockClass(className);
    }
}