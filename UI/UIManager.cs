using System;
using System.Collections.Generic;
using System.Reflection;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Run;
using MewTour.Scene;
using MewUI.Core;
using MewUI.Rendering;
using MewUI.Utility;

namespace MewTour.UI;

public class UIManager : Manager
{
    private Dictionary<string, Drawable> _uiElements = new();
    
    private SceneManager _sceneManager;
    private RunManager _runManager;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        MewUILogger.Logging = false;
        
        MewUI.MewUI.Initialize(Assembly.GetExecutingAssembly());
        MewUIManager.Instance.RegisterFontFromResource("opsilon", "UI/Fonts/Opsilon-Regular.ttf");
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