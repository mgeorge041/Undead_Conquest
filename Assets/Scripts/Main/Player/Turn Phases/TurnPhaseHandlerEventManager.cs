using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhaseHandlerEventManager
{
    public void Clear() 
    {
        onSetNextPhase.Clear();
    }


    public GameEvent<TurnPhaseType> onSetNextPhase { get; private set; } = new GameEvent<TurnPhaseType>();
}
