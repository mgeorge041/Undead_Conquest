using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInputController : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Input controller settings
    public PhaseInputControllerSettings currentControllerSettings { get; private set; }
    public PlayerCamera playerCamera;

    // Hexmap
    public Hexmap hexmap;

    // Event manager
    public PlayerInputControllerEventManager eventManager { get; private set; } = new PlayerInputControllerEventManager();
    public PhaseInputControllerEventManager settingsEventManager => currentControllerSettings.eventManager;


    // Instantiate
    public static PlayerInputController CreatePlayerInputController()
    {
        PlayerInputController inputController = Instantiate(Resources.Load<PlayerInputController>("Prefabs/Player/Player Input Controller"));
        inputController.Initialize();
        return inputController;
    }


    // Initialize
    public void Initialize()
    {
        SetCurrentControllerSettings(new EmptyPhaseControllerSettings());
        hexmap.Initialize();
        playerCamera.Initialize();
        initialized = true;
    }


    // Set current input controller settings
    public void SetCurrentControllerSettings(PhaseInputControllerSettings controllerSettings)
    {
        if (controllerSettings == null)
            throw new System.ArgumentNullException("Cannot set input controller settings to null.");

        UnsubscribeInputControllerEvents();
        currentControllerSettings = controllerSettings;
        SubscribeInputControllerEvents();
    }


    // Subscribe to input controller events
    private void SubscribeInputControllerEvents()
    {
        settingsEventManager.onLeftClick.Subscribe(LeftClick);
        settingsEventManager.onRightClick.Subscribe(RightClick);
        settingsEventManager.onHover.Subscribe(Hover);
        settingsEventManager.onPressKeyboardArrows.Subscribe(PressKeyboardArrows);
        settingsEventManager.onScroll.Subscribe(Scroll);
    }
    private void UnsubscribeInputControllerEvents()
    {
        if (currentControllerSettings == null)
            return;

        settingsEventManager.onLeftClick.Unsubscribe(LeftClick);
        settingsEventManager.onRightClick.Unsubscribe(RightClick);
        settingsEventManager.onHover.Unsubscribe(Hover);
        settingsEventManager.onPressKeyboardArrows.Unsubscribe(PressKeyboardArrows);
        settingsEventManager.onScroll.Unsubscribe(Scroll);
    }


    // Set hexmap data
    public void SetHexmapData(HexmapData hexmapData)
    {
        hexmap.SetHexmapData(hexmapData);
        playerCamera.SetMapPattern(hexmapData.mapPattern);
    }


    // Clicks
    public void LeftClick(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex clickHex = hexmap.GetHexAtWorldPosition(worldPosition);
        eventManager.onLeftClick.OnEvent(clickHex);
    }
    public void RightClick(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex clickHex = hexmap.GetHexAtWorldPosition(worldPosition);
        eventManager.onRightClick.OnEvent(clickHex);
    }


    // Hover
    public void Hover(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex hoverHex = hexmap.GetHexAtWorldPosition(worldPosition);
        eventManager.onHover.OnEvent(hoverHex);
    }


    // Press keyboard arrows
    public void PressKeyboardArrows(float moveX, float moveY)
    {
        playerCamera.MoveCamera(moveX, moveY);
    }


    // Scroll mousewheel
    public void Scroll(float moveZ)
    {
        playerCamera.ZoomCamera(moveZ);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentControllerSettings.Update();
    }
}
