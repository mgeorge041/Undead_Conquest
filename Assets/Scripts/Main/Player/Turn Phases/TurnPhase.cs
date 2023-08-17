using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnPhaseType
{
    Draw,
    StartAction,
    PlayCards,
    MapAction,
    EndTurn,
    None
}


public abstract class TurnPhase
{
    // Phase info
    public abstract TurnPhaseType phaseType { get; }
    public abstract TurnPhaseType nextPhaseType { get; }

    // Player items
    protected PlayerItemManager itemManager;

    // Phase actions
    public virtual void StartPhase() { }
    public virtual void EndPhase()
    {
        ClearPhase();
        eventManager.onEndPhase.OnEvent(nextPhaseType);
    }
    public virtual void EndPhase(bool endOnly)
    {
        ClearPhase();
        if (!endOnly)
            eventManager.onEndPhase.OnEvent(nextPhaseType);
    }
    public virtual void ClearPhase() { }

    // Player input
    public virtual void LeftClick(Hex clickHex) { }
    public virtual void RightClick(Hex clickHex) { }
    public virtual void Hover(Hex hoverHex) { }

    // Event manager
    public virtual TurnPhaseEventManager eventManager { get; protected set; } = new TurnPhaseEventManager();
}
