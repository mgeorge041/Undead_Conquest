using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInFan_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test not in fan hexes
        [Test]
        public void GetsNotInFanHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 1);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test <= 0 fan size
        [Test]
        public void GetsFanHexes_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, -1);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsFanHexes_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 0);
            Assert.AreEqual(1, hexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsFanHexes_CenterHex_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 1);
            Assert.AreEqual(4, hexes.Count);
        }

        [Test]
        public void GetsFanHexes_CenterHex_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 2);
            Assert.AreEqual(9, hexes.Count);
        }

        [Test]
        public void GetsFanHexes_CenterHex_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 15);
            Assert.AreEqual(25, hexes.Count);
        }


        // Test different sized fan sides
        [Test]
        public void GetsFanHexes_DifferentSizedFan_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -3, 2));
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3));
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 2);
            Assert.AreEqual(6, hexes.Count);
        }


        // Test tail
        [Test]
        public void GetsFanHexes_CenterHex_HasTail_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U * 2);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 1, true);
            Assert.AreEqual(5, hexes.Count);
        }

        [Test]
        public void GetsFanHexes_CenterHex_HasTail_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U * 2);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 2, true);
            Assert.AreEqual(10, hexes.Count);
        }

        [Test]
        public void GetsFanHexes_CenterHex_HasTail_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U * 2);
            List<Hex> hexes = HexPathPattern.GetHexesInFan(startHex, targetHex, 15, true);
            Assert.AreEqual(17, hexes.Count);
        }
    }
}