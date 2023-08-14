using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PieceTests
{
    public class Unit_Tests
    {
        private Unit unit;
        private HexmapData hexmapData;
        private int numChangeHealthEvents;
        private int changeHealthEvent_CurrentHealth;
        private int changeHealthEvent_Health;
        private int numChangeSpeedEvents;
        private int changeSpeedEvent_CurrentSpeed;
        private int changeSpeedEvent_Speed;


        // Setup
        [SetUp]
        public void Setup()
        {
            unit = Unit.CreateUnit();
            hexmapData = new HexmapData();

            numChangeHealthEvents = 0;
            changeHealthEvent_CurrentHealth = 0;
            changeHealthEvent_Health = 0;
            numChangeSpeedEvents = 0;
            changeSpeedEvent_CurrentSpeed = 0;
            changeSpeedEvent_Speed = 0;
        }


        // Test creates unit
        [Test]
        public void CreatesUnit()
        {
            Assert.IsNotNull(unit);
        }


        // Test creating unit of type
        [Test]
        public void CreatesUnitWithCardInfo()
        {
            UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            unit = Unit.CreateUnit(unitCardInfo);
            Assert.IsNotNull(unit);
            Assert.AreEqual("Test Unit", unit.unitCardInfo.cardName);
            Assert.AreEqual(2, unit.unitData.attack);
        }


        // Test end turn
        [Test]
        public void EndsTurn()
        {
            unit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitData.SetStat(PieceStatType.CurrentSpeed, 0);
            unit.unitData.SetHasActions(false);
            unit.EndTurn();

            Assert.AreEqual(3, unit.unitData.GetStat(PieceStatType.CurrentSpeed));
            Assert.IsTrue(unit.unitData.hasActions);
        }


        // Test performing movement of unit
        [Test]
        public void MovesUnit_NullHex_ThrowsError()
        {
            hexmapData.AddPiece(unit, Vector3Int.zero);
            Assert.Throws<System.ArgumentNullException>(() => unit.MoveToHex(null));
        }

        [Test]
        public void MovesUnit_HexWithPiece_ThrowsError()
        {
            hexmapData.AddPiece(unit, Vector3Int.zero);
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            hexmapData.AddPiece(piece, targetHexCoords);
            
            Assert.Throws<System.ArgumentException>(() => unit.MoveToHex(targetHex));
        }

        [Test]
        public void MovesUnit()
        {
            UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            unit = Unit.CreateUnit(unitCardInfo);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unit.MoveToHex(targetHex);
            
            Assert.AreEqual(2, unit.unitData.currentSpeed);
            Assert.AreEqual(targetHex, unit.hex);
            Assert.AreEqual(unit, targetHex.piece);
            Assert.IsNull(startHex.piece);
        }

        [Test]
        public void MovesUnit_CreatesMovePath()
        {
            Unit unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            Hex targetHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            List<Hex> hexPath = HexPathfinding.GetPath(unit.hex.pathNode, targetHex.pathNode);
            List<Vector3> movePath = unit.CreateMovePath(hexPath);
            
            Assert.AreEqual(2, movePath.Count);
            Assert.AreEqual(Vector3.zero, movePath[0]);
            Assert.AreEqual(targetHex.pathNode.worldPosition, movePath[1]);

        }


        // Test performing attack
        [Test]
        public void AttacksHex_NullHex_ThrowsError()
        {
            UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            unit = Unit.CreateUnit(unitCardInfo);

            Assert.Throws<System.ArgumentNullException>(() => unit.AttackHex(null));
        }

        [Test]
        public void AttacksHex_ValidHex()
        {
            UnitCardInfo unitCardInfo = CardInfo.LoadCardInfo<UnitCardInfo>(CardPaths.testUnit);
            unit = Unit.CreateUnit(unitCardInfo);
            Piece piece = Piece.CreatePiece(CardPaths.testUnit);
            Vector3Int targetHexCoords = new Vector3Int(1, -1, 0);
            Hex targetHex = hexmapData.GetHexAtHexCoords(targetHexCoords);
            hexmapData.AddPiece(piece, targetHexCoords);

            unit.AttackHex(targetHex);

            Assert.AreEqual(3, targetHex.piece.pieceData.currentHealth);
            Assert.IsFalse(unit.unitData.hasActions);
        }


        // Test changes in health
        private void HandleChangeHealthEvent(int currentHealth, int health)
        {
            numChangeHealthEvents++;
            changeHealthEvent_CurrentHealth = currentHealth;
            changeHealthEvent_Health = health;
        }

        [Test]
        public void FiresEvent_ChangeHealth()
        {
            unit = Unit.CreateUnit(CardPaths.testUnit);
            unit.unitEventManager.onChangeHealth.Subscribe(HandleChangeHealthEvent);
            unit.TakeDamage(1);

            Assert.AreEqual(1, numChangeHealthEvents);
            Assert.AreEqual(4, changeHealthEvent_CurrentHealth);
            Assert.AreEqual(5, changeHealthEvent_Health);
        }


        // Test changes in speed
        private void HandleChangeSpeedEvent(int currentSpeed, int speed)
        {
            numChangeSpeedEvents++;
            changeSpeedEvent_CurrentSpeed = currentSpeed;
            changeSpeedEvent_Speed = speed;
        }

        [Test]
        public void FiresEvent_ChangeSpeed()
        {
            Hex startHex = hexmapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex moveHex = hexmapData.GetHexAtHexCoords(new Vector3Int(1, -1, 0));
            unit = Unit.CreateUnit(CardPaths.testUnit);
            hexmapData.AddPiece(unit, Vector3Int.zero);
            unit.unitEventManager.onChangeSpeed.Subscribe(HandleChangeSpeedEvent);

            unit.MoveToHex(moveHex);
            Assert.AreEqual(1, numChangeSpeedEvents);
            Assert.AreEqual(2, changeSpeedEvent_CurrentSpeed);
            Assert.AreEqual(3, changeSpeedEvent_Speed);
        }
    }
}