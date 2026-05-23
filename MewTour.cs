using MewgenicsModSdk;
using MewgenicsModSdk.Api;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using MewTour.Abstract;
using MewTour.Utility;

namespace MewTour;

public class MewTour : MewgenicsMod
{
    public override string Id => "mew_tour";
    public override string Name => "MewTour";
    public override string Version => "1.0.0";
    public override string Description => "Mewgenics local tournament mod.";
    public override string Category => "Gameplay";

    public override bool AutoEnable => Config.GetBool("IsActive", true);
    
    public static bool IsActive;
    
    private string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    protected override void OnLoad()
    {
        try
        {
            MewTourLogger.ClearLog();
            MewTourLogger.Log(Name + " loaded");
            
            Config.GetString("playerId", Guid.NewGuid().ToString());
            Config.GetString("playerName", RandomString(5));
            
            var managerRegistry = new ManagerRegistry();
            managerRegistry.InitializeAll(this, Config);
        }
        catch (Exception ex)
        {
            MewTourLogger.Log($"Error while trying to load MewTour: {ex.Message}. {ex.InnerException?.Message}");
        }
    }

    public new HookSlot Hook(ulong rva, nint hookFn) => base.Hook(rva, hookFn);

    protected override void OnEnable()
    {
        MewTourLogger.Log(Name + " enabled");
        IsActive = true;
        Config.Set("IsActive", IsActive);
        
        MewUI.MewUI.Enable();
    }
    
    protected override void OnDisable()
    {
        MewTourLogger.Log(Name + " disabled");
        IsActive = false;
        Config.Set("IsActive", IsActive);
        
        MewUI.MewUI.Disable();
    }
    
    internal static unsafe class Exports
    {
        private static readonly MewTour _mod = new();

        [UnmanagedCallersOnly(EntryPoint = "MewMod_GetInfo")]
        public static ModInfo* GetInfo() { try { return ModInfoHelper.GetInfo(_mod); } catch { return null; } }

        [UnmanagedCallersOnly(EntryPoint = "MewMod_Init")]
        public static void Init(MewgenicsApi* api) { try { _mod.InternalLoad(api); } catch { } }

        [UnmanagedCallersOnly(EntryPoint = "MewMod_Enable")]
        public static void Enable() { try { _mod.InternalEnable(); } catch { } }

        [UnmanagedCallersOnly(EntryPoint = "MewMod_Disable")]
        public static void Disable() { try { _mod.InternalDisable(); } catch { } }

        [UnmanagedCallersOnly(EntryPoint = "MewMod_ConfigReload")]
        public static void ConfigReload() { try { _mod.InternalConfigReload(); } catch { } }
    }
}