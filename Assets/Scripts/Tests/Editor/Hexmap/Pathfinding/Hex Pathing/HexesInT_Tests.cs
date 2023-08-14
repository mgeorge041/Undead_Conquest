using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInT_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test not in T hexes
        [Test]
        public void GetsNotInTHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 1);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test <= 0 T size
        [Test]
        public void GetsTHexes_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, -1);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsTHexes_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 0);
            Assert.AreEqual(1, hexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsTHexes_CenterHex_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 1);
            Assert.AreEqual(4, hexes.Count);
        }

        [Test]
        public void GetsTHexes_CenterHex_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 2);
            Assert.AreEqual(7, hexes.Count);
        }

        [Test]
        public void GetsTHexes_CenterHex_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 15);
            Assert.AreEqual(13, hexes.Count);
        }


        // Test different sized T sides
        [Test]
        public void GetsTHexes_DifferentSizedT_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -3, 2));
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3));
            List<Hex> hexes = HexPathPattern.GetHexesInT(startHex, targetHex, 2);
            Assert.AreEqual(5, hexes.Count);
        }
    }
}