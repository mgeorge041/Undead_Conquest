using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInV_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test not in V hexes
        [Test]
        public void GetsNotInVHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 1);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test <= 0 V size
        [Test]
        public void GetsVHexes_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, -1);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsVHexes_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 0);
            Assert.AreEqual(1, hexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsVHexes_CenterHex_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 1);
            Assert.AreEqual(3, hexes.Count);
        }

        [Test]
        public void GetsVHexes_CenterHex_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 2);
            Assert.AreEqual(5, hexes.Count);
        }

        [Test]
        public void GetsVHexes_CenterHex_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 15);
            Assert.AreEqual(7, hexes.Count);
        }


        // Test different sized V sides
        [Test]
        public void GetsVHexes_DifferentSizedV_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(new Vector3Int(4, -2, -2));
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(5, -4, -1));
            List<Hex> hexes = HexPathPattern.GetHexesInV(startHex, targetHex, 2);
            Assert.AreEqual(2, hexes.Count);
        }
    }
}