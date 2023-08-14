using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pathfinding 
{
    public class PathEdge
    {
        public static string edgeLinePath = "Prefabs/Hexmap/Edge Lines/";
        // Load path edge prefab
        public static LineRenderer CreateEdgeLine(HexListType hexListType)
        {
            LineRenderer edgeLine;
            switch (hexListType)
            {
                case HexListType.Ability:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Ability Edge Line"));
                    break;
                case HexListType.Attack:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Attack Edge Line"));
                    break;
                case HexListType.Move:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Move Edge Line"));
                    break;
                case HexListType.AttackMove:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Attack Move Edge Line"));
                    break;
                case HexListType.Rescue:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Rescue Edge Line"));
                    break;
                case HexListType.AttackRescue:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Attack Rescue Edge Line"));
                    break;
                case HexListType.NPC:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Attack Rescue Edge Line"));
                    break;
                case HexListType.Deploy:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Placement Edge Line"));
                    break;
                case HexListType.Select:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Select Edge Line"));
                    break;
                default:
                    edgeLine = GameObject.Instantiate(Resources.Load<LineRenderer>(edgeLinePath + "Move Edge Line"));
                    break;
            }
            return edgeLine;
        }


        // Object for storing information about which hex edges are path edges
        public class HexEdgeInfo
        {
            public HexPathNode hexNode;
            public Dictionary<Vector3Int, HexPathNode> edgeNeighbors = new Dictionary<Vector3Int, HexPathNode>();

            public HexEdgeInfo(HexPathNode hexNode)
            {
                this.hexNode = hexNode;
            }

            public void SetNeighbor(Vector3Int direction, HexPathNode hexNode)
            {
                edgeNeighbors.Add(direction, hexNode);
            }
            public HexPathNode GetNeighbor(Vector3Int direction)
            {
                return edgeNeighbors[direction]; 
            }
        }


        // Create edge line for path
        public static LineRenderer CreateEdgeLineForPath(List<Hex> path, HexListType hexListType)
        {
            LineRenderer edgeLine = CreateEdgeLine(hexListType);
            List<List<Vector3>> edgePoints = GetPathEdge(path);
            DisplayEdgeLine(edgeLine, edgePoints[0]);
            return edgeLine;
        }


        // Display edge line
        public static void DisplayEdgeLine(LineRenderer line, List<Vector3> edgePoints)
        {
            line.positionCount = edgePoints.Count;
            line.SetPositions(edgePoints.ToArray());
            line.loop = true;
            line.gameObject.SetActive(true);
        }


        // Print path edge
        public static void PrintEdgePoints(List<List<Vector3>> edgePoints)
        {
            foreach (List<Vector3> edgeLine in edgePoints)
            {
                Debug.Log("New line");
                foreach (Vector3 edgePoint in edgeLine)
                {
                    Debug.Log("Edge point: " + edgePoint);
                }
            }
        }


        // Check edge point to see if already been processed
        public static bool ProcessedEdgePoint(List<List<Vector3>> edgeLines, Vector3 edgePoint)
        {
            foreach (List<Vector3> edgeLine in edgeLines)
            {
                if (edgeLine.Contains(edgePoint))
                    return true;
            }
            return false;
        }


        // Get line for edge points
        public static List<Vector3> GetEdgeLine(Dictionary<HexPathNode, HexEdgeInfo> edgeHexes, HexPathNode startHexNode, Vector3Int startDirection)
        {
            List<Vector3> edgeLine = new List<Vector3>();

            Vector3 startPoint = HexPathNode.GetLeftHexPointForDirection(startDirection) + startHexNode.worldPositionInt;
            Vector3 currentPoint = Vector3.zero;
            HexPathNode currentHexNode = startHexNode;
            Vector3Int currentDirection = startDirection;
            bool isFirstPoint = true;

            // Trace points around edge
            int iterations = 0;
            while (currentPoint != startPoint)
            {
                HexEdgeInfo edgeInfo = edgeHexes[currentHexNode];
                HexPathNode nextHexNode = edgeInfo.GetNeighbor(currentDirection);
                if (nextHexNode == null)
                {
                    currentPoint = HexPathNode.GetLeftHexPointForDirection(currentDirection) + currentHexNode.worldPositionInt;
                    edgeLine.Add(currentPoint);
                    currentDirection = Direction.GetNextClockwiseDirection(currentDirection);

                    // Check for first point where current == start
                    if (isFirstPoint)
                        currentPoint = Vector3.zero;
                    isFirstPoint = false;
                }
                else
                {
                    currentHexNode = nextHexNode;
                    currentDirection = Direction.GetNextCounterClockwiseDirection(currentDirection, 2);
                }

                iterations++;
                if (iterations > 1000)
                {
                    Debug.LogError("Infinite loooooooop");
                    break;
                }
            }

            edgeLine.Remove(startPoint);

            return edgeLine;
        }


        // Get all edge points for edge hexes
        public static List<List<Vector3>> GetEdgeLines(Dictionary<HexPathNode, HexEdgeInfo> edgeHexes)
        {
            List<List<Vector3>> edgeLines = new List<List<Vector3>>();
            foreach (KeyValuePair<HexPathNode, HexEdgeInfo> pair in edgeHexes)
            {
                for (int i = 0; i < Direction.directions.Length; i++)
                {
                    Vector3Int direction = Direction.directions[i];
                    HexPathNode neighbor = pair.Value.edgeNeighbors[direction];

                    // Is edge for hex
                    if (neighbor == null)
                    {
                        Vector3 startPoint = HexPathNode.GetLeftHexPointForDirection(direction) + pair.Key.worldPositionInt;
                        if (!ProcessedEdgePoint(edgeLines, startPoint))
                        {
                            List<Vector3> edgeLine = GetEdgeLine(edgeHexes, pair.Key, direction);
                            edgeLines.Add(edgeLine);
                        }
                    }
                }
            }
            return edgeLines;
        }


        // Get all edge hexes for path
        public static Dictionary<HexPathNode, HexEdgeInfo> GetEdgeHexes(List<HexPathNode> path)
        {
            Dictionary<HexPathNode, HexEdgeInfo> edgeHexes = new Dictionary<HexPathNode, HexEdgeInfo>();

            foreach (HexPathNode hexNode in path)
            {
                HexEdgeInfo edgeInfo = new HexEdgeInfo(hexNode);
                bool isEdgeHex = false;

                // Check whether neighbor is null or not in path
                foreach (KeyValuePair<Vector3Int, HexPathNode> pair in hexNode.neighborsDict)
                {
                    if (pair.Value == null || !path.Contains(pair.Value))
                    {
                        edgeInfo.SetNeighbor(pair.Key, null);
                        isEdgeHex = true;
                    }
                    else
                    {
                        edgeInfo.SetNeighbor(pair.Key, pair.Value);
                    }
                }

                if (isEdgeHex)
                    edgeHexes.Add(hexNode, edgeInfo);
            }

            return edgeHexes;
        }


        // Get path edge
        public static List<List<Vector3>> GetPathEdge(List<Hex> path)
        {
            List<List<Vector3>> worldPathEdge = new List<List<Vector3>>();

            if (path == null || path.Count == 0)
                return worldPathEdge;

            // Get path nodes list
            List<HexPathNode> pathNodes = new List<HexPathNode>();
            foreach (Hex hex in path)
            {
                pathNodes.Add(hex.pathNode);
            }

            // Convert edge points to world coordinates
            Dictionary<HexPathNode, HexEdgeInfo> edgeHexes = GetEdgeHexes(pathNodes);
            List<List<Vector3>> edgePoints = GetEdgeLines(edgeHexes);
            foreach (List<Vector3> list in edgePoints)
            {
                List<Vector3> worldEdgePoints = new List<Vector3>();
                foreach (Vector3 edgePoint in list)
                {
                    worldEdgePoints.Add(edgePoint / 100f);
                }
                worldPathEdge.Add(worldEdgePoints);
            }

            return worldPathEdge;
        }
    }
}