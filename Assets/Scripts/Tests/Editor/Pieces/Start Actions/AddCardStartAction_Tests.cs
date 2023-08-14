using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PieceTests.StartActionTests
{
    public class AddCardStartAction_Tests
    {
        private AddCardStartAction startAction;
        private PlayerItemManager itemManager;
        private Piece piece;


        // Setup
        [SetUp]
        public void Setup()
        {
            startAction = StartAction.CreateStartAction<AddCardStartAction>(StartActionPaths.testAddCardAction);
            itemManager = new PlayerItemManager();
            piece = Piece.CreatePiece(CardPaths.testUnit);
        }


        // Test creates add card start action
        [Test]
        public void CreatesAddCardStartAction()
        {
            Assert.IsNotNull(startAction);
        }


        // Test setting start action info
        [Test]
        public void SetsActionInfo()
        {
            Assert.AreEqual(StartActionType.AddCard, startAction.actionType);
            Assert.AreEqual(1, startAction.addCardActionInfo.numCards);
            Assert.AreEqual(0, startAction.turnCooldown);
        }


        // Test adding cards
        [Test]
        public void PerformsAddCardAction()
        {
            startAction.PerformStartAction(piece, itemManager);
            
            Assert.AreEqual(1, itemManager.deck.numDiscardCards);
            Assert.AreEqual(1, itemManager.deck.numTotalCards);
        }

        [Test]
        public void DoesntPerformAddCardAction_TurnCooldown()
        {
            startAction.SetTurnCooldown(1);
            startAction.PerformStartAction(piece, itemManager);
            
            Assert.AreEqual(0, itemManager.deck.numDiscardCards);
            Assert.AreEqual(0, itemManager.deck.numTotalCards);
        }
    }
}