using System.Collections.Generic;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Scene;
using MewTour.Utility;

namespace MewTour.Game.World;

public class WorldUnlocker : IInjectable
{
    private WorldManager _worldManager;
    private SceneManager _sceneManager;
    private ModConfig _config;

    // All act 1 + start act 2
    private readonly List<string> _act1 = new List<string>
    {
        "Sewers", "Junkyard", "Caves", "Boneyard",
        "Desert"
    };
    
    // All act 2 + start act 3
    private readonly List<string> _act2 = new List<string>
    {
        "Desert", "Bunker", "Crater", "Core", "Moon",
        "Lab"
    };
    
    // All act 3
    private readonly List<string> _act3 = new List<string>
    {
        "Lab", "IceAge", "Future", "Jurassic", "TheEnd",
    };
    
    public void LoadDependencies(ILoader loader, ModConfig config)
    {
        _sceneManager = loader.Get<SceneManager>();
        _sceneManager.OnSceneChanged += UnlockWorlds;
        
        _worldManager = loader.Get<WorldManager>();
        _config = config;
    }

    private void UnlockWorlds()
    {
        if (_sceneManager.CurrentScene != SceneEnum.House)
            return;

        if (_config.GetBool(ConfigVariables.UNLOCK_ACT_1))
            UnlockAct(in _act1);

        if (_config.GetBool(ConfigVariables.UNLOCK_ACT_2))
            UnlockAct(in _act2);
        
        if (_config.GetBool(ConfigVariables.UNLOCK_ACT_3))
            UnlockAct(in _act3);
    }

    private void UnlockAct(ref readonly List<string> actMaps)
    {
        _worldManager.UnlockMap("HardPath");
        
        foreach (var map in actMaps)
            _worldManager.UnlockMap(map);
    }
}