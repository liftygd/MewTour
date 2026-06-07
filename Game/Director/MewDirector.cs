using System;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;

namespace MewTour.Game.Director;

public class MewDirector : Manager
{
    private static IntPtr? _mewDirector;
    private static HookSlot _mewDirectorHook;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        Initialize(main);
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
        }
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
}