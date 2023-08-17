using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of cards in the player's hand.
/// </summary>
public class Hand
{
    // Cards
    public int numCards => cards.Count;
    public List<Card> cards { get; private set; } = new List<Card>();
    public Card selectedCard { get; private set; }
    public Vector3 hoverOffset => new Vector3(0, 36);
    public bool disableCardActions { get; private set; }

    // Event manager
    public HandEventManager eventManager { get; private set; } = new HandEventManager();


    // Constructor
    public Hand() { }


    // Reset
    public void Reset()
    {
        cards.Clear();
    }


    // Add or remove card
    public void AddCard(Card card)
    {
        if (card == null)
            return;

        cards.Add(card);
        eventManager.onChangeHand.OnEvent(numCards);
        SubscribeCardEvents(card);
    }
    public void RemoveCard(Card card)
    {
        cards.Remove(card);
        eventManager.onChangeHand.OnEvent(numCards);
        UnsubscribeCardEvents(card);
    }


    // Subscribe to events
    private void SubscribeCardEvents(Card card)
    {
        card.eventManager.onStartHover.Subscribe(HandleCardStartHover);
        card.eventManager.onEndHover.Subscribe(HandleCardEndHover);
        card.eventManager.onLeftClick.Subscribe(HandleCardLeftClick);
    }
    private void UnsubscribeCardEvents(Card card)
    {
        card.eventManager.onStartHover.Unsubscribe(HandleCardStartHover);
        card.eventManager.onEndHover.Unsubscribe(HandleCardEndHover);
        card.eventManager.onLeftClick.Unsubscribe(HandleCardLeftClick);
    }


    // Disable card actions
    public void DisableCardActions(bool disableCardActions)
    {
        this.disableCardActions = disableCardActions;
    }


    // Handle card hover
    public void HandleCardStartHover(Card card)
    {
        if (disableCardActions)
            return;

        if (card != selectedCard && !card.handHover)
            SetCardHover(card, true);
    }
    public void HandleCardEndHover(Card card)
    {
        if (disableCardActions)
            return;

        if (card != selectedCard && card.handHover)
            SetCardHover(card, false);
    }
    private void SetCardHover(Card card, bool hover)
    {
        if (hover)
            card.transform.position += hoverOffset;
        else
            card.transform.position -= hoverOffset;
        card.SetHandHover(hover);
    }


    // Handle card click
    public void HandleCardLeftClick(Card card)
    {
        if (disableCardActions)
            return;

        SetSelectedCard(card);
        eventManager.onLeftClickCard.OnEvent((PlayableCard)card);
    }


    // Set selected card
    public void SetSelectedCard(Card card)
    {
        if (card == selectedCard)
            selectedCard = null;
        else
        {
            if (selectedCard != null)
                SetCardHover(selectedCard, false);
            selectedCard = card;
        }
    }


    // Play selected card
    public void PlayCard(Card card)
    {
        if (card != selectedCard)
            throw new System.ArgumentException("Cannot play card that is not the selected card.");

        RemoveCard(card);
        SetSelectedCard(null);
    }
}
