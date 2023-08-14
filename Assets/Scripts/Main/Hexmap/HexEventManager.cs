using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEventManager
{
    public void Clear() 
    {
        onAddPiece.Clear();
    }


    public GameEvent<Piece> onAddPiece { get; private set; } = new GameEvent<Piece>();
}
