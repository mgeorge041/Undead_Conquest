using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhaseEventManager
{
    public virtual void Clear() 
    {
        onEndPhase.Clear();
    }


    public GameEvent<TurnPhaseType> onEndPhase { get; private set; } = new GameEvent<TurnPhaseType>();
}
