using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps track of cards in the player's deck and discard pile.
/// </summary>
public class Deck
{
    // Card info
    public int numTotalCards { get; private set; }
    public int numDeckCards => deck.Count;
    public int numDiscardCards => discardPile.Count;
    public int numPlayedCards => playedPile.Count;
    public List<Card> deck { get; private set; } = new List<Card>();
    public List<Card> discardPile { get; private set; } = new List<Card>();
    public List<Card> playedPile { get; private set; } = new List<Card>();

    // Event manager
    public DeckEventManager eventManager { get; private set; } = new DeckEventManager();


    // Constructor
    public Deck() { }
    public Deck(List<Card> cards)
    {
        deck.AddRange(cards);
        numTotalCards = numDeckCards;
    }


    // Reset
    public void Reset()
    {
        deck.Clear();
    }


    // Add cards to deck
    public void AddNewCardToDeck(Card card)
    {
        if (card == null)
            throw new System.ArgumentNullException("Cannot add null card to deck.");

        deck.Add(card);
        card.gameObject.SetActive(false);

        // Increase deck size
        numTotalCards++;
        eventManager.onChangeDeck.OnEvent(numDeckCards, numTotalCards);
        eventManager.onAddCard.OnEvent(card);
    }
    public void ReturnCardToDeck(Card card)
    {
        if (card == null)
            throw new System.ArgumentNullException("Cannot return null card to deck.");

        deck.Add(card);
        card.gameObject.SetActive(false);
    }

    
    // Add cards to discard pile
    public void AddNewCardToDiscard(Card card)
    {
        AddCardToDiscard(card);

        // Increase deck size
        numTotalCards++;
    }
    public void AddCardToDiscard(Card card)
    {
        if (card == null)
            throw new System.ArgumentNullException("Cannot add null card to discard pile.");

        discardPile.Add(card);
        card.gameObject.SetActive(false);
        eventManager.onChangeDiscardPile.OnEvent(numDiscardCards);
    }


    // Add cards to played card pile
    public void AddCardToPlayedPile(Card card)
    {
        if (card == null)
            throw new System.ArgumentNullException("Cannot add null card to played pile.");

        playedPile.Add(card);
        card.gameObject.SetActive(false);
        eventManager.onChangePlayedPile.OnEvent(numPlayedCards);
    }


    // Remove cards
    public void RemoveCard(Card card)
    {
        if (card == null)
            throw new System.ArgumentNullException("Cannot remove null card from deck.");

        deck.Remove(card);
        eventManager.onChangeDeck.OnEvent(numDeckCards, numTotalCards);
    }


    // Draw top card of deck
    public Card DrawCard()
    {
        if (numDeckCards <= 0)
            return null;

        Card topCard = deck[0];
        RemoveCard(topCard);
        return topCard;
    }


    // Shuffle deck
    public void ShuffleDeck()
    {
        ShuffleCards(deck);
    }
    public void ShuffleDiscard()
    {
        ShuffleCards(discardPile);
    }
    private void ShuffleCards(List<Card> shuffleCards)
    {
        List<Card> shuffleCopy = new List<Card>(shuffleCards);
        shuffleCards.Clear();

        while (shuffleCopy.Count > 0)
        {
            int randomInt = Random.Range(0, shuffleCopy.Count - 1);
            Card card = shuffleCopy[randomInt];
            shuffleCards.Add(card);
            shuffleCopy.Remove(card);
        }
    }



    // Shuffle discard back into deck
    public void ShuffleDiscardIntoDeck()
    {
        ShuffleDiscard();
        foreach (Card card in discardPile)
        {
            ReturnCardToDeck(card);
        }
        discardPile.Clear();
        eventManager.onChangeDiscardPile.OnEvent(numDiscardCards);
    }
}
