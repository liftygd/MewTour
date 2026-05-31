using System;
using System.Numerics;
using System.Reflection;
using MewgenicsModSdk;
using MewTour.UI;
using MewTour.Utility;
using MewUI.Core;
using MewUI.Models;
using MewUI.Rendering;
using MewUI.Utility;

namespace MewTour.Menu;

public class ConfigUI
{
    private enum ConfigTab
    {
        Config,
        Server
    }
    
    private ModConfig _config;
    private UIManager _uiManager;
    
    private bool _isConfigOpen;

    private Drawable? _configButton;
    private DrawableGroup? _configGroup;

    private ConfigTab _currentTab = ConfigTab.Config;

    public ConfigUI(UIManager manager, ModConfig config)
    {
        _uiManager = manager;
        _config = config;
    }

    public void Refresh()
    {
        Clear();
        Draw();
    }

    private void Clear()
    {
        _uiManager.RemoveElement(_configButton);
        _uiManager.RemoveElement(_configGroup);
        
        _configButton = null;
        _configGroup = null;
    }

    private void Draw()
    {
        DrawGear();

        if (!_isConfigOpen)
            return;

        InitGroup();
        DrawNavigation();
        DrawBody();
        DrawHeader();
    }

    private void DrawGear()
    {
        var isModActive = _config.GetBool(ConfigVariables.IS_ACTIVE);
        var activeTint = Color.FromHex("#9dd99a");
        var notActiveTint = Color.FromHex("#d99f9a");
        
        _configButton = _uiManager.AddElement("config_button", (manager) =>
        {
            var configButton = manager.CreateButtonFromResource(
                id: "config_button",
                resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Gear.png"),
                layout: RelativeRect.FromReference(1800, 25, 96, 96),
                onClick: @event =>
                {
                    _isConfigOpen = !_isConfigOpen;
                    Refresh();
                },
                tint: isModActive ? activeTint : notActiveTint
            );

            return configButton;
        });
    }

    private void InitGroup()
    {
        if (_configGroup != null)
            return;
        
        _configGroup = new DrawableGroup("config_group", new Rectangle(0, 0, 1024, 720));
        _uiManager.AddElement(_configGroup);
    }

    private void DrawNavigation()
    {
        var position = 1250;
        var positionWithOffset = position - 20;
        
        // First Tab
        var firstTabButton = MewUIManager.Instance.CreateButtonFromResource(
            id: "config_tab_config_button",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Box_Big.png"),
            layout: RelativeRect.FromReference(_currentTab == ConfigTab.Config ? positionWithOffset : position, 250, 96, 64),
            onClick: @event =>
            {
                _currentTab = ConfigTab.Config;
                Refresh();
            }
        );
        firstTabButton.Interactive = _currentTab != ConfigTab.Config;
        firstTabButton.HighlightOnHover = _currentTab != ConfigTab.Config;

        var firstTabIcon = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_tab_config_preview",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Config_Key.png"),
            layout: RelativeRect.FromReference(_currentTab == ConfigTab.Config ? positionWithOffset : position, 250, 64, 64)
        );
            
        _configGroup?.AddChild(firstTabButton);
        _configGroup?.AddChild(firstTabIcon);
        
        // Second Tab
        var secondTabButton = MewUIManager.Instance.CreateButtonFromResource(
            id: "config_tab_server_button",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Box_Big.png"),
            layout: RelativeRect.FromReference(_currentTab == ConfigTab.Server ? positionWithOffset : position, 350, 96, 64),
            onClick: @event =>
            {
                _currentTab = ConfigTab.Server;
                Refresh();
            }
        );
        secondTabButton.Interactive = _currentTab != ConfigTab.Server;
        secondTabButton.HighlightOnHover = _currentTab != ConfigTab.Server;
        
        var secondTabIcon = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_tab_server_preview",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Config_Server.png"),
            layout: RelativeRect.FromReference(_currentTab == ConfigTab.Server ? positionWithOffset : position, 350, 64, 64)
        );
        
        _configGroup?.AddChild(secondTabButton);
        _configGroup?.AddChild(secondTabIcon);
    }

    private void DrawHeader()
    {
        var header = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_header",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Header.png"),
            layout: RelativeRect.FromReference(1280, 150, 552, 96)
        );
        
        _configGroup?.AddChild(header);

        var xPos = _currentTab == ConfigTab.Config ? 1470 : 1510;
        var text = MewUIManager.Instance.CreateText(
            id: "config_header_text",
            text: _currentTab == ConfigTab.Config ? "Конфигурация" : "Сервер",
            layout: RelativeRect.FromReference(xPos, 175, 320, 96),
            size: 32f,
            Color.Black,
            font: "opsilon"
        );
        
        _configGroup?.AddChild(text);
    }

    private void DrawBody()
    {
        if (_currentTab == ConfigTab.Config)
            DrawConfigTab();
        else
            DrawServerTab();
    }

    private void DrawConfigTab()
    {
        var body = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_tab_config_body",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Box_Big.png"),
            layout: RelativeRect.FromReference(1300, 200, 512, 320)
        );
        
        var configTabGroup = new DrawableGroup("config_group_tab_config", new Rectangle(0, 0, 1024, 720));
        configTabGroup.AddChild(body);
        
        DrawCheckBox(ConfigVariables.IS_ACTIVE, "Включить мод", new Vector2(1330, 220), configTabGroup, (isChecked) =>
        {
            if (!isChecked)
                MewTour.Instance.Disable();
            else
                MewTour.Instance.Enable();
        });
        
        DrawCheckBox(ConfigVariables.UNLOCK_ACT_1, "Открыть весь 1 Акт", new Vector2(1330, 300), configTabGroup, (isChecked) =>
        {
            _config.Set(ConfigVariables.UNLOCK_ACT_1, isChecked);
        });
        
        DrawCheckBox(ConfigVariables.UNLOCK_ACT_2, "Открыть весь 2 Акт", new Vector2(1330, 350), configTabGroup, (isChecked) =>
        {
            _config.Set(ConfigVariables.UNLOCK_ACT_2, isChecked);
        });
        
        DrawCheckBox(ConfigVariables.UNLOCK_ACT_3, "Открыть весь 3 Акт", new Vector2(1330, 400), configTabGroup, (isChecked) =>
        {
            _config.Set(ConfigVariables.UNLOCK_ACT_3, isChecked);
        });
        
        DrawCheckBox(ConfigVariables.UNLOCK_CLASSES, "Открыть все классы", new Vector2(1330, 450), configTabGroup, (isChecked) =>
        {
            _config.Set(ConfigVariables.UNLOCK_CLASSES, isChecked);
        });
        
        _configGroup?.AddChild(configTabGroup);
    }

    private void DrawCheckBox(string configVariable, string text, Vector2 position, DrawableGroup group, Action<bool> @checkboxEvent)
    {
        var isChecked = _config.GetBool(configVariable);
        var image = isChecked ? "CheckBox_Full.png" : "CheckBox_Empty.png";
        
        var checkBoxButton = MewUIManager.Instance.CreateButtonFromResource(
            id: $"config_tab_config_checkbox_{configVariable}",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName($"UI/Images/{image}"),
            layout: RelativeRect.FromReference(position.X, position.Y, 48, 48),
            onClick: @event =>
            {
                isChecked = !isChecked;
                checkboxEvent?.Invoke(isChecked);
                Refresh();
            }
        );
        
        var textUI = MewUIManager.Instance.CreateText(
            id: $"config_tab_config_text_{configVariable}",
            text: text,
            layout: RelativeRect.FromReference(position.X + 60, position.Y + 10, 320, 96),
            size: 32f,
            Color.Black,
            font: "opsilon"
        );
        
        group.AddChild(checkBoxButton);
        group.AddChild(textUI);
    }

    private void DrawServerTab()
    {
        var body = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_tab_server_body",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Box_Big.png"),
            layout: RelativeRect.FromReference(1300, 200, 512, 320)
        );
        
        var configTabGroup = new DrawableGroup("config_group_tab_server", new Rectangle(0, 0, 1024, 720));
        configTabGroup.AddChild(body);

        DrawInputField(configTabGroup);
        DrawSync(configTabGroup);
        
        _configGroup?.AddChild(configTabGroup);
    }

    private void DrawInputField(DrawableGroup group)
    {
        var headerText = MewUIManager.Instance.CreateText(
            id: "config_tab_server_header",
            text: "Ключ игрока",
            layout: RelativeRect.FromReference(1330, 230, 400, 96),
            size: 32f,
            Color.Black,
            font: "opsilon"
        );
        
        var keyBackground = MewUIManager.Instance.CreateEmbeddedTexture(
            id: "config_tab_server_input_background",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Box_Big.png"),
            layout: RelativeRect.FromReference(1330, 260, 400, 48),
            tint: Color.FromHex("#121212")
        );

        var keyInput = MewUIManager.Instance.CreateInputField(
            id: "config_tab_server_input_field",
            layout: RelativeRect.FromReference(1330, 260, 400, 48),
            placeholder: "Введите ключ игрока...",
            fontSize: 24f,
            textColor: Color.White,
            backgroundColor: Color.Transparent,
            font: "opsilon"
        );
        
        var keyClear = MewUIManager.Instance.CreateButtonFromResource(
            id: "config_tab_server_input_clear",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Clear.png"),
            layout: RelativeRect.FromReference(1750, 260, 36, 48),
            onClick: @event =>
            {
                _config.Set(ConfigVariables.SERVER, "");
                keyInput.SetText(String.Empty);
            }
        );
        
        var currentKey = _config.GetString(ConfigVariables.SERVER);
        if (!string.IsNullOrEmpty(currentKey))
            keyInput.SetText(currentKey);
        
        keyInput.CaretColor = Color.White;
        keyInput.BorderColor = Color.Transparent;
        keyInput.FocusedBorderColor = Color.Transparent;
        keyInput.TextChanged += (input, text) => _config.Set(ConfigVariables.SERVER, text);
        
        group.AddChild(headerText);
        group.AddChild(keyBackground);
        group.AddChild(keyInput);
        group.AddChild(keyClear);
    }

    private void DrawSync(DrawableGroup group)
    {
        var button = MewUIManager.Instance.CreateButtonFromResource(
            id: "config_tab_server_sync_button",
            resourceName: Assembly.GetExecutingAssembly().PathToResourceName("UI/Images/Sync.png"),
            layout: RelativeRect.FromReference(1330, 330, 54, 54)
        );
        
        var buttonText = MewUIManager.Instance.CreateText(
            id: "config_tab_server_sync_text",
            text: "Провести синхронизацию",
            layout: RelativeRect.FromReference(1400, 340, 440, 96),
            size: 32f,
            Color.Black,
            font: "opsilon"
        );
        
        group.AddChild(button);
        group.AddChild(buttonText);
    }
}