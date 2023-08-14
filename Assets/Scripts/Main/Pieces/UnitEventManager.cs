using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEventManager : PieceEventManager
{
    public override void Clear() 
    {
        base.Clear();
        onChangeSpeed.Clear();
    }


    public GameEvent<int, int> onChangeSpeed { get; private set; } = new GameEvent<int, int>();
}
