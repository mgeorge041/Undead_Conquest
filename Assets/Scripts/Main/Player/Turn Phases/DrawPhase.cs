using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPhase : TurnPhase
{
    public override TurnPhaseType phaseType => TurnPhaseType.Draw;
    public override TurnPhaseType nextPhaseType => TurnPhaseType.PlayCards;


    // Constructor
    public DrawPhase() { }
    public DrawPhase(PlayerItemManager itemManager)
    {
        this.itemManager = itemManager;
    }


    // Start phase
    public override void StartPhase()
    {
        Debug.Log("Starting draw phase.");
        itemManager.DrawCard();
    }
}
