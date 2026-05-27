using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewgenicsModSdk.Game;
using MewTour.Abstract;
using MewTour.Run;
using MewTour.Scene;
using MewTour.Utility;
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
    
    private static IntPtr? _moduleBase;
    private static nint _uiPtr;
    
    private static HookSlot _catSelectScreenHook;
    private static HookSlot _recomputeUIHook;

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
        _moduleBase = Process.GetCurrentProcess().MainModule?.BaseAddress;
        
        // RVA Hooks
        unsafe
        {
            // Cat changed on cat select screen
            _catSelectScreenHook = main.Hook(
                0xDFC70,
                (nint) (delegate* unmanaged<nint, nint, nint, void>) &CatSelectScreenHook
            );
            
            // Recompute UI hook
            _recomputeUIHook = main.Hook(
                0xDFC70,
                (nint) (delegate* unmanaged<nint, nint, void>) &RecomputeUIHook
            );
        }
    }
    
    public void DrawRollButton(int catId)
    {
        if (!MewTour.IsActive)
            return;
        
        if (_runManager.RunActive)
            return;
        
        if (_sceneManager.GetCurrentScene() != SceneEnum.CatSelectionBeforeAdventure)
            return;

        var cats = _runManager.GetAdventureCats();
        GameChar? cat = cats.Where(c => c.CatId == catId)
            .FirstOrDefault();
        
        if (cats.Count <= 0 || cat == null)
            return;
        
        _uiManager.AddElement("button_roll", (manager) =>
        {
            var catRollButton = manager.CreateButtonFromResource(
                id: "button_roll",
                resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Roll.png"),
                layout: RelativeRect.FromReference(800, 600, 96, 96),
                onClick: _ =>
                {
                    unsafe
                    {
                        MewTourLogger.Log($"Rolling cat -> {_currentCatId}");
                        _rerollManager.RollCat(cat.Value);
                        
                        if (_uiPtr != IntPtr.Zero)
                            _recomputeUIHook.Invoke(_uiPtr, _currentCatId);
                    }
                });
            
            catRollButton.HighlightOnHover = true;
            catRollButton.HoverHighlightStrength = 0.75f;

            return catRollButton;
        });
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void CatSelectScreenHook(nint self, nint catId, nint arg3)
    {
        if (MewTour.IsActive)
        {
            _currentCatId = catId.ToInt32();
            MewTourLogger.Log($"Selected cat -> {_currentCatId}");

            _instance.DrawRollButton(_currentCatId);
        }
        
        _catSelectScreenHook.Invoke(self, catId, arg3);
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void RecomputeUIHook(nint uiPtr, nint catId)
    {
        MewTourLogger.Log($"Recomputing UI -> {catId.ToInt32()}");
        
        _uiPtr = uiPtr;
        _recomputeUIHook.Invoke(uiPtr, catId);
    }
}