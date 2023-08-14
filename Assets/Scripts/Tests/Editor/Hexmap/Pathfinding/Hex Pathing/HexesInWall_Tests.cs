using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;

namespace PathfindingTests.HexTests
{
    public class HexesInWall_Tests
    {
        private HexmapData hexMapData;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
        }


        // Test not in wall hexes
        [Test]
        public void GetsNotInWallHexes()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(Direction.U);
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 1);
            Assert.AreEqual(0, hexes.Count);
        }


        // Test <= 0 wall size
        [Test]
        public void GetsWallHexes_Neg1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, -1);
            Assert.AreEqual(1, hexes.Count);
        }

        [Test]
        public void GetsWallHexes_0()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 0);
            Assert.AreEqual(1, hexes.Count);
        }


        // Test center hex
        [Test]
        public void GetsWallHexes_CenterHex_1()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 1);
            Assert.AreEqual(3, hexes.Count);
        }

        [Test]
        public void GetsWallHexes_CenterHex_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 2);
            Assert.AreEqual(5, hexes.Count);
        }

        [Test]
        public void GetsWallHexes_CenterHex_15()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(Vector3Int.zero);
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -2, 1));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 15);
            Assert.AreEqual(9, hexes.Count);
        }


        // Test different sized wall sides
        [Test]
        public void GetsWallHexes_DifferentSizedWall_2()
        {
            Hex startHex = hexMapData.GetHexAtHexCoords(new Vector3Int(0, -3, 3));
            Hex targetHex = hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4));
            List<Hex> hexes = HexPathPattern.GetHexesInWall(startHex, targetHex, 2);
            Assert.AreEqual(4, hexes.Count);
        }
    }
}