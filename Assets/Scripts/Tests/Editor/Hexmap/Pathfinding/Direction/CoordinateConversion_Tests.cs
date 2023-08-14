using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests.DirectionTests
{
    public class CoordinateConversion_Tests
    {
        private Vector3Int hexCoords;
        private Vector3Int tileCoords;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexCoords = Vector3Int.zero;
            tileCoords = Vector3Int.zero;
        }


        // Test convert hex to tile coords
        [Test]
        public void ConvertsHexToTileCoords_Center()
        {
            Vector3Int newTileCoords = Direction.HexToTileCoords(Vector3Int.zero);
            Vector3Int expectedTileCoords = Vector3Int.zero;
            Assert.AreEqual(expectedTileCoords, newTileCoords);
        }


        // Test convert hex to tile coords
        [Test]
        public void ConvertsHexToTileCoords_NonCenter()
        {
            hexCoords = new Vector3Int(1, -1, 0);
            Vector3Int newTileCoords = Direction.HexToTileCoords(hexCoords);
            Vector3Int expectedTileCoords = new Vector3Int(0, 1, 0);
            Assert.AreEqual(expectedTileCoords, newTileCoords);
        }


        // Test convert tile to hex coords
        [Test]
        public void ConvertsTileToHexCoords_Center()
        {
            Vector3Int newHexCoords = Direction.TileToHexCoords(Vector3Int.zero);
            Vector3Int expectedHexCoords = Vector3Int.zero;
            Assert.AreEqual(expectedHexCoords, newHexCoords);
        }


        // Test convert tile to hex coords
        [Test]
        public void ConvertsTileToHexCoords_NonCenter()
        {
            tileCoords = new Vector3Int(1, -1, 0);
            Vector3Int newHexCoords = Direction.TileToHexCoords(tileCoords);
            Vector3Int expectedHexCoords = new Vector3Int(-1, -1, 2);
            Assert.AreEqual(expectedHexCoords, newHexCoords);
        }


        // Test get distance between hexes
        [Test]
        public void GetsDistanceBetweenHexes_1()
        {
            Vector3Int hexCoords2 = new Vector3Int(1, -1, 0);
            int distance = Direction.GetDistanceHexCoords(hexCoords, hexCoords2);
            int expectedDistance = 1;
            Assert.AreEqual(expectedDistance, distance);
        }


        // Test get distance between hexes
        [Test]
        public void GetsDistanceBetweenHexes_2_Center()
        {
            Vector3Int hexCoords2 = new Vector3Int(2, -1, -1);
            int distance = Direction.GetDistanceHexCoords(hexCoords, hexCoords2);
            int expectedDistance = 2;
            Assert.AreEqual(expectedDistance, distance);
        }


        // Test get distance between hexes
        [Test]
        public void GetsDistanceBetweenHexes_2_NonCenter()
        {
            hexCoords = new Vector3Int(0, -1, 1);
            Vector3Int hexCoords2 = new Vector3Int(2, -1, -1);
            int distance = Direction.GetDistanceHexCoords(hexCoords, hexCoords2);
            int expectedDistance = 2;
            Assert.AreEqual(expectedDistance, distance);
        }
    }
}