using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.CardTests
{
    public class Hand_Tests
    {
        private Hand hand;
        private int numCardClickEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            hand = new Hand();
            hand.eventManager.onLeftClickCard.Subscribe(HandleCardClick);
            numCardClickEvents = 0;
        }


        // Test creates hand
        [Test]
        public void CreatesHand()
        {
            Assert.IsNotNull(hand);
        }


        // Test resets
        [Test]
        public void Resets()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            hand.AddCard(card);
            hand.Reset();
            Assert.AreEqual(0, hand.numCards);
        }


        // Test getting card count
        [Test]
        public void GetsCardCount_EmptyHand()
        {
            int numCards = hand.numCards;
            Assert.AreEqual(0, numCards);
        }

        [Test]
        public void GetsCardCount_1Card()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            hand.AddCard(card);
            int numCards = hand.numCards;
            Assert.AreEqual(1, numCards);
        }


        // Test removing card
        [Test]
        public void RemovesCard()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            hand.AddCard(card);
            hand.RemoveCard(card);
            Assert.AreEqual(0, hand.numCards);
        }


        // Test setting selected card
        [Test]
        public void SetsSelectedCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            hand.AddCard(card);
            hand.SetSelectedCard(card);
            Assert.AreEqual(card, hand.selectedCard);
        }

        [Test]
        public void SetsSelectedCard_SameCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            hand.AddCard(card);
            hand.SetSelectedCard(card);
            hand.SetSelectedCard(card);
            Assert.IsNull(hand.selectedCard);
        }


        // Test playing card
        [Test]
        public void PlaysCard_IsSelectedCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            hand.AddCard(card);
            hand.SetSelectedCard(card);
            hand.PlayCard(card);
            Assert.IsNull(hand.selectedCard);
            Assert.AreEqual(0, hand.numCards);
        }

        [Test]
        public void PlaysCard_NotSelectedCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            hand.AddCard(card);
            Assert.Throws<System.ArgumentException>(() => hand.PlayCard(card));
        }


        // Test card hovering
        [Test]
        public void HandlesCardStartHover() 
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            hand.AddCard(card);
            Vector3 startPosition = card.transform.position;
            hand.CardStartHover(card);

            Vector3 endPosition = startPosition + new Vector3(0, 36);
            Assert.AreEqual(endPosition, card.transform.position);
        }

        [Test]
        public void HandlesCardEndHover()
        {
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            hand.AddCard(card);
            Vector3 startPosition = card.transform.position;
            hand.CardStartHover(card);
            hand.CardEndHover(card);
            Assert.AreEqual(startPosition, card.transform.position);
        }

        [Test]
        public void HoversNonSelectedCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            Vector3 startPosition = card2.transform.position;
            hand.AddCard(card);
            hand.AddCard(card2);
            hand.CardStartHover(card);
            hand.CardLeftClick(card);
            hand.CardStartHover(card2);
            Assert.AreEqual(startPosition + hand.hoverOffset, card2.transform.position);
        }

        [Test]
        public void ClearsHover_SelectNewCard()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            Vector3 startPosition1 = card.transform.position;
            Vector3 startPosition2 = card2.transform.position;
            hand.AddCard(card);
            hand.AddCard(card2);
            hand.CardStartHover(card);
            hand.CardLeftClick(card);
            hand.CardStartHover(card2);
            hand.CardLeftClick(card2);
            Assert.AreEqual(card2, hand.selectedCard);
            Assert.AreEqual(startPosition1, card.transform.position);
            Assert.AreEqual(startPosition2 + hand.hoverOffset, card2.transform.position);
        }


        // Test card click
        private void HandleCardClick(Card card)
        {
            numCardClickEvents++;
        }

        [Test]
        public void HandlesCardClick()
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            hand.AddCard(card);
            card.OnPointerClick(null);
            Assert.AreEqual(1, numCardClickEvents);
            Assert.AreEqual(card, hand.selectedCard);
        }

    }
}