using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIEventManager
{
    public void Clear()
    {
        onClickNextPhaseButton.Clear();
        onFinishDrawAnimations.Clear();
        onFinishAddCardAnimations.Clear();
    }


    public GameEvent onClickNextPhaseButton { get; private set; } = new GameEvent();
    public GameEvent onFinishDrawAnimations { get; private set; } = new GameEvent();
    public GameEvent onFinishAddCardAnimations { get; private set; } = new GameEvent();
}
