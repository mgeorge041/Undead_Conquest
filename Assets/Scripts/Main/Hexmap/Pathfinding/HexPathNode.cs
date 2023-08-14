using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexPathNode : IHeapItem<HexPathNode>
{
    // Hex info
    public Hex hex { get; private set; }
    public Vector3Int hexCoords { get; private set; }
    public Vector3 worldPosition { get; private set; }
    public Vector3Int worldPositionInt { get; private set; }

    // Neighbors
    public Dictionary<Vector3Int, HexPathNode> neighborsDict { get; private set; } = new Dictionary<Vector3Int, HexPathNode>()
    {
        { Direction.U, null },
        { Direction.UR, null },
        { Direction.DR, null },
        { Direction.D, null },
        { Direction.DL, null },
        { Direction.UL, null },
    };

    // Pathfinding variables
    public bool moveable => !hex.hasPiece;
    //public bool moveable { get { return piece != null ? false : hexStats.moveable; } set { hexStats.moveable = value; } }
    public HexPathNode pathParent;
    public int moveCost => 1;//{ get { return hexStats != null ? hexStats.moveCost : 1; } }
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public bool visited = false;

    // Heap variables
    public int heapValue { get { return fCost; } }
    public int heapIndex { get; set; }

    // Hex points
    private static Vector3 UL = new Vector2(-8, 16);
    private static Vector3 UR = new Vector2(8, 16);
    private static Vector3 R = new Vector2(16, 0);
    private static Vector3 DR = new Vector2(8, -16);
    private static Vector3 DL = new Vector2(-8, -16);
    private static Vector3 L = new Vector2(-16, 0);
    private static Vector3[] hexPoints = new Vector3[]
    {
        UL,
        UR,
        R,
        DR,
        DL,
        L
    };



    // Constructor
    public HexPathNode() { }
    public HexPathNode(Hex hex)
    {
        this.hex = hex;
        hexCoords = hex.hexCoords;
        int yDist = hexCoords.z - hexCoords.y;
        worldPositionInt = new Vector3Int(hexCoords.x * 24, yDist * 16, 0);
        worldPosition = new Vector3(worldPositionInt.x / 100f, worldPositionInt.y / 100f);
    }


    // Reset
    public void Reset()
    {
        hex = null;
        hexCoords = Vector3Int.zero;
        worldPosition = Vector3.zero;
        worldPositionInt = Vector3Int.zero;
        neighborsDict.Clear();
    }


    // Get neighbor coords at direction
    public Vector3Int GetNeighborCoords(Vector3Int direction)
    {
        return direction + hexCoords;
    }


    // Get neighbor at direction
    public HexPathNode GetNeighbor(Vector3Int direction)
    {
        return neighborsDict[direction];
    }


    // Set hex neighbor
    public void SetNeighbor(Vector3Int neighborCoords, HexPathNode neighborHex)
    {
        neighborsDict[neighborCoords] = neighborHex;
    }


    // Get hex neighbors
    public List<HexPathNode> GetAllNeighbors()
    {
        List<HexPathNode> neighbors = new List<HexPathNode>();
        foreach (HexPathNode pathNode in neighborsDict.Values)
        {
            if (pathNode != null)
                neighbors.Add(pathNode);
        }
        neighbors.Reverse();
        return neighbors;
    }


    // Get direction for hex corner point
    public static Vector3Int GetLeftDirectionForHexPoint(Vector3 hexPoint, Vector3 hexPosition)
    {
        if (hexPoint.x > hexPosition.x)
        {
            if (hexPoint.y > hexPosition.y)
                return Direction.U;
            else if (hexPoint.y == hexPosition.y)
                return Direction.UR;
            else
                return Direction.DR;
        }
        else
        {
            if (hexPoint.y > hexPosition.y)
                return Direction.UL;
            else if (hexPoint.y == hexPosition.y)
                return Direction.DL;
            else
                return Direction.D;
        }
    }


    // Get direction for hex corner point
    public static Vector3Int GetRightDirectionForHexPoint(Vector3 hexPoint, Vector3 hexPosition)
    {
        if (hexPoint.x > hexPosition.x)
        {
            if (hexPoint.y > hexPosition.y)
                return Direction.UR;
            else if (hexPoint.y == hexPosition.y)
                return Direction.DR;
            else
                return Direction.D;
        }
        else
        {
            if (hexPoint.y > hexPosition.y)
                return Direction.U;
            else if (hexPoint.y == hexPosition.y)
                return Direction.UL;
            else
                return Direction.DL;
        }
    }


    // Get hex point for direction
    public static Vector3 GetLeftHexPointForDirection(Vector3Int direction)
    {
        int directionIndex = Direction.GetDirectionIndex(direction);
        return hexPoints[directionIndex];
    }
    public static Vector3 GetRightHexPointForDirection(Vector3Int direction)
    {
        int directionIndex = Direction.GetDirectionIndex(direction);
        return hexPoints[(directionIndex + 1) % hexPoints.Length];
    }

}
