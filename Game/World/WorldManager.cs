using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour.Game.World;

public class WorldManager : Manager
{
    private static HookSlot _mapFlagUnlockHook;
    
    private IntPtr? _moduleBase;
    
    private const int MapGenManagerRva = 0x13C7BD0;
    private const int HashMapOffset = 0x38;
    private const int NodeValueOffset = 0x30;
    
    [StructLayout(LayoutKind.Sequential)]
    private struct MapFlagInsertResult
    {
        public nint Node;
        public byte Inserted;
    }
    
    public override void Configure(MewTour main, ModConfig config)
    {
        _moduleBase = Process.GetCurrentProcess().MainModule?.BaseAddress;
        Initialize(main);
    }

    private void Initialize(MewTour main)
    {
        unsafe
        {
            // Map unlock hook
            _mapFlagUnlockHook = main.Hook(
                0x60D20,
                (nint) (delegate* unmanaged<nint, nint, nint, nint>) &MapFlagInsertHook
            );
        }
    }
    
    [UnmanagedCallersOnly]
    private static unsafe nint MapFlagInsertHook(nint hashMap, nint outPtr, nint keyPtr)
    {
        return _mapFlagUnlockHook.InvokeRet(hashMap, outPtr, keyPtr);
    }

    public void UnlockMap(string mapName)
    {
        MewTourLogger.Log($"Trying to unlock map: {mapName}");
        
        if (_moduleBase == null)
            return;

        IntPtr mapGenManager = Marshal.ReadIntPtr(_moduleBase.Value + MapGenManagerRva);
        if (mapGenManager == IntPtr.Zero)
        {
            MewTourLogger.Log("MapGen manager not initialized");
            return;
        }
        
        string mapFlag = $"mapflag_{mapName}Unlocked";
        
        unsafe
        {
            nint hashMapContainer = mapGenManager + HashMapOffset;
            GameString gameString = GameString.FromManaged(mapFlag);
            nint keyPtr = (nint)(&gameString);
            
            MapFlagInsertResult result = default;
            _mapFlagUnlockHook.InvokeRet(hashMapContainer, (nint)(&result), keyPtr);
            GameString.Free(ref gameString);
            
            if (result.Node != 0)
            {
                *(long*)(result.Node + NodeValueOffset) = 1;
                MewTourLogger.Log($"Unlocked map: {mapName} ({mapFlag})");
            }
            else
            {
                MewTourLogger.Log($"Failed to unlock map: {mapName} ({mapFlag})");
            }
        }
    }
}