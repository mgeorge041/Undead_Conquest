using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInRange_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test singular hex
        [Test]
        public void GetsAllNeighborsInRange_CenterHex_Neg1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, -1);

            Assert.AreEqual(1, neighbors.Count);
        }
        
        [Test]
        public void GetsAllNeighborsInRange_CenterHex_0()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 0);

            Assert.AreEqual(1, neighbors.Count);
        }


        // Test center hex
        [Test]
        public void GetsAllNeighborsInRange_CenterHex_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(7, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_CenterHex_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(19, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_CenterHex_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }


        // Test top corner
        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopCorner_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(4, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopCorner_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(9, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopCorner_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }


        // Test bottom corner
        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomCorner_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(4, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomCorner_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(9, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomCorner_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, 5, -5));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }


        // Test top right corner
        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopRightCorner_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(4, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopRightCorner_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(9, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_TopRightCorner_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }


        // Test bottom left corner
        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomLeftCorner_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(4, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomLeftCorner_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(9, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_EdgeHex_BottomLeftCorner_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(-5, 5, 0));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }


        // Test close to top corner
        [Test]
        public void GetsAllNeighborsInRange_NearEdgeHex_TopCorner_1()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 1);

            Assert.AreEqual(7, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_NearEdgeHex_TopCorner_2()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 2);

            Assert.AreEqual(14, neighbors.Count);
        }

        [Test]
        public void GetsAllNeighborsInRange_NearEdgeHex_TopCorner_15()
        {
            Hex centerHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4));
            List<Hex> neighbors = HexPathPattern.GetHexesInRange(centerHex, 15);

            Assert.AreEqual(91, neighbors.Count);
        }
    }
}