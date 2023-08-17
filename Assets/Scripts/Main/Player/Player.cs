using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Initialized
    public bool initialized { get; private set; }

    // Item manager
    public PlayerStartResources startResources;
    public PlayerStartPieces startPieces;
    public PlayerItemManager itemManager { get; private set; }

    // Turn phases
    public TurnPhaseHandler turnPhaseHandler { get; private set; }
    private Vector3 startActionPhaseCameraPosition;

    // UI
    public PlayerUI playerUI;

    // Input
    public PlayerInputController inputController;


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
        // Item manager
        itemManager = new PlayerItemManager();
        if (startResources != null)
            itemManager.resourceManager.AddResources(startResources.resources);

        // Input controller
        inputController.Initialize();

        // Turn phase handler
        turnPhaseHandler = new TurnPhaseHandler(itemManager);
        turnPhaseHandler.SubscribeInputControllerEvents(inputController);
        SubscribeTurnPhaseHandlerEvents();

        // UI
        playerUI.Initialize();
        playerUI.SetItemManagerInfo(itemManager);
        playerUI.SubscribeTurnPhaseHandlerEvents(turnPhaseHandler);
        SubscribePlayerUIEvents();

        // Item manager
        SubscribeItemManagerEvents();
        itemManager.SubscribePlayerUIEvents(playerUI);

        initialized = true;
    }


    // Subscribe to events
    private void SubscribeTurnPhaseHandlerEvents()
    {
        turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhase);
    }
    private void SubscribeItemManagerEvents()
    {
        itemManager.pieceManager.eventManager.onAddPiece.Subscribe(AddPiece);
        itemManager.pieceManager.eventManager.onRemovePiece.Subscribe(RemovePiece);
    }
    private void SubscribePlayerUIEvents()
    {
        playerUI.eventManager.onClickNextPhaseButton.Subscribe(ClickNextPhaseButton);
    }


    // Reset
    public void Reset()
    {
        itemManager.Reset();
        playerUI.Reset();
    }


    // Set hexmap data and starting pieces
    public void SetHexmapData(HexmapData hexmapData)
    {
        inputController.SetHexmapData(hexmapData);
        if (startPieces != null)
        {
            foreach (KeyValuePair<PlayableCardInfo, Vector3Int> pair in startPieces.pieces)
            {
                Piece piece = Piece.CreatePiece(pair.Key);
                hexmapData.AddPiece(piece, pair.Value);
                itemManager.pieceManager.AddPiece(piece);
            }
        }
    }


    // Add piece
    private void AddPiece(Piece piece)
    {
        inputController.playerCamera.SubscribePieceEvents(piece);
    }
    private void RemovePiece(Piece piece)
    {
        inputController.playerCamera.UnsubscribePieceEvents(piece);
    }


    // Handle setting next turn phase
    private void HandleSetNextPhase(TurnPhaseType nextPhase)
    {
        inputController.SetCurrentControllerSettings(turnPhaseHandler.GetPhaseControllerSettings(nextPhase));

        // Set camera start location
        if (nextPhase == TurnPhaseType.StartAction)
            startActionPhaseCameraPosition = inputController.playerCamera.transform.position;
        else if (nextPhase == TurnPhaseType.Draw)
            inputController.playerCamera.SetPosition(startActionPhaseCameraPosition);
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
