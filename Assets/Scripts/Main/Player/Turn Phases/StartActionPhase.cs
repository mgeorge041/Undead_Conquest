using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActionPhase : TurnPhase
{
    public override TurnPhaseType phaseType => TurnPhaseType.StartAction;
    public override TurnPhaseType nextPhaseType => TurnPhaseType.Draw;
    private Queue<Piece> startActionPieceQueue = new Queue<Piece>();


    // Constructor
    public StartActionPhase() { }
    public StartActionPhase(PlayerItemManager itemManager)
    {
        this.itemManager = itemManager;
    }


    // Start phase
    public override void StartPhase()
    {
        SetStartActions();
        PerformNextStartAction(null);
    }


    // Perform each piece start action
    private void SetStartActions()
    {
        foreach (Piece piece in itemManager.pieceManager.pieces)
        {
            if (!piece.hasStartAction)
                continue;

            startActionPieceQueue.Enqueue(piece);
        }
    }


    // Perform next start action
    public void PerformNextStartAction(StartAction startAction)
    {
        // Unsubscribe from previous action
        if (startAction != null)
            startAction.eventManager.onFinishStartAction.Unsubscribe(PerformNextStartAction);

        if (startActionPieceQueue.Count <= 0)
        {
            EndPhase();
            return;
        }

        Piece actionPiece = startActionPieceQueue.Dequeue();
        StartAction nextAction = actionPiece.startAction;
        nextAction.eventManager.onFinishStartAction.Subscribe(PerformNextStartAction);
        actionPiece.eventManager.onCenterCameraOnPiece.OnEvent(actionPiece, true);
        nextAction.PerformStartAction(actionPiece, itemManager);
    }
}
