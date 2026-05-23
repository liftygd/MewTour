using System;
using System.Collections.Generic;
using System.Reflection;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Scene;
using MewUI.Rendering;

namespace MewTour.UI;

public class UIManager : Manager
{
    private Dictionary<string, Drawable> _uiElements = new();
    
    private SceneManager _sceneManager;
    
    public override void Configure(MewTour main, ModConfig config)
    {
        MewUI.Utility.Logger.Logging = false;
        MewUI.MewUI.Initialize(Assembly.GetExecutingAssembly());
    }

    public override void LoadDependencies(ILoader loader)
    {
        _sceneManager = loader.Get<SceneManager>();
        _sceneManager.OnSceneChanged += ClearUI;
    }

    public Drawable? AddElement(string id, Func<MewUI.Core.UIManager, Drawable> builder)
    {
        if (_uiElements.ContainsKey(id))
        {
            MewUI.Core.UIManager.Instance.RemoveDrawable(id);
            _uiElements.Remove(id);
        }
        
        var drawable = builder?.Invoke(MewUI.Core.UIManager.Instance);
        if (drawable == null)
            return null;
        
        _uiElements.Add(id, drawable);
        return drawable;
    }

    public void ClearUI()
    {
        foreach (var element in _uiElements)
        {
            MewUI.Core.UIManager.Instance.RemoveDrawable(element.Key);
        }
        
        _uiElements.Clear();
    }
}