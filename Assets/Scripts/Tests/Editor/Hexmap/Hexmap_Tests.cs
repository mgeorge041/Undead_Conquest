using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace HexmapTests
{
    public class Hexmap_Tests
    {
        private Hexmap hexmap;


        // Setup
        [SetUp]
        public void Setup()
        {
            hexmap = Hexmap.CreateHexmap();
        }


        // Test creates hexmap
        [Test]
        public void CreatesHexmap()
        {
            Assert.IsNotNull(hexmap);
        }


        // Test initializes
        [Test]
        public void Initializes()
        {
            Assert.IsTrue(hexmap.initialized);
        }


        // Test creates default hexagon map
        [Test]
        public void CreatesDefaultHexagonMap()
        {
            Assert.AreEqual(91, hexmap.hexmapData.numHexes);
        }


        [Test]
        public void HexCoordsDictHasHexes_Radius_3()
        {
            hexmap.mapType = MapType.Hexagon;
            hexmap.mapRadius = 3;
            hexmap.UpdateMapPattern();
            Assert.AreEqual(37, hexmap.hexmapData.hexes.Count);
        }


        // Test gets hex at hex coords
        [Test]
        public void GetsHexAtHexCoords_Exists()
        {
            Hex hex = hexmap.GetHexAtHexCoords(Vector3Int.zero);
            Assert.IsNotNull(hex);
            Assert.AreEqual(Vector3Int.zero, hex.hexCoords);
        }

        [Test]
        public void GetsHexAtHexCoords_NotExists()
        {
            Hex hex = hexmap.GetHexAtHexCoords(new Vector3Int(1, 1, 1));
            Assert.IsNull(hex);
        }


        // Test gets hex at tile coords
        [Test]
        public void GetsHexAtTileCoords_Exists()
        {
            Hex hex = hexmap.GetHexAtTileCoords(Vector3Int.zero);
            Assert.IsNotNull(hex);
            Assert.AreEqual(Vector3Int.zero, hex.hexCoords);
        }

        [Test]
        public void GetsHexAtTileCoords_NotExists()
        {
            Hex hex = hexmap.GetHexAtTileCoords(new Vector3Int(1, 1, 1));
            Assert.IsNull(hex);
        }


        // Test gets hex at world position
        [Test]
        public void GetsHexAtWorldPosition_Exists()
        {
            Hex hex = hexmap.GetHexAtWorldPosition(Vector3.zero);
            Assert.IsNotNull(hex);
            Assert.AreEqual(Vector3Int.zero, hex.hexCoords);
        }

        [Test]
        public void GetsHexAtWorldPosition_NotExists()
        {
            Hex hex = hexmap.GetHexAtWorldPosition(new Vector3(1000, 1000, 1000));
            Assert.IsNull(hex);
        }


        // Test gets hex neighbors
        [Test]
        public void GetsHexNeighbors_Center()
        {
            Hex hex = hexmap.GetHexAtHexCoords(Vector3Int.zero);
            Assert.AreEqual(6, hex.pathNode.GetAllNeighbors().Count);
        }

        [Test]
        public void GetsHexNeighbors_Edge()
        {
            Hex hex = hexmap.GetHexAtHexCoords(new Vector3Int(5, -5, 0));
            Assert.AreEqual(3, hex.pathNode.GetAllNeighbors().Count);
        }
    }
}