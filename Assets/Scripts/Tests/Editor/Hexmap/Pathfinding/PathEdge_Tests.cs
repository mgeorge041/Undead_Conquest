using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Pathfinding;


namespace PathfindingTests
{
    public class PathEdge_Tests
    {
        private HexmapData hexMapData;
        private List<Hex> edgeHexes;


        // Create ring of hexes
        private List<Hex> CreateHexRing(List<Hex> hexes)
        {
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.U));
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.UR));
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.DR));
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.D));
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.DL));
            hexes.Add(hexMapData.GetHexAtHexCoords(Direction.UL));
            return hexes;
        }


        // Setup
        [SetUp]
        public void Setup()
        {
            hexMapData = new HexmapData();
            edgeHexes = new List<Hex>();
        }


        // Test create path edge line
        [Test]
        public void CreatePathEdgeLine_2Hex()
        {
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.UR));

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(1, edgePoints.Count);
            Assert.AreEqual(10, edgePoints[0].Count);
        }


        // Test create path edge line
        [Test]
        public void CreatePathEdgeLine_3Hex()
        {
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.UR));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.UR + Direction.UR));

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(1, edgePoints.Count);
            Assert.AreEqual(14, edgePoints[0].Count);
        }


        // Test create path edge line
        [Test]
        public void CreatePathEdgeLine_5Hex()
        {
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.UR));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.DR));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.D));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.DL));

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(1, edgePoints.Count);
            Assert.AreEqual(16, edgePoints[0].Count);
        }


        // Test create path edge line
        [Test]
        public void CreatePathEdgeLine_5Hex_Fan()
        {
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.U));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.DR));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.D));
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Direction.DL));

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(1, edgePoints.Count);
            Assert.AreEqual(18, edgePoints[0].Count);
        }


        // Test get edge points
        [Test]
        public void GetsEdgePoints_Ring_Filled()
        {
            // Create edge ring
            edgeHexes = CreateHexRing(edgeHexes);
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(1, edgePoints.Count);
            Assert.AreEqual(18, edgePoints[0].Count);
        }


        // Test get path edge
        [Test]
        public void GetsPathEdge_Ring_Filled()
        {
            // Create edge ring
            edgeHexes = CreateHexRing(edgeHexes);
            edgeHexes.Add(hexMapData.GetHexAtHexCoords(Vector3Int.zero));

            List<List<Vector3>> pathEdge = PathEdge.GetPathEdge(edgeHexes);
            Assert.AreEqual(1, pathEdge.Count);
            Assert.AreEqual(18, pathEdge[0].Count);
        }


        // Test get edge points
        [Test]
        public void GetsEdgePoints_Ring()
        {
            // Create edge ring
            edgeHexes = CreateHexRing(edgeHexes);

            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(edgeHexes);
            PathEdge.PrintEdgePoints(edgePoints);
            Assert.AreEqual(2, edgePoints.Count);
            Assert.AreEqual(18, edgePoints[0].Count);
            Assert.AreEqual(6, edgePoints[1].Count);
        }


        // Test get path edge
        [Test]
        public void GetsPathEdge_Ring()
        {
            // Create edge ring
            edgeHexes = CreateHexRing(edgeHexes);

            List<List<Vector3>> pathEdge = PathEdge.GetPathEdge(edgeHexes);
            Assert.AreEqual(2, pathEdge.Count);
            Assert.AreEqual(18, pathEdge[0].Count);
            Assert.AreEqual(6, pathEdge[1].Count);
        }


        // Test getting hexes on edge of path
        [Test]
        public void GetsEdgeHexes()
        {
            List<HexPathNode> hexes = new List<HexPathNode>()
            {
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -4, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -3, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -3, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3)).pathNode,
            };
            Dictionary<HexPathNode, PathEdge.HexEdgeInfo> edgeHexes = PathEdge.GetEdgeHexes(hexes);
            Assert.AreEqual(6, edgeHexes.Count);
            Assert.IsFalse(edgeHexes.ContainsKey(hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4)).pathNode));
        }

        [Test]
        public void GetsEdgeHexes_HoleInCenter()
        {
            List<HexPathNode> hexes = new List<HexPathNode>()
            {
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -4, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -3, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -3, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -3, 2)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(2, -5, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(2, -4, 2)).pathNode,
            };
            Dictionary<HexPathNode, PathEdge.HexEdgeInfo> edgeHexes = PathEdge.GetEdgeHexes(hexes);
            Assert.AreEqual(9, edgeHexes.Count);
            Assert.IsTrue(edgeHexes.ContainsKey(hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3)).pathNode));
        }


        // Test getting edge line
        [Test]
        public void GetsEdgeLine()
        {
            List<HexPathNode> hexes = new List<HexPathNode>()
            {
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -4, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -3, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -4, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -3, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3)).pathNode,
            };
            Dictionary<HexPathNode, PathEdge.HexEdgeInfo> edgeHexes = PathEdge.GetEdgeHexes(hexes);
            List<Vector3> edgeLine = PathEdge.GetEdgeLine(edgeHexes, hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5)).pathNode, Direction.U);
            Debug.Log("Line count: " + edgeLine.Count);
            Assert.AreEqual(18, edgeLine.Count);
        }

        [Test]
        public void GetsEdgeLines_HoleInCenter()
        {
            List<HexPathNode> hexes = new List<HexPathNode>()
            {
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -4, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(-1, -3, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -5, 5)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(0, -3, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -5, 4)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -4, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(1, -3, 2)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(2, -5, 3)).pathNode,
                hexMapData.GetHexAtHexCoords(new Vector3Int(2, -4, 2)).pathNode,
            };
            Dictionary<HexPathNode, PathEdge.HexEdgeInfo> edgeHexes = PathEdge.GetEdgeHexes(hexes);
            List<List<Vector3>> edgeLine = PathEdge.GetEdgeLines(edgeHexes);
            Debug.Log("Line count: " + edgeLine.Count);
            Assert.AreEqual(2, edgeLine.Count);
            Assert.AreEqual(22, edgeLine[0].Count);
            Assert.AreEqual(6, edgeLine[1].Count);
        }
    }
}