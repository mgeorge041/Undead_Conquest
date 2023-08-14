using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Unit : Piece
{
    // Piece info
    public override PieceType pieceType => PieceType.Unit;
    public override PlayableCardInfo playableCardInfo => unitCardInfo;
    public UnitCardInfo unitCardInfo { get; protected set; }
    public override PieceData pieceData => unitData;
    public UnitData unitData = new UnitData();

    // Animator
    public override PieceAnimator pieceAnimator => unitAnimator;
    public UnitAnimator unitAnimator;
    public Sprite sprite;

    // Event manager
    public override PieceEventManager eventManager => unitEventManager;
    public UnitEventManager unitEventManager { get; private set; } = new UnitEventManager();


    // Instantiate unit
    public static Unit CreateUnit()
    {
        Unit unit = Instantiate(Resources.Load<Unit>("Prefabs/Pieces/Unit"));
        unit.Initialize();
        return unit;
    }
    public static Unit CreateUnit(string cardPath)
    {
        UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(cardPath);
        return CreateUnit(unitCardInfo);
    }
    public static Unit CreateUnit(UnitCardInfo unitCardInfo)
    {
        Unit unit = CreateUnit();
        unit.SetInfo(unitCardInfo);
        return unit;
    }


    // Reset unit
    public void Reset()
    {
        SetInfo(unitCardInfo);
    }


    // Set piece info
    public void SetInfo(UnitCardInfo cardInfo)
    {
        unitCardInfo = cardInfo;
        sprite = cardInfo.cardSprite;
        unitData.SetInfo(unitCardInfo);
        unitAnimator.spriteRenderer.sprite = cardInfo.cardSprite;
        CreateStartAction(cardInfo.startActionInfo);
    }


    // End turn
    public override void EndTurn()
    {
        unitData.SetStat(PieceStatType.CurrentSpeed, unitData.speed);
        unitData.SetHasActions(true);
    }


    // Move unit to hex
    public void MoveToHex(Hex targetHex)
    {
        if (targetHex == null)
            throw new System.ArgumentNullException("Cannot move to null hex.");

        if (targetHex.hasPiece)
            throw new System.ArgumentException("Cannot move to hex that already has a piece.");

        // Decrement move remaining
        int moveDist = Direction.GetDistanceHexes(hex, targetHex);
        unitData.AddStat(PieceStatType.CurrentSpeed, -moveDist);

        // Update hex information
        List<Hex> hexPath = HexPathfinding.GetPath(hex.pathNode, targetHex.pathNode);
        List<Vector3> movePath = CreateMovePath(hexPath);

        hex.ClearPiece();
        targetHex.SetPiece(this);
        SetHex(targetHex);

        unitAnimator.MoveUnit(movePath);
        unitEventManager.onChangeSpeed.OnEvent(unitData.currentSpeed, unitData.speed);
    }


    // Create move path
    public List<Vector3> CreateMovePath(List<Hex> hexPath)
    {
        List<Vector3> movePath = new List<Vector3>();
        foreach (Hex hex in hexPath)
        {
            movePath.Add(hex.pathNode.worldPosition);
        }
        return movePath;
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
