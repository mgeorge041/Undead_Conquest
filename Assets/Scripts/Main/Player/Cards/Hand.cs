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
        card.eventManager.onStartHover.Subscribe(CardStartHover);
        card.eventManager.onEndHover.Subscribe(CardEndHover);
        card.eventManager.onLeftClick.Subscribe(CardLeftClick);
    }
    private void UnsubscribeCardEvents(Card card)
    {
        card.eventManager.onStartHover.Unsubscribe(CardStartHover);
        card.eventManager.onEndHover.Unsubscribe(CardEndHover);
        card.eventManager.onLeftClick.Unsubscribe(CardLeftClick);
    }


    // Handle card hover
    public void CardStartHover(Card card)
    {
        if (card != selectedCard)
            card.transform.position += hoverOffset;
    }
    public void CardEndHover(Card card)
    {
        if (card != selectedCard)
            card.transform.position -= hoverOffset;
    }


    // Handle card click
    public void CardLeftClick(Card card)
    {
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
                selectedCard.transform.position -= hoverOffset;
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
