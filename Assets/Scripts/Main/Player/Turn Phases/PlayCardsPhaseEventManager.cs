using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCardsPhaseEventManager : TurnPhaseEventManager
{
    public override void Clear()
    {
        base.Clear();
    }


    private event Action<Piece, Hex> onPlayPiece;
    public void OnPlayPiece(Piece card, Hex playHex) => onPlayPiece?.Invoke(card, playHex);
    public void SubscribePlayPiece(Action<Piece, Hex> action) => onPlayPiece += action;
    public void UnsubscribePlayPiece(Action<Piece, Hex> action) => onPlayPiece -= action;
}
