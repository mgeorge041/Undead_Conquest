using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceEventManager
{
    public virtual void Clear() 
    {
        onChangeHealth.Clear();
        onDeath.Clear();
        onCenterCameraOnPiece.Clear();
        onFinishAttackAnimation.Clear();
    }


    public GameEvent<int, int> onChangeHealth { get; private set; } = new GameEvent<int, int>();
    public GameEvent<Piece> onDeath { get; private set; } = new GameEvent<Piece>();
    public GameEvent<Piece, bool> onCenterCameraOnPiece { get; private set; } = new GameEvent<Piece, bool>();
    public GameEvent<Piece> onFinishAttackAnimation { get; private set; } = new GameEvent<Piece>();
}
