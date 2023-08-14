using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MapActionPhaseStates;

namespace PlayerTests.TurnPhaseTests.MapActionPhaseStateTests
{
    public class NeutralState_Tests
    {
        private NeutralState state;
        private PlayerItemManager itemManager;
        private HexmapData hexmapData;
        private int numEndStateEvents;
        private StateStartInfo startInfo;


        private void HandleEndStateEvent(StateStartInfo startInfo)
        {
            numEndStateEvents++;
            this.startInfo = startInfo;
        }


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            state = new NeutralState(itemManager);
            hexmapData = new HexmapData();
            numEndStateEvents = 0;
        }


        // Test creates neutral state
        [Test]
        public void CreatesNeutralState()
        {
            Assert.IsNotNull(state);
        }


        // Test left clicks
        [Test]
        public void LeftClick_NullHex()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            state.LeftClick(null);

            Assert.AreEqual(0, numEndStateEvents);
        }

        [Test]
        public void LeftClick_EmptyHex()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Hex hex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            state.LeftClick(hex);

            Assert.AreEqual(0, numEndStateEvents);
        }

        [Test]
        public void LeftClick_FriendlyHex_HasActions()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.AddPiece(unit);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            state.LeftClick(hex);

            Assert.AreEqual(1, numEndStateEvents);
            Assert.AreEqual(StateType.Action, startInfo.nextStateType);
        }

        [Test]
        public void LeftClick_FriendlyHex_NoActions()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.AddPiece(unit);
            unit.unitData.SetHasActions(false);
            Hex hex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            state.LeftClick(hex);

            Assert.AreEqual(1, numEndStateEvents);
            Assert.AreEqual(StateType.Info, startInfo.nextStateType);
        }

        [Test]
        public void LeftClick_EnemyHex()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Unit enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
            Vector3Int hexCoords = new Vector3Int(1, -1, 0);
            hexmapData.AddPiece(enemyUnit, hexCoords);
            Hex hex = hexmapData.GetHexAtHexCoords(hexCoords);
            state.LeftClick(hex);

            Assert.AreEqual(1, numEndStateEvents);
            Assert.AreEqual(StateType.Info, startInfo.nextStateType);
        }
    }
}