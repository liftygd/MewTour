using System.Reflection;
using MewgenicsModSdk;
using MewTour.Abstract;
using MewTour.Scene;
using MewTour.UI;
using MewUI.Models;
using MewUI.Utility;

namespace MewTour.Menu;

public class MenuUI : IInjectable
{
    private UIManager _uiManager;
    private SceneManager _sceneManager;
    private ConfigUI _configUi;
    private ModConfig _config;

    public void LoadDependencies(ILoader loader, ModConfig config)
    {
        _uiManager = loader.Get<UIManager>();
        _sceneManager = loader.Get<SceneManager>();
        
        _configUi = new ConfigUI(_uiManager, config);   
        _config = config;
        
        _sceneManager.OnSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged()
    {
        if (_sceneManager.GetCurrentScene() != SceneEnum.Menu)
            return;

        if (MewTour.Instance.IsActive)
        {
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

        _configUi.Refresh();
    }
}