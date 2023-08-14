using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEventManager
{
    public void Clear() 
    {
        onChangeHand.Clear();
        onLeftClickCard.Clear();
    }

    public GameEvent<int> onChangeHand { get; private set; } = new GameEvent<int>();
    public GameEvent<PlayableCard> onLeftClickCard { get; private set; } = new GameEvent<PlayableCard>();
}
