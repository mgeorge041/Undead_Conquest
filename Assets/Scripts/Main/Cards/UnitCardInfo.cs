using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit Card Info", menuName = "Unit Card Info")]
public class UnitCardInfo : PlayableCardInfo
{
    // Base info
    public override CardType cardType => CardType.Unit;

    // Stats
    public int health;
    public int attack;
    public int defense;
    public int speed;
    public int mana;
    public int range;

    // Abilities
    // TODO
}
