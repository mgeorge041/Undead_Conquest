using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceActionType
{
    Ability, 
    Attack,
    Move,
    None
}

public class PieceActionData
{
    // Piece info
    public Piece actionPiece { get; private set; }
    public Unit actionUnit => actionPiece.isUnit ? (Unit)actionPiece : null;
    public Building actionBuilding => actionPiece.isBuilding ? (Building)actionPiece : null;

    // Hex info
    public Hex startHex { get; private set; }
    public Hex targetHex { get; private set; }
    public Piece targetPiece { get; private set; }
    public Unit targetUnit => targetPiece.isUnit ? (Unit)targetPiece : null;
    public Building targetBuilding => targetPiece.isBuilding ? (Building)targetPiece : null;

    // Action piece change info
    public UnitData startUnitData { get; private set; }
    public UnitData endUnitData { get; private set; }
    public BuildingData startBuildingData { get; private set; }
    public BuildingData endBuildingData { get; private set; }

    // Target piece data change info
    public UnitData startTargetUnitData { get; private set; }
    public UnitData endTargetUnitData { get; private set; }
    public BuildingData startTargetBuildingData { get; private set; }
    public BuildingData endTargetBuildingData { get; private set; }

    // Action info
    public PieceActionType actionType { get; private set; }

    // Event manager
    public PieceActionDataEventManager eventManager { get; private set; } = new PieceActionDataEventManager();


    // Constructor
    public PieceActionData() { }
    public PieceActionData(Piece piece, PieceActionType actionType, Hex startHex, Hex targetHex)
    {
        if (piece == null || piece.hex == null)
            throw new System.ArgumentNullException("Cannot create action data for null piece.");

        if (actionType == PieceActionType.None)
            throw new System.ArgumentNullException("Cannot create action data for None type action.");

        if (startHex == null)
            throw new System.ArgumentNullException("Cannot create action data for null start hex.");

        if (targetHex == null)
            throw new System.ArgumentNullException("Cannot create action data for null target hex.");

        actionPiece = piece;
        this.startHex = startHex;
        this.targetHex = targetHex;
        targetPiece = targetHex.piece;
        this.actionType = actionType;

        if (actionPiece.isUnit)
        {
            startUnitData = new UnitData(actionUnit.unitData);
            actionUnit.unitEventManager.onFinishMoveAnimation.Subscribe(HandleFinishAction);
        }
        else if (actionPiece.isBuilding)
        {
            startBuildingData = new BuildingData(actionBuilding.buildingData);
        }

        actionUnit.eventManager.onFinishAttackAnimation.Subscribe(HandleFinishAction);
    }


    // Perform action
    public void PerformAction() 
    {
        switch (actionType)
        {
            case PieceActionType.Ability:
                return;

            case PieceActionType.Attack:
                if (targetPiece.isUnit) 
                {
                    startTargetUnitData = new UnitData(targetUnit.unitData);
                }
                else if (targetPiece.isBuilding)
                {
                    startTargetBuildingData = new BuildingData(targetBuilding.buildingData);
                }
                PerformAttack();
                return;

            case PieceActionType.Move:
                if (!actionPiece.isUnit)
                    throw new System.ArgumentException("Cannot move non-unit piece.");

                PerformMove();
                return;

            default:
                return;
        }
    }

    public void UndoAction()
    {
        switch (actionType)
        {
            case PieceActionType.Ability:
                return;

            case PieceActionType.Attack:
                UndoAttack();
                return;

            case PieceActionType.Move:
                UndoMove();
                return;

            default:
                return;
        }
    }


    // Perform move
    private void PerformMove()
    {
        actionUnit.MoveToHex(targetHex);
        endUnitData = new UnitData(actionUnit.unitData);
    }

    private void UndoMove()
    {
        actionUnit.SetHexAndPosition(startHex);
        startHex.SetPiece(actionUnit);
        targetHex.ClearPiece();
        actionUnit.unitData = new UnitData(startUnitData);
    }


    // Perform attack
    private void PerformAttack()
    {
        actionPiece.AttackHex(targetHex);
        
        // Set action piece data change
        if (actionPiece.isUnit)
        {
            endUnitData = new UnitData(actionUnit.unitData);
        }
        else if (actionPiece.isBuilding)
        {
            endBuildingData = new BuildingData(actionBuilding.buildingData);
        }

        // Set target data change
        if (targetPiece.isUnit)
        {
            endTargetUnitData = new UnitData(targetUnit.unitData);
        }
        else if (targetPiece.isBuilding)
        {
            endTargetBuildingData = new BuildingData(targetBuilding.buildingData);
        }
    }

    private void UndoAttack()
    {
        if (actionPiece.isUnit)
            actionUnit.unitData = new UnitData(startUnitData);
        else if (actionPiece.isBuilding)
            actionBuilding.buildingData = new BuildingData(startBuildingData);

        if (targetPiece.isUnit)
            targetUnit.unitData = new UnitData(startTargetUnitData);
        else if (targetPiece.isBuilding)
            targetBuilding.buildingData = new BuildingData(startTargetBuildingData);
    }


    // Handle finishing of action
    private void HandleFinishAction(Piece piece)
    {
        if (piece.isUnit)
        {
            Unit unit = (Unit)piece;
            unit.unitEventManager.onFinishMoveAnimation.Unsubscribe(HandleFinishAction);
        }
        piece.eventManager.onFinishAttackAnimation.Unsubscribe(HandleFinishAction);
            
        eventManager.onFinishPieceAction.OnEvent(this);
    }
}
