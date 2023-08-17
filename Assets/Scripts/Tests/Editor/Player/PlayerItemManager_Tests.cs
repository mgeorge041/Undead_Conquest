using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests
{
    public class PlayerItemManager_Tests
    {
        private PlayerItemManager itemManager;
        private int numCardClickEvents;
        private int numFinishDrawingEvents;
        private Queue<Tuple<Card, int, int, int>> drawQueue;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            numCardClickEvents = 0;
            numFinishDrawingEvents = 0;
        }


        // Test creates item manager
        [Test]
        public void CreatesItemManager()
        {
            Assert.IsNotNull(itemManager);
        }


        // Test drawing card
        [Test]
        public void DrawsCard_EmptyDeck()
        {
            itemManager.DrawCard();
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(0, itemManager.hand.numCards);
        }

        [Test]
        public void DrawsCard_PieceCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(1, itemManager.hand.numCards);
            Assert.AreEqual(card, itemManager.hand.cards[0]);
        }

        [Test]
        public void DrawsResourceCard()
        {
            ResourceCard card = Card.CreateCard<ResourceCard>(CardPaths.testBoneResource);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();
            Assert.AreEqual(1, itemManager.resourceManager.GetResource(ResourceType.Bone));
        }


        // Test playing card
        [Test]
        public void PlaysCard()
        {
            itemManager.resourceManager.SetResource(ResourceType.Bone, 5);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 5);
            UnitCard card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();
            itemManager.hand.SetSelectedCard(card);
            itemManager.PlayCard(card);

            Assert.AreEqual(0, itemManager.hand.numCards);
            Assert.AreEqual(3, itemManager.resourceManager.GetResource(ResourceType.Bone));
            Assert.AreEqual(4, itemManager.resourceManager.GetResource(ResourceType.Stone));
        }


        // Test setting card playability
        [Test]
        public void SetsCardPlayability_InsufficientResources()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();
            Assert.IsFalse(card.playable);
        }

        [Test]
        public void SetsCardPlayability_SufficientResources()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.resourceManager.SetResource(ResourceType.Bone, 5);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 5);
            itemManager.DrawCard();
            Assert.IsTrue(card.playable);
        }

        [Test]
        public void SetsCardPlayability_SufficientResourcesMultipleCards()
        {
            itemManager.resourceManager.SetResource(ResourceType.Bone, 5);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 5);

            Card card = Card.CreateCard(CardPaths.testUnit);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.deck.AddNewCardToDeck(card2);
            itemManager.DrawCard();
            itemManager.DrawCard();

            Assert.IsTrue(card.playable);
            Assert.IsTrue(card2.playable);
        }

        [Test]
        public void SetsCardPlayability_InsufficientResourcesAfterPlayedCard()
        {
            itemManager.resourceManager.SetResource(ResourceType.Bone, 2);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 1);

            UnitCard card = Card.CreateCard<UnitCard>(CardPaths.testUnit);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.deck.AddNewCardToDeck(card2);
            itemManager.DrawCard();
            itemManager.DrawCard();
            itemManager.hand.SetSelectedCard(card);
            itemManager.PlayCard(card);

            Assert.IsFalse(card2.playable);
        }


        // Test event handling
        private void HandleCardClick(Card card)
        {
            numCardClickEvents++;
        }
        private void HandleFinishDrawingCards(Queue<Tuple<Card, int, int, int>> drawQueue)
        {
            numFinishDrawingEvents++;
            this.drawQueue = drawQueue;
        }

        [Test]
        public void FiresEvent_ClickCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.resourceManager.SetResource(ResourceType.Bone, 5);
            itemManager.resourceManager.SetResource(ResourceType.Stone, 5);
            itemManager.eventManager.onLeftClickCard.Subscribe(HandleCardClick);
            itemManager.hand.AddCard(card);
            card.OnPointerClick(null);
            Assert.AreEqual(1, numCardClickEvents);
        }

        [Test]
        public void FiresEvent_DrawCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.eventManager.onFinishDrawingCards.Subscribe(HandleFinishDrawingCards);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.DrawCard();

            // Draw event queue
            Assert.AreEqual(1, numFinishDrawingEvents);
            Assert.AreEqual(1, drawQueue.Count);
            Assert.AreEqual(card, drawQueue.Peek().Item1);
            Assert.AreEqual(0, drawQueue.Peek().Item2);
            Assert.AreEqual(1, drawQueue.Peek().Item3);
            Assert.AreEqual(1, drawQueue.Peek().Item4);
        }
    }
}