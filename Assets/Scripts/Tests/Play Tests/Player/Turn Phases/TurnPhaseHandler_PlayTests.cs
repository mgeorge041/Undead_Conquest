using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class TurnPhaseHandler_PlayTests
    {
        private Player player;
        private TurnPhaseHandler turnPhaseHandler;
        private PlayerItemManager itemManager;
        private int numSetNextPhaseEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            player = Player.CreatePlayer();
            itemManager = player.itemManager;
            turnPhaseHandler = player.turnPhaseHandler;
            numSetNextPhaseEvents = 0;
        }

        private void HandleSetNextPhaseEvent(TurnPhaseType nextPhase)
        {
            numSetNextPhaseEvents++;
        }


        // Test starts turn
        [UnityTest]
        public IEnumerator StartsTurn_PerformsDrawAndStartActions()
        {
            turnPhaseHandler.SetNextPhase(TurnPhaseType.EndTurn);
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.StartNextPhase();

            yield return new WaitForSeconds(1);
            Assert.AreEqual(3, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.PlayCards), turnPhaseHandler.currentPhase);
        }


        // Test each phase
        [UnityTest]
        public IEnumerator StartsDrawPhase_EndsAtPlayCardsPhase()
        {
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.SetNextPhase(TurnPhaseType.Draw);

            yield return new WaitForSeconds(1);
            Assert.AreEqual(2, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.PlayCards), turnPhaseHandler.currentPhase);
        }

        [UnityTest]
        public IEnumerator StartsStartActionPhase_EndsAtPlayCardsPhase()
        {
            turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleSetNextPhaseEvent);
            turnPhaseHandler.SetNextPhase(TurnPhaseType.StartAction);

            yield return new WaitForSeconds(1);
            Assert.AreEqual(3, numSetNextPhaseEvents);
            Assert.AreEqual(turnPhaseHandler.GetPhase(TurnPhaseType.PlayCards), turnPhaseHandler.currentPhase);
        }
    }
}