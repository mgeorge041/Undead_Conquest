using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests.StartActionTests
{
    public class DrawStartAction_Tests
    {
        private DrawStartAction startAction;
        private PlayerItemManager itemManager;


        // Setup
        [SetUp]
        public void Setup()
        {
            startAction = StartAction.CreateStartAction<DrawStartAction>(StartActionPaths.testDrawAction);
            itemManager = new PlayerItemManager();
        }


        // Test creates draw start action
        [Test]
        public void CreatesDrawStartAction()
        {
            Assert.IsNotNull(startAction);
        }


        // Test setting start action info
        [Test]
        public void SetsActionInfo()
        {
            Assert.AreEqual(StartActionType.Draw, startAction.actionType);
            Assert.AreEqual(1, startAction.drawActionInfo.numCards);
        }


        // Test performing draw
        [Test]
        public void PerformsDraw_ResourceCard()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Card card = Card.CreateCard(CardPaths.testBoneResource);
            itemManager.deck.AddNewCardToDeck(card);
            startAction.PerformStartAction(unit, itemManager);
            Assert.AreEqual(0, itemManager.hand.numCards);
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(1, itemManager.deck.numDiscardCards);
        }

        [Test]
        public void PerformsDraw_PlayableCard()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            startAction.PerformStartAction(unit, itemManager);
            Assert.AreEqual(1, itemManager.hand.numCards);
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(0, itemManager.deck.numDiscardCards);
        }

        [Test]
        public void DoesntPerformDraw_TurnCooldown()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Card card = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            startAction.SetTurnCooldown(1);
            startAction.PerformStartAction(unit, itemManager);
            Assert.AreEqual(0, itemManager.hand.numCards);
            Assert.AreEqual(1, itemManager.deck.numDeckCards);
            Assert.AreEqual(0, itemManager.deck.numDiscardCards);
            Assert.AreEqual(0, startAction.turnCooldown);
        }

        
        // Test performing draw multiple cards
        [Test]
        public void DrawsMultipleCards()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Card card = Card.CreateCard(CardPaths.testUnit);
            Card card2 = Card.CreateCard(CardPaths.testUnit);
            itemManager.deck.AddNewCardToDeck(card);
            itemManager.deck.AddNewCardToDeck(card2);
            startAction.drawActionInfo.numCards = 2;
            startAction.PerformStartAction(unit, itemManager);
            Assert.AreEqual(2, itemManager.hand.numCards);
            Assert.AreEqual(0, itemManager.deck.numDeckCards);
            Assert.AreEqual(0, itemManager.deck.numDiscardCards);
        }
    }
}