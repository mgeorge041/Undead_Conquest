using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.CardTests
{
    public class Deck_Tests
    {
        private Deck deck;
        private int numDeckChangeEvents;
        private int numDiscardChangeEvents;
        private int numPlayedPileChangeEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            deck = new Deck();
            numDeckChangeEvents = 0;
            numDiscardChangeEvents = 0;
            numPlayedPileChangeEvents = 0;
        }


        // Test creates deck
        [Test]
        public void CreatesDeck()
        {
            Assert.IsNotNull(deck);
        }


        // Resets
        [Test]
        public void Resets()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDeck(card);
            deck.Reset();
            Assert.AreEqual(0, deck.numDeckCards);
        }


        // Test getting card count
        [Test]
        public void GetsCardCount_EmptyDeck()
        {
            int numCards = deck.numDeckCards;
            Assert.AreEqual(0, numCards);
        }

        [Test]
        public void GetsCardCount_1Card()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDeck(card);
            int numCards = deck.numDeckCards;
            Assert.AreEqual(1, numCards);
        }


        // Test setting initial deck
        [Test]
        public void SetsInitialDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            List<Card> cards = new List<Card>() { card };
            deck = new Deck(cards);
            Assert.AreEqual(1, deck.numDeckCards);
            Assert.AreEqual(1, deck.numTotalCards);
        }


        // Test drawing card
        [Test]
        public void DrawsCard_EmptyDeck()
        {
            Card drawnCard = deck.DrawCard();
            Assert.IsNull(drawnCard);
            Assert.AreEqual(0, deck.numDeckCards);
            Assert.AreEqual(0, deck.numTotalCards);
        }

        [Test]
        public void DrawsCard_1CardInDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDeck(card);
            Card drawnCard = deck.DrawCard();
            Assert.AreEqual(card, drawnCard);
            Assert.AreEqual(0, deck.numDeckCards);
            Assert.AreEqual(1, deck.numTotalCards);
        }


        // Test adding new card to deck
        [Test]
        public void AddsNewCard_EmptyDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDiscard(card);

            Assert.AreEqual(0, deck.numDeckCards);
            Assert.AreEqual(1, deck.numDiscardCards);
            Assert.AreEqual(1, deck.numTotalCards);
        }

        [Test]
        public void AddsNewCard_1CardInHand()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            Card card2 = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDeck(card);
            Card drawCard = deck.DrawCard();
            deck.AddNewCardToDiscard(card2);

            Assert.AreEqual(0, deck.numDeckCards);
            Assert.AreEqual(1, deck.numDiscardCards);
            Assert.AreEqual(2, deck.numTotalCards);
        }


        // Test playing cards
        [Test]
        public void AddsCardToPlayedPile()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            deck.AddNewCardToDeck(card);
            Card drawCard = deck.DrawCard();
            deck.AddCardToPlayedPile(drawCard);

            Assert.AreEqual(0, deck.numDeckCards);
            Assert.AreEqual(1, deck.numPlayedCards);
            Assert.AreEqual(1, deck.numTotalCards);
        }
        


        // Test events 
        private void HandleDeckChangeEvent(int numCards, int totalNumCards)
        {
            numDeckChangeEvents++;
        }
        private void HandleDiscardChangeEvent(int numCards)
        {
            numDiscardChangeEvents++;
        }
        private void HandlePlayedPileChangeEvent(int numCards)
        {
            numPlayedPileChangeEvents++;
        }

        [Test]
        public void FiresChangeDeckEvent_AddCard()
        {
            deck.eventManager.onChangeDeck.Subscribe(HandleDeckChangeEvent);
            Card card = Card.CreateCard(CardPaths.testUnit);
            deck.AddNewCardToDeck(card);
            Assert.AreEqual(1, numDeckChangeEvents);
        }

        [Test]
        public void FiresChangeDeckEvent_RemoveCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            deck.AddNewCardToDeck(card);
            deck.eventManager.onChangeDeck.Subscribe(HandleDeckChangeEvent);
            deck.RemoveCard(card);
            Assert.AreEqual(1, numDeckChangeEvents);
        }

        [Test]
        public void FiresEventChangeDiscard_AddCard()
        {
            deck.eventManager.onChangeDiscardPile.Subscribe(HandleDiscardChangeEvent);
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddCardToDiscard(card);
            Assert.AreEqual(1, numDiscardChangeEvents);
        }

        [Test]
        public void FiresEventChangeDiscard_ShuffleIntoDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddCardToDiscard(card);
            deck.eventManager.onChangeDiscardPile.Subscribe(HandleDiscardChangeEvent);
            deck.ShuffleDiscardIntoDeck();
            Assert.AreEqual(1, numDiscardChangeEvents);
        }

        [Test]
        public void FiresEventChangePlayedPile()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            deck.eventManager.onChangePlayedPile.Subscribe(HandlePlayedPileChangeEvent);
            deck.AddCardToPlayedPile(card);

            Assert.AreEqual(1, numPlayedPileChangeEvents);
            Assert.AreEqual(1, deck.numPlayedCards);
        }


        // Test shuffling
        [Test]
        public void ShufflesDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            Card card3 = Card.CreateCard(CardPaths.testBuilding);
            deck.AddNewCardToDeck(card);
            deck.AddNewCardToDeck(card2);
            deck.AddNewCardToDeck(card3);
            List<Card> cards = new List<Card>() { card, card2, card3 };
            int numDifferent = 0;

            // Shuffle 5 times to count number of different permutations
            Assert.AreEqual(deck.deck[0], cards[0]);
            for (int i = 0; i < 10; i++)
            {
                deck.ShuffleDeck();
                for (int j = 0; j < deck.deck.Count; j++)
                {
                    if (deck.deck[j] != cards[j])
                    {
                        numDifferent++;
                        break;
                    }
                }
            }

            Assert.IsTrue(numDifferent > 0);
        }

        [Test]
        public void ShufflesDiscardIntoDeck()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddCardToDiscard(card);
            deck.ShuffleDiscardIntoDeck();
            Assert.AreEqual(1, deck.numDeckCards);
            Assert.AreEqual(0, deck.numTotalCards);
            Assert.AreEqual(0, deck.numDiscardCards);
        }

        [Test]
        public void ShufflesDiscardIntoDeck_AddNewCard()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            deck.AddNewCardToDiscard(card);
            deck.ShuffleDiscardIntoDeck();
            Assert.AreEqual(1, deck.numDeckCards);
            Assert.AreEqual(1, deck.numTotalCards);
            Assert.AreEqual(0, deck.numDiscardCards);
        }
    }
}