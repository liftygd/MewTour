using System.Reflection;
using MewTour.Abstract;
using MewTour.Scene;
using MewUI.Models;
using MewUI.Utility;

namespace MewTour.Menu;

public class MenuUI : IInjectable
{
    private UI.UIManager _uiManager;
    private SceneManager _sceneManager;

    public void LoadDependencies(ILoader loader)
    {
        _uiManager = loader.Get<UI.UIManager>();
        _sceneManager = loader.Get<SceneManager>();
        
        _sceneManager.OnSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged()
    {
        if (!MewTour.IsActive)
            return;
        
        if (_sceneManager.GetCurrentScene() != SceneEnum.Menu)
            return;
        
        _uiManager.AddElement("tour_logo", (manager) =>
        {
            var tourLogo = manager.CreateButtonFromResource(
                id: "tour_logo",
                resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Logo.png"),
                layout: RelativeRect.FromReference(50, 175, 287 * 1.25f, 131 * 1.25f)
            );
            
            tourLogo.HighlightOnHover = true;
            tourLogo.HoverHighlightStrength = 0.75f;

            return tourLogo;
        });
    }
}