using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PlayerTests.TurnPhaseTests
{
    public class StartActionPhase_Tests
    {
        private StartActionPhase phase;
        private PlayerItemManager itemManager;
        private int numEndPhaseEvents;
        private TurnPhaseType nextPhase;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            
            phase = new StartActionPhase(itemManager);
            phase.eventManager.onEndPhase.Subscribe(HandleEndPhaseEvent);

            numEndPhaseEvents = 0;
            nextPhase = TurnPhaseType.None;
        }


        // Test creates start action phase
        [Test]
        public void CreatesStartActionPhase()
        {
            Assert.IsNotNull(phase);
            Assert.AreEqual(TurnPhaseType.StartAction, phase.phaseType);
        }


        // Test starts phase
        private void HandleEndPhaseEvent(TurnPhaseType nextPhase)
        {
            numEndPhaseEvents++;
            this.nextPhase = nextPhase;
        }

        [Test]
        public void StartsPhase()
        {
            phase.StartPhase();
            Assert.AreEqual(1, numEndPhaseEvents);
            Assert.AreEqual(TurnPhaseType.Draw, nextPhase);
        }
    }
}