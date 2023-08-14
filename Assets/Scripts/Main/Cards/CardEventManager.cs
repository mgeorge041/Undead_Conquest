using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEventManager
{
    public void Clear() 
    {
        onStartHover.Clear();
        onEndHover.Clear();
        onLeftClick.Clear();
        onFinishRotate.Clear();
    }


    public GameEvent<Card> onStartHover { get; private set; } = new GameEvent<Card>();
    public GameEvent<Card> onEndHover { get; private set; } = new GameEvent<Card>();
    public GameEvent<Card> onLeftClick { get; private set; } = new GameEvent<Card>();
    public GameEvent<Card> onFinishRotate { get; private set; } = new GameEvent<Card>();
}
