using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class DrawPhase_Tests
    {
        private DrawPhase phase;
        private PlayerItemManager itemManager;
        private Card card;
        private int numFinishDrawingEvents;
        private Queue<Tuple<Card, int, int, int>> drawQueue;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);

            phase = new DrawPhase(itemManager);
            
            // Event items
            numFinishDrawingEvents = 0;
        }


        // Test creates draw phase
        [Test]
        public void CreatesDrawPhase()
        {
            Assert.IsNotNull(phase);
            Assert.AreEqual(TurnPhaseType.Draw, phase.phaseType);
        }


        // Test performs draw action
        private void HandleDrawCardEvent(Queue<Tuple<Card, int, int, int>> drawQueue)
        {
            numFinishDrawingEvents++;
            this.drawQueue = drawQueue;
        }

        [Test]
        public void DrawsCard()
        {
            itemManager.eventManager.onFinishDrawingCards.Subscribe(HandleDrawCardEvent);
            phase.StartPhase();

            Assert.AreEqual(1, numFinishDrawingEvents);
            Assert.AreEqual(1, drawQueue.Count);
            Assert.AreEqual(1, drawQueue.Peek().Item4);
            Assert.AreEqual(0, drawQueue.Peek().Item2);
            Assert.AreEqual(1, drawQueue.Peek().Item3);
            Assert.AreEqual(card, drawQueue.Peek().Item1);
        }
    }
}