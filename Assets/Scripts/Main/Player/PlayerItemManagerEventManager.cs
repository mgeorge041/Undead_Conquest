using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemManagerEventManager
{
    public void Clear()
    {
        onChangeResource.Clear();
        onLeftClickCard.Clear();
        onFinishDrawingCards.Clear();
        onFinishAnimatingDrawCards.Clear();
        onPlayCard.Clear();
        onFinishAddingCards.Clear();
        onFinishAnimatingAddCards.Clear();
    }


    public GameEvent<ResourceType, int> onChangeResource { get; private set; } = new GameEvent<ResourceType, int>();
    public GameEvent<PlayableCard> onLeftClickCard { get; private set; } = new GameEvent<PlayableCard>();

    // Drawing cards
    public GameEvent<Queue<Tuple<Card, int, int, int>>> onFinishDrawingCards { get; private set; } = new GameEvent<Queue<Tuple<Card, int, int, int>>>();
    public GameEvent onFinishAnimatingDrawCards { get; private set; } = new GameEvent();

    // Playing cards
    public GameEvent<PlayableCard, PlayerResourceManager> onPlayCard { get; private set; } = new GameEvent<PlayableCard, PlayerResourceManager>();

    // Adding cards to deck
    public GameEvent<Queue<Tuple<Card, int, int>>> onFinishAddingCards { get; private set; } = new GameEvent<Queue<Tuple<Card, int, int>>>();
    public GameEvent onFinishAnimatingAddCards { get; private set; } = new GameEvent();
}
