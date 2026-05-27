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
            _sceneUpdateHook = main.Hook(
                0x9A5020,
                (nint)(delegate* unmanaged<nint, nint, void>)&SceneUpdateHook
            );
        }
    }

    // TODO: These numbers are clearly some world states. For example, if you are at home, it is 24. But if it is raining, it would be 6, conflicting with current logic.
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
    
    private static HookSlot _sceneUpdateHook;
    [UnmanagedCallersOnly]
    private static unsafe void SceneUpdateHook(nint self, nint arg2)
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
        
        _sceneUpdateHook.Invoke(self, arg2);
    }
}