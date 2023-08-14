using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceEventManager
{
    public virtual void Clear() 
    {
        onChangeHealth.Clear();
        onCenterCameraOnPiece.Clear();
    }


    public GameEvent<int, int> onChangeHealth { get; private set; } = new GameEvent<int, int>();
    public GameEvent<Piece, bool> onCenterCameraOnPiece { get; private set; } = new GameEvent<Piece, bool>();
}
