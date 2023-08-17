using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPieceManagerEventManager
{
    public void Clear()
    {
        onAddPiece.Clear();
        onRemovePiece.Clear();
        onSetSelectedPiece.Clear();
    }


    // Handling pieces
    public GameEvent<Piece> onAddPiece { get; private set; } = new GameEvent<Piece>();
    public GameEvent<Piece> onRemovePiece { get; private set; } = new GameEvent<Piece>();
    public GameEvent<Piece> onSetSelectedPiece { get; private set; } = new GameEvent<Piece>();
}
