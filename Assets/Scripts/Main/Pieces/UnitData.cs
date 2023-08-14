using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : PieceData
{
    // Card info
    public UnitCardInfo unitCardInfo { get; private set; }

    // Stats
    public int speed => GetStat(PieceStatType.Speed);
    public int currentSpeed => GetStat(PieceStatType.CurrentSpeed);


    // Constructor
    public UnitData() { }
    public UnitData(UnitCardInfo unitCardInfo)
    {
        SetInfo(unitCardInfo);
    }
    public UnitData(UnitData copyData)
    {
        SetInfo(copyData.unitCardInfo);
        stats = new Dictionary<PieceStatType, int>(copyData.stats);
        SetCanAttack(copyData.canAttack);
    }


    // Set unit card info
    public void SetInfo(UnitCardInfo unitCardInfo)
    {
        this.unitCardInfo = unitCardInfo;
        stats[PieceStatType.Attack] = unitCardInfo.attack;
        stats[PieceStatType.Defense] = unitCardInfo.defense;
        stats[PieceStatType.Health] = unitCardInfo.health;
        stats[PieceStatType.CurrentHealth] = unitCardInfo.health;
        stats[PieceStatType.Mana] = unitCardInfo.mana;
        stats[PieceStatType.Speed] = unitCardInfo.speed;
        stats[PieceStatType.CurrentSpeed] = unitCardInfo.speed;
        stats[PieceStatType.Range] = unitCardInfo.range;
    }


    // Set has actions
    public override void SetHasActions(bool hasActions)
    {
        if (!hasActions)
            SetStat(PieceStatType.CurrentSpeed, 0);
        base.SetHasActions(hasActions);
    }
    public override void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
        if (!canAttack)
            SetStat(PieceStatType.CurrentSpeed, 0);
        eventManager.onSetCanAttack.OnEvent(canAttack);
    }
}
