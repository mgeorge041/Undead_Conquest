using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputControllerEventManager
{
    public void Clear() 
    {
        onLeftClick.Clear();
        onRightClick.Clear();
        onHover.Clear();
        onPressKeyboardArrows.Clear();
        onScroll.Clear();
    }


    public GameEvent<Vector3> onLeftClick { get; private set; } = new GameEvent<Vector3>();
    public GameEvent<Vector3> onRightClick { get; private set; } = new GameEvent<Vector3>();
    public GameEvent<Vector3> onHover { get; private set; } = new GameEvent<Vector3>();
    public GameEvent<float, float> onPressKeyboardArrows { get; private set; } = new GameEvent<float, float>();
    public GameEvent<float> onScroll { get; private set; } = new GameEvent<float>();
}
