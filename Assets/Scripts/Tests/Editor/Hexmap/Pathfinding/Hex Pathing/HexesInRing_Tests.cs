using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInRing_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test not in ring hexes
        [Test]
        public void GetsNonInRingHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 1);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test <= 0 distances
        [Test]
        public void GetsRingHexes_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, -1);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsRingHexes_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 0);
            Assert.AreEqual(1, hexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsRingHexes_CenterHex_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 1);
            Assert.AreEqual(3, hexes.Count);
        }

        [Test]
        public void GetsRingHexes_CenterHex_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 2);
            Assert.AreEqual(5, hexes.Count);
        }

        [Test]
        public void GetsRingHexes_CenterHex_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 15);
            Assert.AreEqual(11, hexes.Count);
        }


        // Test different sided rings
        [Test]
        public void GetsRingHexes_DifferentSidedRings_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4));
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3));
            List<Hex> hexes = HexPathPattern.GetHexesInRing(startHex, targetHex, 2);
            Assert.AreEqual(4, hexes.Count);
        }
    }
}