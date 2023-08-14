using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingCard : PlayableCard
{
    // Card info
    public override CardInfo cardInfo => base.cardInfo;
    public override PlayableCardInfo playableCardInfo { get => base.playableCardInfo; protected set => base.playableCardInfo = value; }
    public BuildingCardInfo buildingCardInfo { get; private set; }

    // Stats
    public int health => buildingCardInfo.health;
    public int attack => buildingCardInfo.attack;
    public int defense => buildingCardInfo.defense;
    public int mana => buildingCardInfo.mana;
    public int range => buildingCardInfo.range;

    // Card stats
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI attackLabel;
    public TextMeshProUGUI defenseLabel;


    // Instantiate building card
    public static BuildingCard CreateBuildingCard()
    {
        BuildingCard card = Instantiate(Resources.Load<BuildingCard>("Prefabs/Cards/Building Card"));
        return card;
    }
    public static BuildingCard CreateBuildingCard(BuildingCardInfo buildingCardInfo)
    {
        BuildingCard card = CreateBuildingCard();
        card.SetInfo(buildingCardInfo);
        return card;
    }


    // Set info
    public void SetInfo(BuildingCardInfo cardInfo)
    {
        buildingCardInfo = cardInfo;
        base.SetInfo(cardInfo);

        // Set stat info
        healthLabel.text = health.ToString();
        attackLabel.text = attack.ToString();
        defenseLabel.text = defense.ToString();
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
