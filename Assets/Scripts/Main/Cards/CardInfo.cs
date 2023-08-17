using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Building,
    Resource,
    Spell,
    Unit,
    None
}


public class CardInfo : ScriptableObject
{
    public Sprite cardSprite;
    public string cardName;
    [Multiline(4)] public string cardDesc;
    [field: SerializeField] public virtual CardType cardType { get; protected set; }

    // Card types
    public bool isPlayableType => isBuildingType || isUnitType;
    public bool isBuildingType => cardType == CardType.Building;
    public bool isUnitType => cardType == CardType.Unit;
    public bool isResourceType => cardType == CardType.Resource;

    // Card casts
    public PlayableCardInfo playableCardInfo => isPlayableType ? (PlayableCardInfo)this : null;
    public BuildingCardInfo buildingCardInfo => isBuildingType ? (BuildingCardInfo)this : null;
    public UnitCardInfo unitCardInfo => isUnitType ? (UnitCardInfo)this : null;
    public ResourceCardInfo resourceCardInfo => isResourceType ? (ResourceCardInfo)this : null;


    // Load card info
    public static CardInfo LoadCardInfo(string cardPath)
    {
        return Instantiate(Resources.Load<CardInfo>(cardPath));
    }
    public static T LoadCardInfo<T>(string cardPath) where T : CardInfo
    {
        return Instantiate(Resources.Load<T>(cardPath));
    }
}
