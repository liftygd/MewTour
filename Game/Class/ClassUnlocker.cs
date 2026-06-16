using System.Collections.Generic;
using System.Threading.Tasks;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Game.Director;
using MewTour.Scene;
using MewTour.Server;
using MewTour.Utility;

namespace MewTour.Game.Class;

public class ClassUnlocker : IInjectable
{
    private ClassManager _classManager;
    private ServerManager _serverManager;
    private SceneManager _sceneManager;
    private MewDirector _mewDirector;
    private ModConfig _config;

    private bool _triedRefresh;
    
    private readonly List<string> _classes = new List<string>
    {
        "Fighter", "Hunter", "Mage", "Tank",
        "Medic", "Thief", "Necromancer", "Tinkerer",
        "Butcher", "Druid", "Psychic", "Monk",
        "Jester"
    };
    
    public void LoadDependencies(ILoader loader, ModConfig config)
    {
        _mewDirector = loader.Get<MewDirector>();
        _mewDirector.OnLockedContentRebuild += () => Task.Run(RefreshClasses);
        
        _sceneManager = loader.Get<SceneManager>();
        _sceneManager.OnSceneChanged += () => Task.Run(ChangedScene);
        
        _classManager = loader.Get<ClassManager>();
        _serverManager = loader.Get<ServerManager>();
        _config = config;
    }

    private async Task ChangedScene()
    {
        _triedRefresh = false;
        if (_sceneManager.CurrentScene != SceneEnum.House)
            return;

        await Task.Delay(2000); 
        await RefreshClasses();
    }

    private async Task RefreshClasses()
    {
        if (_triedRefresh)
            return;
        
        _triedRefresh = true;
        if (!_config.GetBool(ConfigVariables.UNLOCK_CLASSES))
            return;
        
        var draftResults = await _serverManager.GetDraftResults();
        var classesToUnlock = _classes;

        if (draftResults.Count > 0)
            classesToUnlock = draftResults;
        
        _classManager.ClearAllClasses();
        foreach (var className in classesToUnlock)
            _classManager.UnlockClass(className);
    }
}