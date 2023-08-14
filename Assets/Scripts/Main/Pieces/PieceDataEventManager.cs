using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceDataEventManager
{
    public void Clear() 
    {
        onSetHasActions.Clear();
        onSetCanAttack.Clear();
    }


    public GameEvent<bool> onSetHasActions { get; private set; } = new GameEvent<bool>();
    public GameEvent<bool> onSetCanAttack{ get; private set; } = new GameEvent<bool>();
}
