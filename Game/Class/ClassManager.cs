using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Game.Director;
using MewTour.Utility;

namespace MewTour.Game.Class;

public class ClassManager : Manager
{
    private static HookSlot _classUnlockHook;
    private MewDirector _mewDirector;
    
    private IntPtr? _moduleBase;
    private IntPtr? _fapoBase;

    private const int AvailableClassesVecOffset = 0xC0;
    private const int ChangedFlagOffset = 0x328;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _moduleBase = Process.GetCurrentProcess().MainModule?.BaseAddress;
        _fapoBase = _moduleBase + 0x50AF0;
        
        Initialize(main);
    }

    public override void LoadDependencies(ILoader loader, ModConfig config)
    {
        _mewDirector = loader.Get<MewDirector>();
    }

    private void Initialize(MewTour main)
    {
        unsafe
        {
            // Class unlock hook
            _classUnlockHook = main.Hook(
                0x2306C0,
                (nint) (delegate* unmanaged<nint, nint, void>) &ClassUnlockHook
            );
        }
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void ClassUnlockHook(nint classManager, nint className)
    {
        _classUnlockHook.Invoke(classManager, className);
    }
    
    public void UnlockClass(string className)
    {
        MewTourLogger.Log($"Trying to unlock class: {className}");
        
        var lockedContent = _mewDirector.GetLockedContent();
        if (lockedContent == null)
            return;
        
        unsafe
        {
            GameString gameString = GameString.FromManaged(className);
            nint ptr = (nint)(&gameString);
            
            _classUnlockHook.Invoke(lockedContent.Value, ptr);
            MewTourLogger.Log($"Unlocked class: {className}");
        }
    }
    
    public void ClearAllClasses()
    {
        var lockedContent = _mewDirector.GetLockedContent();
        if (lockedContent == null || 
            _fapoBase == null)
            return;
        
        MewTourLogger.Log("Clearing all unlocked classes");
        
        unsafe
        {
            IntPtr lockedContentPtr = lockedContent.Value;
            IntPtr vecPtr = lockedContentPtr + AvailableClassesVecOffset;
            
            IntPtr begin = *(IntPtr*)vecPtr;
            IntPtr end = *(IntPtr*)(vecPtr + 8);
            
            // Clear memory
            var destructor = (delegate* unmanaged<IntPtr, void>)(_fapoBase.Value + 0x17E0);
            for (IntPtr ptr = begin; ptr < end; ptr += 32)
            {
                destructor(ptr);
            }
            
            // Reset vector to empty (set end = begin)
            *(IntPtr*)(vecPtr + 8) = begin;
            
            // Set changed flag
            *(byte*)(lockedContentPtr + ChangedFlagOffset) = 1;
            
            MewTourLogger.Log("Cleared all unlocked classes");
        }
    }
}