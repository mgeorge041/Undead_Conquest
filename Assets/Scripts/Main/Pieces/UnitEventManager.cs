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
        onFinishMove.Clear();
        onFinishMoveAnimation.Clear();
    }


    public GameEvent<int, int> onChangeSpeed { get; private set; } = new GameEvent<int, int>();
    public GameEvent<Unit> onFinishMove { get; private set; } = new GameEvent<Unit>();
    public GameEvent<Unit> onFinishMoveAnimation { get; private set; } = new GameEvent<Unit>();
}
