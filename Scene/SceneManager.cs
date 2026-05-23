using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour.Scene;

public class SceneManager : Manager
{
    public Action OnSceneChanged;
    
    private static SceneManager? _instance;
    private static int _lastValue;
    private static IntPtr? _moduleBase;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _instance = this;
        _moduleBase = Process.GetCurrentProcess().MainModule?.BaseAddress;
        
        // RVA Hook
        unsafe
        {
            // Scene changed
            _sceneTransitionHook = main.Hook(
                0x9A8E20,
                (nint)(delegate* unmanaged<nint, nint, nint, void>)&SceneTransitionHook
            );
        }
    }

    public SceneEnum GetCurrentScene()
    {
        switch (_lastValue)
        {
            case 102:
                return SceneEnum.Menu;
            case 90:
                return SceneEnum.SaveSelection;
            case 6:
                return SceneEnum.CatSelectionBeforeAdventure;
            default:
                return SceneEnum.Unknown;
        }
    }
    
    private static HookSlot _sceneTransitionHook;
    [UnmanagedCallersOnly]
    private static unsafe void SceneTransitionHook(nint self, nint arg2, nint arg3)
    {
        if (MewTour.IsActive && _moduleBase != null)
        {
            IntPtr rbx = _moduleBase.Value + 0x12E60D0;
            
            IntPtr rax = Marshal.ReadIntPtr(rbx + 0x38);
            IntPtr address = rax + (0x28000 * 4);

            var finalValue = Marshal.ReadInt32(address);

            if (finalValue != _lastValue)
            {
                _lastValue = finalValue;
                _instance?.OnSceneChanged?.Invoke();
                
                MewTourLogger.Log($"Scene transition: {finalValue}");
            }
        }
        
        _sceneTransitionHook.Invoke(self, arg2, arg3);
    }
}