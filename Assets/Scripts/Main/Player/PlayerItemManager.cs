using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerItemManager
{
    // Player info
    public int playerId = 1;

    // Card items
    public Deck deck { get; private set; } = new Deck();
    public Hand hand { get; private set; } = new Hand();
    
    private Queue drawCardActionQueue = new Queue();
    private Queue<Tuple<Card, int, int, int>> drawCardQueue = new Queue<Tuple<Card, int, int, int>>();
    public Queue<Card> addCardQueue = new Queue<Card>();
    private Queue<Tuple<Card, int, int>> finishAddCardQueue = new Queue<Tuple<Card, int,int>>();

    // Resources
    public PlayerResourceManager resourceManager { get; private set; } = new PlayerResourceManager();

    // Pieces
    public PlayerPieceManager pieceManager { get; private set; } = new PlayerPieceManager();

    // Event manager
    public PlayerItemManagerEventManager eventManager { get; private set; } = new PlayerItemManagerEventManager();


    // Constructor
    public PlayerItemManager() 
    {
        SubscribeToEvents();
    }
    public PlayerItemManager(int playerId)
    {
        this.playerId = playerId;
    }


    // Subscribe to events
    private void SubscribeToEvents()
    {
        hand.eventManager.onLeftClickCard.Subscribe(HandleLeftClickCard);
    }
    public void SubscribePlayerUIEvents(PlayerUI playerUI)
    {
        playerUI.eventManager.onFinishDrawAnimations.Subscribe(FinishDrawingCardAnimations);
        playerUI.eventManager.onFinishAddCardAnimations.Subscribe(FinishAddingCardAnimations);
    }


    // Reset
    public void Reset()
    {
        deck.Reset();
        hand.Reset();
        resourceManager.Reset();
    }


    // End turn
    public void EndTurn()
    {
        pieceManager.EndTurn();
    }


    // Update resources and UI
    public void AddResource(ResourceType resource, int amount)
    {
        resourceManager.AddResource(resource, amount);
        CheckPlayableCards();
    }
    public void ShowAddResourceUI(ResourceType resource)
    {
        eventManager.onChangeResource.OnEvent(resource, resourceManager.GetResource(resource));
    }


    // Add draw action to queue
    public void DrawCard()
    {
        AddDrawCardAction();
        DrawNextCard();
    }
    public void AddDrawCardAction()
    {
        drawCardActionQueue.Enqueue(1);
    }


    // Draw a card from the deck
    public void DrawNextCard()
    {
        // No more cards to draw
        if (drawCardActionQueue.Count <= 0)
        {
            FinishDrawingCards();
            return;
        }
        else
        {
            drawCardActionQueue.Dequeue();
        }

        Card card = deck.DrawCard();

        // Empty deck check
        if (card == null)
        {
            deck.ShuffleDiscardIntoDeck();
            if (deck.numDeckCards == 0)
                FinishDrawingCards();
            else
                DrawCard();
            return;
        }

        // Add resources
        if (card.cardType == CardType.Resource)
        {
            ResourceCard resourceCard = (ResourceCard)card;
            foreach (KeyValuePair<ResourceType, int> pair in resourceCard.resources)
            {
                if (pair.Key != ResourceType.None)
                    AddResource(pair.Key, pair.Value);
            }
            deck.AddCardToDiscard(card);
        }
        else
        {
            hand.AddCard(card);
        }

        // Add card to hand
        drawCardQueue.Enqueue(new Tuple<Card, int, int, int>(card, deck.numDeckCards, deck.numTotalCards, hand.numCards));
        CheckPlayableCards();
        DrawNextCard();
    }


    // Finish drawing cards
    private void FinishDrawingCards()
    {
        Queue<Tuple<Card, int, int, int>> copyDrawCardQueue = new Queue<Tuple<Card, int, int, int>>(drawCardQueue);
        eventManager.onFinishDrawingCards.OnEvent(copyDrawCardQueue);
        drawCardQueue.Clear();
    }


    // Finish drawing card animations
    private void FinishDrawingCardAnimations()
    {
        eventManager.onFinishAnimatingDrawCards.OnEvent();
    }


    // Add add card action to queue



    // Add card to deck
    public void AddNextCard()
    {
        if (addCardQueue.Count <= 0)
        {
            eventManager.onFinishAddingCards.OnEvent(finishAddCardQueue);
            finishAddCardQueue.Clear();
            return;
        }

        Card addCard = addCardQueue.Dequeue();
        Tuple<Card, int, int> addedInfo = AddCardToDeck(addCard);
        finishAddCardQueue.Enqueue(addedInfo);
        AddNextCard();
    }
    private Tuple<Card, int, int> AddCardToDeck(Card card)
    {
        deck.AddNewCardToDiscard(card);
        return new Tuple<Card, int, int>(card, deck.numDiscardCards, deck.numTotalCards);
    }


    // Finish adding card animations
    private void FinishAddingCardAnimations()
    {
        eventManager.onFinishAnimatingAddCards.OnEvent();
    }


    // Play card
    public void PlayCard(PlayableCard card)
    {
        if (card == null)
            return;

        hand.PlayCard(card);
        resourceManager.PlayCard(card);
        deck.AddCardToDiscard(card);
        card.gameObject.SetActive(false);
        CheckPlayableCards();
        eventManager.onPlayCard.OnEvent(card, resourceManager);
    }


    // Check whether cards in hand are playable
    private void CheckPlayableCards()
    {
        foreach (Card card in hand.cards)
        {
            if (resourceManager.CanPlayCard(card))
                card.SetPlayable(true);
            else
                card.SetPlayable(false);
        }
    }


    // Card clicks
    public void HandleLeftClickCard(PlayableCard card)
    {
        if (!resourceManager.CanPlayCard(card))
        {
            eventManager.onLeftClickCard.OnEvent(null);
        }
        else
        {
            eventManager.onLeftClickCard.OnEvent(card);
        }
    }
}
