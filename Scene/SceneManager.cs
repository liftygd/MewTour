using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour.Scene;

public class SceneManager : Manager
{
    public Action OnSceneTransitioned;
    public Action OnSceneChanged;

    public SceneEnum CurrentScene { get; private set; } = SceneEnum.Unknown;
    public SceneEnum PreviousScene { get; private set; } = SceneEnum.Unknown;
    
    private static SceneManager? _instance;

    private IntPtr? _moduleBase;
    private static HookSlot _sceneTransitionHook;
    private static HookSlot _sceneChangeHook;
    
    private Dictionary<IntPtr, SceneEnum> _sceneMap = new Dictionary<IntPtr, SceneEnum>();
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _instance = this;
        _moduleBase = Process.GetCurrentProcess().MainModule?.BaseAddress;
        
        InitializeDictionary();
        
        // RVA Hook
        unsafe
        {
            // Scene changed
            _sceneTransitionHook = main.Hook(
                0x9A8E20,
                (nint)(delegate* unmanaged<nint, nint, nint, void>)&SceneTransitionHook
            );
            
            // Next scene
            _sceneChangeHook = main.Hook(
                0x95AF40,
                (nint)(delegate* unmanaged<nint, nint, void>)&SceneChangeHook
            );
        }
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void SceneTransitionHook(nint arg1, nint arg2, nint arg3)
    {
        try
        {
            if (MewTour.Instance.IsActive)
            {
                byte flag = Marshal.ReadByte(arg1, 0xA2);

                if (flag == 0)
                {
                    var firstPointer = SafeMemoryReader.ReadIntPtrSafe(arg1 + 0x68);

                    if (firstPointer != IntPtr.Zero)
                    {
                        var previousSceneObj = SafeMemoryReader.ReadIntPtrSafe(firstPointer);
                        _instance?.TransitionScene(previousSceneObj);
                    }
                }
            }
        }
        catch
        {
            // ignored
        }

        _sceneTransitionHook.Invoke(arg1, arg2, arg3);
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void SceneChangeHook(nint arg1, nint arg2)
    {
        try
        {
            if (MewTour.Instance.IsActive)
                _instance?.ChangeScene(SafeMemoryReader.ReadIntPtrSafe(arg2));
        }
        catch
        {
            // ignored
        }

        _sceneChangeHook.Invoke(arg1, arg2);
    }

    private void InitializeDictionary()
    {
        if (_moduleBase == null)
            return;
        
        _sceneMap.Clear();
        
        _sceneMap.Add(_moduleBase.Value + 0xEDA840, SceneEnum.Menu);
        _sceneMap.Add(_moduleBase.Value + 0xEDAB20, SceneEnum.SaveSelection);
        _sceneMap.Add(_moduleBase.Value + 0xF02DD8, SceneEnum.House);
        _sceneMap.Add(_moduleBase.Value + 0xEEFFE0, SceneEnum.Interstitial);
        _sceneMap.Add(_moduleBase.Value + 0xF09180, SceneEnum.ClassSelection);
        _sceneMap.Add(_moduleBase.Value + 0xF06950, SceneEnum.InventoryScreen2);
        _sceneMap.Add(_moduleBase.Value + 0xF08D30, SceneEnum.ActSelection);
    }

    private void TransitionScene(IntPtr? previousScenePtr)
    {
        if (previousScenePtr == null ||
            previousScenePtr == IntPtr.Zero)
            return;

        CurrentScene = SceneEnum.Unknown;
        PreviousScene = _sceneMap.GetValueOrDefault(previousScenePtr.Value, SceneEnum.Unknown);
        
        MewTourLogger.Log($"Transitioned from scene -> {PreviousScene}");
        OnSceneTransitioned?.Invoke();
    }

    private void ChangeScene(IntPtr? newScenePtr)
    {
        if (newScenePtr == null ||
            newScenePtr == IntPtr.Zero)
            return;
        
        var newScene = _sceneMap.GetValueOrDefault(newScenePtr.Value, SceneEnum.Unknown);
        if (newScene == SceneEnum.Unknown)
            return;
        
        CurrentScene = newScene;
        MewTourLogger.Log($"Changed to scene -> {CurrentScene}");
        OnSceneChanged?.Invoke();
    }
}