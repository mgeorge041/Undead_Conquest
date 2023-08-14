using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Card Info", menuName = "Building Card Info")]
public class BuildingCardInfo : PlayableCardInfo
{
    // Base info
    public override CardType cardType => CardType.Building;

    // Stats
    public int health;
    public int attack;
    public int defense;
    public int mana;
    public int range;
    public int domainRange;

    // Abilities
    // TODO
}
