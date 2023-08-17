using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexEventManager
{
    public void Clear() 
    {
        onAddPiece.Clear();
        onSetPiece.Clear();
        onSetTile.Clear();
    }


    public GameEvent<Piece> onAddPiece { get; private set; } = new GameEvent<Piece>();
    public GameEvent<Piece> onSetPiece { get; private set; } = new GameEvent<Piece>();
    public GameEvent<Hex> onSetTile { get; private set; } = new GameEvent<Hex>();
}
