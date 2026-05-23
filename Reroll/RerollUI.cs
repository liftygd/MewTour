using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Run;
using MewTour.Scene;
using MewUI.Core;
using MewUI.Models;
using MewUI.Utility;

namespace MewTour.Reroll;

public class RerollUI : IInjectable
{
    private RerollManager _rerollManager;
    private RunManager _runManager;
    private UI.UIManager _uiManager;
    private SceneManager _sceneManager;
    
    private static RerollUI _instance;
    private static int _currentCatId;

    public void LoadDependencies(ILoader loader)
    {
        _rerollManager = loader.Get<RerollManager>();
        _runManager = loader.Get<RunManager>();
        _uiManager = loader.Get<UI.UIManager>();
        _sceneManager = loader.Get<SceneManager>();

        _sceneManager.OnSceneChanged += () => DrawRollButton(_currentCatId);
    }

    public void Initialize(MewTour main)
    {
        _instance = this;
        
        // RVA Hooks
        unsafe
        {
            // Cat changed on cat select screen
            _catSelectScreenHook = main.Hook(
                0xDFC70,
                (nint) (delegate* unmanaged<nint, nint, nint, void>) &CatSelectScreenHook
            );
        }
    }
    
    public void DrawRollButton(int catId)
    {
        if (_runManager.RunActive)
            return;
        
        if (_sceneManager.GetCurrentScene() != SceneEnum.CatSelectionBeforeAdventure)
            return;
        
        GameChar? cat = _runManager.GetAdventureCats()
            .Where(c => c.CatId == catId)
            .FirstOrDefault();
        
        if (cat == null)
            return;

        _uiManager.AddElement("button_roll", (manager) =>
        {
            var catRollButton = manager.CreateButtonFromResource(
                id: "button_roll",
                resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Roll.png"),
                layout: RelativeRect.FromReference(800, 600, 96, 96),
                onClick: _ => _rerollManager.RollCat(cat.Value)
            );
            
            catRollButton.HighlightOnHover = true;
            catRollButton.HoverHighlightStrength = 0.75f;

            return catRollButton;
        });
    }
    
    private static HookSlot _catSelectScreenHook;
    [UnmanagedCallersOnly]
    private static unsafe void CatSelectScreenHook(nint self, nint catId, nint arg3)
    {
        if (MewTour.IsActive)
        {
            _currentCatId = catId.ToInt32();
            _instance.DrawRollButton(_currentCatId);
        }
        
        _catSelectScreenHook.Invoke(self, catId, arg3);
    }
}