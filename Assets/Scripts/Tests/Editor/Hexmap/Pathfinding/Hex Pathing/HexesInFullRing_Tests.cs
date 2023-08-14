using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInFullRing_Tests
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


        // Test <= 0 ranges
        [Test]
        public void GetsFullRing_CenterHex_Neg1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, -1);
            Assert.AreEqual(1, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_CenterHex_0()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 0);
            Assert.AreEqual(1, ringHexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsFullRing_CenterHex_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(6, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_CenterHex_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(12, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_CenterHex_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(11, ringHexes.Count);
        }


        // Test top corner
        [Test]
        public void GetsFullRing_TopCorner_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(2, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_TopCorner_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(3, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_TopCorner_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(6, ringHexes.Count);
        }


        // Test bottom corner
        [Test]
        public void GetsFullRing_BottomCorner_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(4, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_BottomCorner_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(7, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_BottomCorner_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(11, ringHexes.Count);
        }


        // Test top right corner
        [Test]
        public void GetsFullRing_TopRightCorner_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(1, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_TopRightCorner_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(1, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_TopRightCorner_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(1, ringHexes.Count);
        }


        // Test bottom left corner
        [Test]
        public void GetsFullRing_BottomLeftCorner_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(6, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_BottomLeftCorner_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(12, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_BottomLeftCorner_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(11, ringHexes.Count);
        }


        // Test near top corner
        [Test]
        public void GetsFullRing_NearTopCorner_1()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 1);
            Assert.AreEqual(4, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_NearTopCorner_2()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 2);
            Assert.AreEqual(5, ringHexes.Count);
        }

        [Test]
        public void GetsFullRing_NearTopCorner_15()
        {
            Hex hex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> ringHexes = HexPathPattern.GetHexesInFullRing(hex, startDirection, 15);
            Assert.AreEqual(7, ringHexes.Count);
        }
    }
}