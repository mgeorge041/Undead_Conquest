using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : PieceData
{
    // Building info
    public BuildingCardInfo buildingCardInfo { get; private set; }

    // Stats
    public int domainRange;

    // Constructor
    public BuildingData() { }
    public BuildingData(BuildingCardInfo buildingCardInfo)
    {
        SetInfo(buildingCardInfo);
    }
    public BuildingData(BuildingData copyData)
    {
        SetInfo(copyData.buildingCardInfo);
        stats = new Dictionary<PieceStatType, int>(copyData.stats);
        SetCanAttack(copyData.canAttack);
    }


    // Set unit card info
    public void SetInfo(BuildingCardInfo buildingCardInfo)
    {
        this.buildingCardInfo = buildingCardInfo;
        stats[PieceStatType.Attack] = buildingCardInfo.attack;
        stats[PieceStatType.Defense] = buildingCardInfo.defense;
        stats[PieceStatType.Health] = buildingCardInfo.health;
        stats[PieceStatType.CurrentHealth] = buildingCardInfo.health;
        stats[PieceStatType.Mana] = buildingCardInfo.mana;
        stats[PieceStatType.Range] = buildingCardInfo.range;
        domainRange = buildingCardInfo.domainRange;
    }
}
