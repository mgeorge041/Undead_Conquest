using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInLine_Tests
    {
        private HexmapData hexMapData;
        private Vector3Int startDirection;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
            startDirection = Direction.U;
        }


        // Test non in line hexes
        [Test]
        public void GetsNonInLineHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInLine(startHex, targetHex);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test hexes in line
        [Test]
        public void GetsHexesInLine_1Apart()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInLine(startHex, targetHex);
            Assert.AreEqual(2, hexes.Count);
        }

        [Test]
        public void GetsHexesInLine_2Apart()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U * 2);
            List<Hex> hexes = HexPathPattern.GetHexesInLine(startHex, targetHex);
            Assert.AreEqual(3, hexes.Count);
        }

        [Test]
        public void GetsHexesInLine_5Apart()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U * 5);
            List<Hex> hexes = HexPathPattern.GetHexesInLine(startHex, targetHex);
            Assert.AreEqual(6, hexes.Count);
        }


        // Test hexes in direction
        [Test]
        public void GetsHexesInDirection_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            int dist = -1;
            List<Hex> hexes = HexPathPattern.GetHexesInDirection(startHex, startDirection, dist);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsHexesInDirection_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            int dist = 0;
            List<Hex> hexes = HexPathPattern.GetHexesInDirection(startHex, startDirection, dist);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsHexesInDirection_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            int dist = 0;
            List<Hex> hexes = HexPathPattern.GetHexesInDirection(startHex, startDirection, dist);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsHexesInDirection_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            int dist = 1;
            List<Hex> hexes = HexPathPattern.GetHexesInDirection(startHex, startDirection, dist);
            Assert.AreEqual(2, hexes.Count);
        }

        [Test]
        public void GetsHexesInDirection_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            int dist = 15;
            List<Hex> hexes = HexPathPattern.GetHexesInDirection(startHex, startDirection, dist);
            Assert.AreEqual(6, hexes.Count);
        }
    }
}