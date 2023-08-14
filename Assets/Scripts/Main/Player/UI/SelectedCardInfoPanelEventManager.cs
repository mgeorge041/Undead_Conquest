using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCardInfoPanelEventManager
{
    public void Clear() 
    {
        onClickShowHandButton.Clear();
    }


    public GameEvent<bool> onClickShowHandButton { get; private set; } = new GameEvent<bool>();
}
