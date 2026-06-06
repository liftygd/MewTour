using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Run;
using MewTour.Scene;
using MewTour.Utility;
using MewUI.Core;
using MewUI.Rendering;
using MewUI.Utility;

namespace MewTour.UI;

public class UIManager : Manager
{
    private Dictionary<string, Drawable> _uiElements = new();
    
    private SceneManager _sceneManager;
    private RunManager _runManager;

    private static HookSlot _uiRefreshHook;
    private static UIManager _instance;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        MewUILogger.Logging = false;
        
        MewUI.MewUI.Initialize(Assembly.GetExecutingAssembly());
        MewUIManager.Instance.RegisterFontFromResource("opsilon", "UI/Fonts/Opsilon-Regular.ttf");
        
        Initialize(main);
    }

    private void Initialize(MewTour main)
    {
        _instance = this;
        
        unsafe
        {
            // Refresh UI when scene transitions
            _uiRefreshHook = main.Hook(
                0x9A8E20,
                (nint) (delegate* unmanaged<nint, nint, nint, void>) &UIRefreshHook
            );
        }
    }
    
    [UnmanagedCallersOnly]
    private static unsafe void UIRefreshHook(nint arg1, nint arg2, nint arg3)
    {
        if (MewTour.Instance.IsActive)
        {
            byte flag = Marshal.ReadByte(arg1, 0xA2);

            if (flag == 0)
                _instance.ClearUI();
        }

        _uiRefreshHook.Invoke(arg1, arg2, arg3);
    }

    public override void LoadDependencies(ILoader loader, ModConfig config)
    {
        _sceneManager = loader.Get<SceneManager>();
        _sceneManager.OnSceneChanged += ClearUI;
        
        _runManager = loader.Get<RunManager>();
        _runManager.OnRunStarted += ClearUI;
        _runManager.OnRunEnded += ClearUI;
    }

    public Drawable? AddElement(string id, Func<MewUIManager, Drawable> builder)
    {
        RemoveElement(id);
        
        var drawable = builder?.Invoke(MewUIManager.Instance);
        if (drawable == null)
            return null;
        
        MewUIManager.Instance.AddDrawable(drawable);
        _uiElements.Add(id, drawable);
        return drawable;
    }
    
    public Drawable? AddElement(Drawable? element)
    {
        if (element == null)
            return null;

        var drawable = AddElement(element.Id, manager => element);
        return drawable;
    }
    
    public void RemoveElement(Drawable? element)
    {
        if (element == null)
            return;
        
        RemoveElement(element.Id);
    }

    public void RemoveElement(string id)
    {
        if (!_uiElements.ContainsKey(id)) 
            return;
        
        MewUIManager.Instance.RemoveDrawable(id);
        _uiElements.Remove(id);
    }

    public void ClearUI()
    {
        MewUI.MewUI.Clear();
        _uiElements.Clear();
    }
}