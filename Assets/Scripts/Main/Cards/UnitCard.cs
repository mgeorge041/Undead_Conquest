using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : PlayableCard
{
    // Card info
    public override CardInfo cardInfo => unitCardInfo;
    public override PlayableCardInfo playableCardInfo => unitCardInfo;
    public UnitCardInfo unitCardInfo { get; private set; }

    // Stats
    public int health => unitCardInfo.health;
    public int attack => unitCardInfo.attack;
    public int defense => unitCardInfo.defense;
    public int move => unitCardInfo.speed;
    public int mana => unitCardInfo.mana;
    public int range => unitCardInfo.range;

    // Card stats
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI attackLabel;
    public TextMeshProUGUI defenseLabel;
    public TextMeshProUGUI moveLabel;



    // Instantiate unit card
    public static UnitCard CreateUnitCard()
    {
        UnitCard card = Instantiate(Resources.Load<UnitCard>("Prefabs/Cards/Unit Card"));
        return card;
    }
    public static UnitCard CreateUnitCard(string cardPath)
    {
        UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(cardPath);
        return CreateUnitCard(unitCardInfo);
    }
    public static UnitCard CreateUnitCard(UnitCardInfo unitCardInfo)
    {
        UnitCard card = CreateUnitCard();
        card.SetInfo(unitCardInfo);
        return card;
    }


    // Set info
    public void SetInfo(UnitCardInfo cardInfo)
    {
        unitCardInfo = cardInfo;
        base.SetInfo(cardInfo);

        // Set stat info
        healthLabel.text = health.ToString();
        attackLabel.text = attack.ToString();
        defenseLabel.text = defense.ToString();
        moveLabel.text = move.ToString();
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
