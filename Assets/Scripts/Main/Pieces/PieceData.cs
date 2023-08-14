using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData
{
    // Card info
    public virtual PlayableCardInfo playableCardInfo { get; protected set; }

    // Player info
    public int playerId;

    // Stats
    protected Dictionary<PieceStatType, int> stats = new Dictionary<PieceStatType, int>();
    public int attack => GetStat(PieceStatType.Attack);
    public int defense => GetStat(PieceStatType.Defense);
    public int health => GetStat(PieceStatType.Health);
    public int currentHealth => GetStat(PieceStatType.CurrentHealth);
    public int mana => GetStat(PieceStatType.Mana);
    public int currentMana => GetStat(PieceStatType.CurrentMana);
    public int range => GetStat(PieceStatType.Range);

    // Actions
    public bool hasActions => canAttack;
    public bool canAttack { get; protected set; } = true;

    // Event manager
    public virtual PieceDataEventManager eventManager { get; protected set; } = new PieceDataEventManager();



    // Get or set stats
    public int GetStat(PieceStatType statType)
    {
        int value;
        if (stats.TryGetValue(statType, out value))
            return value;
        return 0;
    }
    public void SetStat(PieceStatType statType, int value)
    {
        stats[statType] = Mathf.Max(0, value);
    }


    // Increment stat
    public void AddStat(PieceStatType statType, int value)
    {
        int currentValue;
        int newValue;
        if (stats.TryGetValue(statType, out currentValue))
            newValue = currentValue + value;
        else
            newValue = value;

        newValue = Mathf.Max(newValue, 0);
        stats[statType] = newValue;
    }


    // Set has actions
    public virtual void SetHasActions(bool hasActions)
    {
        canAttack = hasActions;
        eventManager.onSetHasActions.OnEvent(hasActions);
    }
    public virtual void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
        eventManager.onSetCanAttack.OnEvent(canAttack);
    }
}
