using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Piece
{
    // Piece info
    public override PieceType pieceType => PieceType.Building;
    public override PlayableCardInfo playableCardInfo => buildingCardInfo;
    public BuildingCardInfo buildingCardInfo { get; protected set; }
    public override PieceData pieceData => buildingData;
    public BuildingData buildingData = new BuildingData();

    // Sprite
    public override PieceAnimator pieceAnimator => buildingAnimator;
    public BuildingAnimator buildingAnimator;
    public Sprite sprite;

    // Event manager
    public override PieceEventManager eventManager => buildingEventManager;
    public BuildingEventManager buildingEventManager { get; private set; } = new BuildingEventManager();


    // Instantiate building
    public static Building CreateBuilding()
    {
        Building building = Instantiate(Resources.Load<Building>("Prefabs/Pieces/Building"));
        building.Initialize();
        return building;
    }
    public static Building CreateBuilding(string cardPath)
    {
        BuildingCardInfo buildingCardInfo = CardInfo.LoadCardInfo<BuildingCardInfo>(cardPath);
        return CreateBuilding(buildingCardInfo);
    }
    public static Building CreateBuilding(BuildingCardInfo buildingCardInfo)
    {
        Building building = CreateBuilding();
        building.SetInfo(buildingCardInfo);
        return building;
    }


    // Set info
    public void SetInfo(BuildingCardInfo cardInfo)
    {
        buildingCardInfo = cardInfo;
        sprite = cardInfo.cardSprite;
        buildingData.SetInfo(buildingCardInfo);
        buildingAnimator.spriteRenderer.sprite = cardInfo.cardSprite;
        CreateStartAction(cardInfo.startActionInfo);
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
