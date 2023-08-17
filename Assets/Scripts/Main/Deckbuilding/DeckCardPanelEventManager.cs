using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckCardPanelEventManager
{
    public void Clear()
    {
        onAddCard.Clear();
        onRemoveCard.Clear();
    }


    public GameEvent<CardInfo> onAddCard { get; private set; } = new GameEvent<CardInfo>();
    public GameEvent<CardInfo> onRemoveCard { get; private set; } = new GameEvent<CardInfo>();
}
