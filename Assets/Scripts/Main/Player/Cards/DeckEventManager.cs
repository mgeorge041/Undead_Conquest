using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckEventManager
{
    public void Clear() 
    {
        onAddCard.Clear();
        onChangeDeck.Clear();
        onChangeDiscardPile.Clear();
        onChangePlayedPile.Clear();
    }


    public GameEvent<Card> onAddCard { get; private set; } = new GameEvent<Card>();
    public GameEvent<int, int> onChangeDeck { get; private set; } = new GameEvent<int, int>();
    public GameEvent<int> onChangeDiscardPile { get; private set; } = new GameEvent<int>();
    public GameEvent<int> onChangePlayedPile { get; private set; } = new GameEvent<int>();
}
