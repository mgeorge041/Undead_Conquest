using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class DrawPhase_PlayTests
    {
        private DrawPhase phase;
        private PlayerItemManager itemManager;
        private PlayerUI playerUI;
        private int numFinishDrawingCardsEvents;
        private Queue<Tuple<Card, int, int, int>> drawQueue;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            playerUI = PlayerUI.CreatePlayerUI();
            playerUI.SetItemManagerInfo(itemManager);

            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);

            phase = new DrawPhase(itemManager);
            itemManager.eventManager.onFinishDrawingCards.Subscribe(HandleFinishDrawingCardsEvent);

            numFinishDrawingCardsEvents = 0;
        }


        // Test starting draw phase
        private void HandleFinishDrawingCardsEvent(Queue<Tuple<Card, int, int, int>> drawQueue)
        {
            numFinishDrawingCardsEvents++;
            this.drawQueue = drawQueue;
        }

        [UnityTest]
        public IEnumerator StartsDrawPhase()
        {
            phase.StartPhase();
            yield return new WaitForSeconds(4);
            Assert.AreEqual(1, itemManager.hand.numCards);
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(1, numFinishDrawingCardsEvents);
        }
    }
}