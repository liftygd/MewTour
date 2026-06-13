using System;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Scene;
using MewTour.Utility;

namespace MewTour.Game.Director;

public class MewDirector : Manager
{
    public Action OnLockedContentRebuild;
    
    private static IntPtr? _mewDirector;
    private static HookSlot _mewDirectorHook;
    private static HookSlot _contentRebuildHook;
    
    private SceneManager _sceneManager;
    private static MewDirector _instance;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _instance = this;
        
        Initialize(main);
    }

    public override void LoadDependencies(ILoader loader, ModConfig config)
    {
        _sceneManager = loader.Get<SceneManager>();
    }

    public IntPtr? GetMewDirector()
    {
        if (_mewDirector == IntPtr.Zero)
            return null;

        return _mewDirector;
    }

    public IntPtr? GetLockedContent()
    {
        if (_mewDirector == IntPtr.Zero)
            return null;
        
        return _mewDirector + 0x38;
    }
    
    private void Initialize(MewTour main)
    {
        unsafe
        {
            // Class unlock hook
            _mewDirectorHook = main.Hook(
                0x3ADD50,
                (nint) (delegate* unmanaged<nint, void>) &MewDirectorHook
            );
            
            // Locked content rebuild hook
            _contentRebuildHook = main.Hook(
                0x228950,
                (nint) (delegate* unmanaged<nint, void>) &LockedContentRebuildHook
            );
        }
    }

    private void LockedContentRebuild()
    {
        if (_sceneManager.CurrentScene != SceneEnum.House)
            return;
        
        MewTourLogger.Log("Locked content rebuilt.");
        OnLockedContentRebuild?.Invoke();
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void MewDirectorHook(nint mewDirector)
    {
        try
        {
            if (MewTour.Instance.IsActive)
                _mewDirector = mewDirector;
        }
        catch
        {
            // ignored
        }
        
        _mewDirectorHook.Invoke(mewDirector);
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void LockedContentRebuildHook(nint arg1)
    {
        _contentRebuildHook.Invoke(arg1);
        
        if (MewTour.Instance.IsActive)
            _instance.LockedContentRebuild();
    }
}