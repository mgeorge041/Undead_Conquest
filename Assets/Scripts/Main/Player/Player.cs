using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialized
    public bool initialized { get; private set; }

    // Item manager
    public PlayerItemManager itemManager { get; private set; } = new PlayerItemManager();

    // UI
    public PlayerUI playerUI;

    // Input
    public PlayerCamera playerCamera;
    public PlayerInputController inputController;
    public TurnPhaseHandler turnPhaseHandler = new TurnPhaseHandler();

    // Hexmap
    public Hexmap hexmap;


    // Instantiate player
    public static Player CreatePlayer()
    {
        Player player = Instantiate(Resources.Load<Player>("Prefabs/Player/Player"));
        player.Initialize();
        return player;
    }


    // Initialize
    public void Initialize()
    {
        // Input controller
        SubscribeInputControllerEvents();

        // Turn phase handler
        turnPhaseHandler = new TurnPhaseHandler(itemManager);
        
        // UI
        playerUI.Initialize();
        playerUI.SubscribePlayerItemEvents(itemManager);
        playerUI.SubscribeTurnPhaseHandlerEvents(turnPhaseHandler);
        SubscribePlayerUIEvents();

        // Item manager
        SubscribeItemManagerEvents();
        itemManager.SubscribePlayerUIEvents(playerUI);

        // Hexmap
        hexmap.Initialize();

        initialized = true;
    }


    // Subscribe to events
    private void SubscribeItemManagerEvents()
    {
        itemManager.eventManager.onFinishDrawingCards.Subscribe(FinishDrawingCards);
        itemManager.eventManager.onAddPiece.Subscribe(AddPiece);
        itemManager.eventManager.onRemovePiece.Subscribe(RemovePiece);
    }
    private void SubscribePlayerUIEvents()
    {
        playerUI.eventManager.onClickNextPhaseButton.Subscribe(ClickNextPhaseButton);
    }
    private void SubscribeInputControllerEvents()
    {
        inputController.eventManager.onLeftClick.Subscribe(LeftClick);
        inputController.eventManager.onRightClick.Subscribe(RightClick);
        inputController.eventManager.onHover.Subscribe(Hover);
        inputController.eventManager.onPressKeyboardArrows.Subscribe(PressKeyboardArrows);
        inputController.eventManager.onScroll.Subscribe(Scroll);
    }


    // Reset
    public void Reset()
    {
        itemManager.Reset();
        playerUI.Reset();
    }


    // Set hexmap data
    public void SetHexmapData(HexmapData hexmapData)
    {
        hexmap.SetHexmapData(hexmapData);
        playerCamera.SetMapPattern(hexmapData.mapPattern);
    }


    // Finish drawing cards
    private void FinishDrawingCards(Queue<Tuple<Card, int, int, int>> drawQueue)
    {
        if (turnPhaseHandler.currentPhase.phaseType != TurnPhaseType.Draw)
            return;

        turnPhaseHandler.StartNextPhase();
    }


    // Add piece
    private void AddPiece(Piece piece)
    {
        playerCamera.SubscribePieceEvents(piece);
    }
    private void RemovePiece(Piece piece)
    {
        playerCamera.UnsubscribePieceEvents(piece);
    }


    // Clicks
    public void LeftClick(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex clickHex = hexmap.GetHexAtWorldPosition(worldPosition);
        turnPhaseHandler.LeftClick(clickHex);
    }
    public void RightClick(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex clickHex = hexmap.GetHexAtWorldPosition(worldPosition);
        turnPhaseHandler.RightClick(clickHex);
    }


    // Hover
    public void Hover(Vector3 mousePosition)
    {
        Vector3 worldPosition = playerCamera.cameraObject.ScreenToWorldPoint(mousePosition);
        Hex hoverHex = hexmap.GetHexAtWorldPosition(worldPosition);
        turnPhaseHandler.Hover(hoverHex);
    }


    // Press keyboard arrows
    public void PressKeyboardArrows(float moveX, float moveY)
    {
        Vector3 newPosition = new Vector3(playerCamera.transform.position.x + moveX, playerCamera.transform.position.y + moveY);
        playerCamera.MoveCamera(newPosition);
    }


    // Scroll mousewheel
    public void Scroll(float moveZ)
    {
        playerCamera.ZoomCamera(moveZ);
    }


    // Click next phase button
    public void ClickNextPhaseButton()
    {
        turnPhaseHandler.StartNextPhase();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
