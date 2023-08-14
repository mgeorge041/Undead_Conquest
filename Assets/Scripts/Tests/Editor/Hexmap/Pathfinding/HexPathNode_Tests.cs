using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace PathfindingTests
{
    public class HexPathNode_Tests
    {
        private HexPathNode pathNode;


        // Setup
        [SetUp]
        public void Setup()
        {
            pathNode = new HexPathNode();
        }


        // Test creates hex path node
        [Test]
        public void CreatesHexPathNode()
        {
            Assert.IsNotNull(pathNode);
        }


        // Test initial coordinates
        [Test]
        public void SetsInitialHexCoords()
        {
            Vector3Int hexCoords = new Vector3Int(1, -1, 0);
            Hex hex = new Hex(hexCoords);
            pathNode = new HexPathNode(hex);
            Assert.AreEqual(hexCoords, pathNode.hexCoords);
        }


        // Test getting neighbors
        [Test]
        public void GetsNeighbors_Default()
        {
            HexPathNode neighbor;
            neighbor = pathNode.GetNeighbor(Direction.U);
            Assert.IsNull(neighbor);
            neighbor = pathNode.GetNeighbor(Direction.UR);
            Assert.IsNull(neighbor);
            neighbor = pathNode.GetNeighbor(Direction.DR);
            Assert.IsNull(neighbor);
            neighbor = pathNode.GetNeighbor(Direction.D);
            Assert.IsNull(neighbor);
            neighbor = pathNode.GetNeighbor(Direction.DL);
            Assert.IsNull(neighbor);
            neighbor = pathNode.GetNeighbor(Direction.UL);
            Assert.IsNull(neighbor);
        }

        [Test]
        public void SetsNeighbor()
        {
            HexPathNode neighbor = new HexPathNode(new Hex(Direction.U));
            pathNode.SetNeighbor(Direction.U, neighbor);
            HexPathNode getNeighbor = pathNode.GetNeighbor(Direction.U);
            Assert.AreEqual(neighbor, getNeighbor);
        }
    }
}