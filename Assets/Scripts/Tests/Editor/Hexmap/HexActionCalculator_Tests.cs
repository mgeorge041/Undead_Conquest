using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests
{
    public class HexActionCalculator_Tests
    {
        private HexmapData hexmapData;
        private Unit unit;
        private Unit friendlyUnit;
        private Unit enemyUnit;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexmapData = new HexmapData();
            unit = Unit.CreateUnit(CardPaths.testUnit);
            friendlyUnit = Unit.CreateUnit(CardPaths.testUnit);
            enemyUnit = Unit.CreateUnit(CardPaths.testUnit);

            unit.pieceData.playerId = 1;
            friendlyUnit.pieceData.playerId = 1;
            enemyUnit.pieceData.playerId = 2;

            hexmapData.AddPiece(unit, Vector3Int.zero);
        }


        // Test getting move hexes
        [Test]
        public void GetsMoveHexes_EmptyMap()
        {
            List<Hex> moveHexes = HexActionCalculator.GetMoveHexes(unit);
            Assert.AreEqual(36, moveHexes.Count);
        }

        [Test]
        public void GetsMoveHexes_AdjacentUnit()
        {
            hexmapData.AddPiece(friendlyUnit, new Vector3Int(1, -1, 0));
            List<Hex> moveHexes = HexActionCalculator.GetMoveHexes(unit);
            Assert.AreEqual(34, moveHexes.Count);
        }


        // Test getting attackable hexes
        [Test]
        public void GetsAttackableHexes_EmptyMap()
        {
            List<Hex> attackableHexes = HexActionCalculator.GetAttackableHexes(unit);
            Assert.AreEqual(60, attackableHexes.Count);
        }

        [Test]
        public void GetsAttackableHexes_AdjacentUnit()
        {
            hexmapData.AddPiece(friendlyUnit, new Vector3Int(1, -1, 0));
            List<Hex> attackableHexes = HexActionCalculator.GetAttackableHexes(unit);
            Assert.AreEqual(59, attackableHexes.Count);
        }


        // Test getting attack hexes
        [Test]
        public void GetsAttackHexes_EmptyMap()
        {
            List<Hex> attackHexes = HexActionCalculator.GetAttackHexes(unit);
            Assert.AreEqual(0, attackHexes.Count);
        }

        [Test]
        public void GetsAttackHexes_AdjacentFriendlyUnit()
        {
            hexmapData.AddPiece(friendlyUnit, new Vector3Int(1, -1, 0));
            List<Hex> attackHexes = HexActionCalculator.GetAttackHexes(unit);
            Assert.AreEqual(0, attackHexes.Count);
        }

        [Test]
        public void GetsAttackHexes_AdjacentEnemyUnit()
        {
            hexmapData.AddPiece(enemyUnit, new Vector3Int(1, -1, 0));
            List<Hex> attackHexes = HexActionCalculator.GetAttackHexes(unit);
            Assert.AreEqual(1, attackHexes.Count);
        }


        // Test getting attack range hexes
        [Test]
        public void GetsAttackRangeHexes_EmptyMap()
        {
            List<Hex> attackRangeHexes = HexActionCalculator.GetAttackRangeHexes(unit.hex, unit.unitData.range);
            Assert.AreEqual(6, attackRangeHexes.Count);
        }
    }
}