using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActionEventManager
{
    public void Clear()
    {
        onFinishStartAction.Clear();
    }


    public GameEvent<StartAction> onFinishStartAction { get; private set; } = new GameEvent<StartAction>();
}
