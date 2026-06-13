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
    private MewDirector _mewDirector;
    private ModConfig _config;

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
        _mewDirector.OnLockedContentRebuild += RefreshClasses;
        
        _classManager = loader.Get<ClassManager>();
        _serverManager = loader.Get<ServerManager>();
        _config = config;
    }

    private async void RefreshClasses()
    {
        if (!_config.GetBool(ConfigVariables.UNLOCK_CLASSES))
            return;
        
        await Task.Delay(1000);
        
        var draftResults = await _serverManager.GetDraftResults();
        var classesToUnlock = _classes;

        if (draftResults.Count > 0)
            classesToUnlock = draftResults;
        
        _classManager.ClearAllClasses();
        foreach (var className in classesToUnlock)
            _classManager.UnlockClass(className);
    }
}