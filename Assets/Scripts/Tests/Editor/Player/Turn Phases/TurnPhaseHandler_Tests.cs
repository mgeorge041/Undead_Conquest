using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class TurnPhaseHandler_Tests
    {
        private TurnPhaseHandler turnPhaseHandler;
        private PlayerItemManager itemManager;
        private int numSetNextPhaseEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            turnPhaseHandler = new TurnPhaseHandler(itemManager);
            numSetNextPhaseEvents = 0;
        }

        private void HandleSetNextPhaseEvent(TurnPhaseType nextPhase)
        {
            numSetNextPhaseEvents++;
        }


        // Test creates turn phase handler
        [Test]
        public void CreatesTurnPhaseHandler()
        {
            Assert.IsNotNull(turnPhaseHandler);
            Assert.IsNotNull(turnPhaseHandler.GetPhase(TurnPhaseType.Draw));
            Assert.IsNotNull(turnPhaseHandler.GetPhase(TurnPhaseType.StartAction));
            Assert.IsNotNull(turnPhaseHandler.GetPhase(TurnPhaseType.PlayCards));
            Assert.IsNotNull(turnPhaseHandler.GetPhase(TurnPhaseType.MapAction));
            Assert.IsNotNull(turnPhaseHandler.GetPhase(TurnPhaseType.EndTurn));
        }


        // Test each phase
        [Test]
        public void StartsPlayCardsPhase()
        {
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.SetNextPhase(TurnPhaseType.PlayCards);

            Assert.AreEqual(1, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.PlayCards), turnPhaseHandler.currentPhase);
        }

        [Test]
        public void StartsMapActionPhase()
        {
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.SetNextPhase(TurnPhaseType.MapAction);

            Assert.AreEqual(1, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.MapAction), turnPhaseHandler.currentPhase);
        }

        [Test]
        public void StartsEndTurnPhase()
        {
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.SetNextPhase(TurnPhaseType.EndTurn);

            Assert.AreEqual(1, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.EndTurn), turnPhaseHandler.currentPhase);
        }
    }
}