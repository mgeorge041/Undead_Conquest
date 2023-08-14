using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MapActionPhaseStates;

namespace PlayerTests.TurnPhaseTests
{
    public class MapActionPhase_Tests
    {
        private MapActionPhase phase;
        private PlayerItemManager itemManager;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            phase = new MapActionPhase(itemManager);
        }


        // Test creates map action phase
        [Test]
        public void CreatesMapActionPhase()
        {
            Assert.IsNotNull(phase);
        }


        // Test sets states
        [Test]
        public void SetsPhaseStates()
        {
            Assert.IsNotNull(phase.phaseStates[StateType.Neutral]);
            Assert.IsNotNull(phase.phaseStates[StateType.Info]);
            Assert.IsNotNull(phase.phaseStates[StateType.Action]);
        }


        // Test starts phase
        [Test]
        public void StartsPhase()
        {
            Assert.AreEqual(StateType.Neutral, phase.currentPhaseState.stateType);
        }
    }
}