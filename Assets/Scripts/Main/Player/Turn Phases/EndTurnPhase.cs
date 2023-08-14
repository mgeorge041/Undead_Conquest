using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPhase : TurnPhase
{
    public override TurnPhaseType phaseType => TurnPhaseType.EndTurn;
    public override TurnPhaseType nextPhaseType => TurnPhaseType.StartAction;

    // Constructor
    public EndTurnPhase() { }
}
