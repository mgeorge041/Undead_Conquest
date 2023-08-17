using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using MapActionPhaseStates;

namespace PlayerTests.TurnPhaseTests.MapActionPhaseStateTests
{
    public class ActionState_Tests
    {
        private ActionState state;
        private PlayerItemManager itemManager;
        private HexmapData hexmapData;
        private int numEndStateEvents;
        private StateStartInfo startInfo;
        private int numCreateActionDataEvents;
        private PieceActionData actionData;
        private List<PieceActionData> actionDataList = new List<PieceActionData>();
        private int numPerformPieceActionEvents;


        // Setup
        [SetUp]
        public void Setup()
        {
            itemManager = new PlayerItemManager();
            state = new ActionState(itemManager);
            hexmapData = new HexmapData();
            numEndStateEvents = 0;
            numCreateActionDataEvents = 0;
            actionDataList.Clear();
            numPerformPieceActionEvents = 0;
        }

        private void HandleEndStateEvent(StateStartInfo startInfo)
        {
            numEndStateEvents++;
            this.startInfo = startInfo;
        }

        private void HandleCreateActionDataEvent(PieceActionData actionData)
        {
            numCreateActionDataEvents++;
            this.actionData = actionData;
            actionDataList.Add(actionData);
        }

        private void HandlePerformPieceActionsEvent()
        {
            numPerformPieceActionEvents++;
            foreach(PieceActionData data in actionDataList)
            {
                data.PerformAction();
            }
        }



        // Test creates action state
        [Test]
        public void CreatesActionState()
        {
            Assert.IsNotNull(state);
        }


        // Test starts state
        [Test]
        public void StartsState_NullPiece()
        {
            Assert.Throws<System.ArgumentNullException>(() => state.StartState(new StateStartInfo(StateType.Action, null)));
        }

        [Test]
        public void StartsState_Unit()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            state.StartState(new StateStartInfo(StateType.Action, unit));

            Assert.AreEqual(37, state.reachableHexes.Count);
            Assert.AreEqual(36, state.moveHexes.Count);
            Assert.AreEqual(0, state.attackHexes.Count);
            Assert.AreEqual(2, state.numEdgeLines);
        }

        [Test]
        public void StartsState_Building()
        {
            Building building = Building.CreateBuilding(CardPaths.testBuilding);
            hexmapData.AddPiece(building, Vector3Int.zero);
            state.StartState(new StateStartInfo(StateType.Action, building));

            Assert.AreEqual(1, state.reachableHexes.Count);
            Assert.AreEqual(0, state.moveHexes.Count);
            Assert.AreEqual(0, state.attackHexes.Count);
            Assert.AreEqual(1, state.numEdgeLines);
        }


        // Test left clicks
        [Test]
        public void LeftClick_NullHex()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            state.LeftClick(null);

            Assert.AreEqual(1, numEndStateEvents);
            Assert.AreEqual(StateType.Neutral, startInfo.nextStateType);
        }

        [Test]
        public void LeftClick_EmptyHex()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Hex hex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            state.LeftClick(hex);

            Assert.AreEqual(1, numEndStateEvents);
            Assert.AreEqual(StateType.Neutral, startInfo.nextStateType);
        }

        [Test]
        public void LeftClick_FriendlyHex_HasActions()
        {
            state.eventManager.SubscribeEndPhase(HandleEndStateEvent);
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);
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
            itemManager.pieceManager.AddPiece(unit);
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


        // Test right clicks
        [Test]
        public void RightClick_MoveHex_SetsActionData()
        {
            state.eventManager.SubscribeCreatePieceActionData(HandleCreateActionDataEvent);
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.RightClick(targetHex);

            Assert.AreEqual(1, numCreateActionDataEvents);
            Assert.IsNotNull(actionData);
            Assert.AreEqual(unit, actionData.actionUnit);
            Assert.AreEqual(PieceActionType.Move, actionData.actionType);
            Assert.AreEqual(unitHex, actionData.startHex);
            Assert.AreEqual(targetHex, actionData.targetHex);
        }

        [Test]
        public void RightClick_AttackHex_SetsActionData()
        {
            state.eventManager.SubscribeCreatePieceActionData(HandleCreateActionDataEvent);
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Unit enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.playerId = 1;
            enemyUnit.unitData.playerId = 2;
            
            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            hexmapData.AddPiece(enemyUnit, targetHexCoords);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.RightClick(targetHex);

            Assert.AreEqual(1, numCreateActionDataEvents);
            Assert.IsNotNull(actionData);
            Assert.AreEqual(unit, actionData.actionUnit);
            Assert.AreEqual(PieceActionType.Attack, actionData.actionType);
            Assert.AreEqual(unitHex, actionData.startHex);
            Assert.AreEqual(targetHex, actionData.targetHex);
            Assert.AreEqual(enemyUnit, actionData.targetUnit);
        }

        [Test]
        public void Hover_AttackHex_AttackMoveTarget()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Unit enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.playerId = 1;
            enemyUnit.unitData.playerId = 2;

            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int attackMoveHexCoords = new Vector3Int(1, -1, 0);
            Hex attackMoveHex = hexmapData.GetHexAtHexCoords(attackMoveHexCoords);

            Vector3Int targetHexCoords = new Vector3Int(2, -2, 0);
            hexmapData.AddPiece(enemyUnit, targetHexCoords);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.Hover(targetHex);

            // Event check
            Assert.AreEqual(2, state.movePath.Count);
            Assert.AreEqual(unitHex, state.movePath[0]);
            Assert.AreEqual(attackMoveHex, state.movePath[1]);
        }

        [Test]
        public void RightClick_MoveHex_PerformsActionData()
        {
            state.eventManager.SubscribeCreatePieceActionData(HandleCreateActionDataEvent);
            state.eventManager.SubscribePerformPieceActions(HandlePerformPieceActionsEvent);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.RightClick(targetHex);

            Assert.AreEqual(1, numPerformPieceActionEvents);
            Assert.AreEqual(2, unit.unitData.currentSpeed);
            Assert.AreEqual(targetHex, unit.hex);
            Assert.AreEqual(unit, targetHex.piece);
            Assert.IsNull(unitHex.piece);
        }

        [Test]
        public void RightClick_AttackHex_PerformsActionData()
        {
            state.eventManager.SubscribeCreatePieceActionData(HandleCreateActionDataEvent);
            state.eventManager.SubscribePerformPieceActions(HandlePerformPieceActionsEvent);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Unit enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.playerId = 1;
            enemyUnit.unitData.playerId = 2;

            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            hexmapData.AddPiece(enemyUnit, targetHexCoords);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.RightClick(targetHex);

            Assert.AreEqual(1, numPerformPieceActionEvents);
            Assert.AreEqual(3, enemyUnit.unitData.currentHealth);
            Assert.IsFalse(unit.unitData.hasActions);
        }

        [Test]
        public void RightClick_AttackHex_AttackMoveTarget_PerformsActionData()
        {
            state.eventManager.SubscribeCreatePieceActionData(HandleCreateActionDataEvent);
            state.eventManager.SubscribePerformPieceActions(HandlePerformPieceActionsEvent);

            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            Unit enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.playerId = 1;
            enemyUnit.unitData.playerId = 2;

            Vector3Int unitHexCoords = Vector3Int.zero;
            Hex unitHex = hexmapData.GetHexAtHexCoords(unitHexCoords);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            itemManager.pieceManager.AddPiece(unit);

            Vector3Int attackMoveHexCoords = new Vector3Int(1, -1, 0);
            Hex attackMoveHex = hexmapData.GetHexAtHexCoords(attackMoveHexCoords);

            Vector3Int targetHexCoords = new Vector3Int(2, -2, 0);
            hexmapData.AddPiece(enemyUnit, targetHexCoords);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            state.StartState(new StateStartInfo(StateType.Action, unit));
            state.Hover(targetHex);
            state.RightClick(targetHex);

            Assert.AreEqual(1, numPerformPieceActionEvents);
            Assert.AreEqual(0, unit.unitData.currentSpeed);
            Assert.AreEqual(attackMoveHex, unit.hex);
            Assert.AreEqual(unit, attackMoveHex.piece);
            Assert.IsNull(unitHex.piece);
            Assert.AreEqual(3, enemyUnit.unitData.currentHealth);
            Assert.IsFalse(unit.unitData.hasActions);
        }
    }
}