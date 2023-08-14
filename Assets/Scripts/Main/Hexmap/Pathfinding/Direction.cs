using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    // Line directions
    public static Vector3Int U = new Vector3Int(0, -1, 1);
    public static Vector3Int UR = new Vector3Int(1, -1, 0);
    public static Vector3Int DR = new Vector3Int(1, 0, -1);
    public static Vector3Int D = new Vector3Int(0, 1, -1);
    public static Vector3Int DL = new Vector3Int(-1, 1, 0);
    public static Vector3Int UL = new Vector3Int(-1, 0, 1);
    public static Vector3Int[] directions = new Vector3Int[]
    {
        U,
        UR,
        DR,
        D,
        DL,
        UL
    };

    // Diagonal directions
    public static Vector3Int URDiag = new Vector3Int(1, -2, 1);
    public static Vector3Int RDiag = new Vector3Int(2, -1, -1);
    public static Vector3Int DRDiag = new Vector3Int(1, 1, -2);
    public static Vector3Int DLDiag = new Vector3Int(-1, 2, -1);
    public static Vector3Int LDiag = new Vector3Int(-2, 1, 1);
    public static Vector3Int ULDiag = new Vector3Int(-1, -1, 2);
    public static Vector3Int[] diagDirections = new Vector3Int[]
    {
        URDiag,
        RDiag,
        DRDiag,
        DLDiag,
        LDiag,
        ULDiag
    };


    // Get direction index
    public static int GetDirectionIndex(Vector3Int direction)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            if (directions[i] == direction)
            {
                return i;
            }
        }
        return -1;
    }


    // Get direction between hexes
    public static Vector3Int GetDirectionHexes(Hex startHex, Hex targetHex)
    {
        if (startHex == null || targetHex == null || CoordsAreNeighbors(startHex.hexCoords, targetHex.hexCoords))
            return Vector3Int.zero;

        Vector3Int direction = targetHex.hexCoords - startHex.hexCoords;
        return direction;
    }


    // Get direction between hexes
    public static Vector3Int GetDirectionEnterHex(Hex startHex, Hex targetHex)
    {
        if (startHex == null || targetHex == null || CoordsAreNeighbors(startHex.hexCoords, targetHex.hexCoords))
            return Vector3Int.zero;

        Vector3Int direction = startHex.hexCoords - targetHex.hexCoords;
        return direction;
    }


    // Get next neighbor direction clockwise
    public static Vector3Int GetNextClockwiseDirection(Vector3Int direction, int numRotations = 1)
    {
        return GetNextDirection(direction, numRotations);
    }


    // Get next neighbor direction counter-clockwise
    public static Vector3Int GetNextCounterClockwiseDirection(Vector3Int direction, int numRotations = 1)
    {
        return GetNextDirection(direction, -numRotations);
    }


    // Get next neighbor direction
    private static Vector3Int GetNextDirection(Vector3Int direction, int clockwise)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            if (directions[i] == direction)
            {
                return directions[(i + clockwise + directions.Length) % directions.Length];
            }
        }
        return Vector3Int.zero;
    }


    // Gets whether two sets of hex coords are neighbors
    public static bool CoordsAreNeighbors(Vector3Int hex1, Vector3Int hex2)
    {
        if (GetDistanceHexCoords(hex1, hex2) == 0)
            return true;
        return false;
    }


    // Converts hex coordinates to tilemap coordinates
    public static Vector3Int HexToTileCoords(Vector3Int hexCoords)
    {
        int x = hexCoords.z + (hexCoords.x - (hexCoords.x & 1)) / 2;
        int y = hexCoords.x;

        return new Vector3Int(x, y, 0);
    }


    // Converts tilemap coordinates to hex coordinates
    public static Vector3Int TileToHexCoords(Vector3Int tileCoords)
    {
        int x = tileCoords.y;
        int y = -tileCoords.x - (tileCoords.y + (tileCoords.y & 1)) / 2;
        int z = -x - y;

        return new Vector3Int(x, y, z);
    }


    // Converts list of hex coordinates to tile coordinates
    public static List<Vector3Int> HexToTileCoords(List<Vector3Int> hexCoords)
    {
        List<Vector3Int> tileCoords = new List<Vector3Int>();

        if (hexCoords == null || hexCoords.Count == 0)
            return tileCoords;

        foreach (Vector3Int hexCoord in hexCoords)
        {
            tileCoords.Add(HexToTileCoords(hexCoord));
        }

        return tileCoords;
    }


    // Get distance between two hex coordinates
    public static int GetDistanceHexCoords(Vector3Int hexCoords1, Vector3Int hexCoords2)
    {
        int x = Math.Abs(hexCoords1.x - hexCoords2.x);
        int y = Math.Abs(hexCoords1.y - hexCoords2.y);
        int z = Math.Abs(hexCoords1.z - hexCoords2.z);

        int distance = (x + y + z) / 2;

        return distance;
    }


    // Get distance between two hexes
    public static int GetDistanceHexes(Hex hex1, Hex hex2)
    {
        return GetDistanceHexCoords(hex1.hexCoords, hex2.hexCoords);
    }
    public static int GetDistanceHexNodes(HexPathNode hexNode1, HexPathNode hexNode2)
    {
        return GetDistanceHexCoords(hexNode1.hexCoords, hexNode2.hexCoords);
    }


    // Get absolute difference between coordinates
    public static Vector3Int GetAbsoluteDifferenceCoords(Vector3Int coordsA, Vector3Int coordsB)
    {
        Vector3Int absDiff = new Vector3Int(
            Math.Abs(coordsA.x - coordsB.x),
            Math.Abs(coordsA.y - coordsB.y),
            Math.Abs(coordsA.z - coordsB.z)
        );
        return absDiff;
    }


    // Get whether coords in range of each other
    public static bool CoordsInRange(Vector3Int coords1, Vector3Int coords2, int range)
    {
        return range >= GetDistanceHexCoords(coords1, coords2);
    }


    // Get whether coords in target pattern
    /*
    public static bool CoordsInTargetPattern(EffectPattern pattern, Vector3Int coords1, Vector3Int coords2)
    {
        switch (pattern)
        {
            case EffectPattern.Line:
                return CoordsInLine(coords1, coords2);
            case EffectPattern.Ring:
                return CoordsInLine(coords1, coords2);
            case EffectPattern.Wall:
                return CoordsInDiagonalLine(coords1, coords2);
            case EffectPattern.Fan:
                return CoordsInLine(coords1, coords2);
            case EffectPattern.SpreadV:
                return CoordsInLine(coords1, coords2);
            case EffectPattern.TShape:
                return CoordsInLine(coords1, coords2);
            case EffectPattern.VShape:
                return CoordsInDiagonalLine(coords1, coords2);
            default:
                return false;
        }
    }
    */


    // Get whether coords in line
    public static bool CoordsInLine(Vector3Int coords1, Vector3Int coords2)
    {
        return
            CoordsInXLine(coords1, coords2) ||
            CoordsInYLine(coords1, coords2) ||
            CoordsInZLine(coords1, coords2);
    }


    // Get whether coords in X line
    public static bool CoordsInXLine(Vector3Int coords1, Vector3Int coords2)
    {
        return coords1.x == coords2.x;
    }


    // Get whether coords in Y line
    public static bool CoordsInYLine(Vector3Int coords1, Vector3Int coords2)
    {
        return coords1.y == coords2.y;
    }


    // Get whether coords in Z line
    public static bool CoordsInZLine(Vector3Int coords1, Vector3Int coords2)
    {
        return coords1.z == coords2.z;
    }


    // Get whether coords in diagonal line
    public static bool CoordsInDiagonalLine(Vector3Int coords1, Vector3Int coords2)
    {
        return
            CoordsInXDiagonal(coords1, coords2) ||
            CoordsInYDiagonal(coords1, coords2) ||
            CoordsInZDiagonal(coords1, coords2);
    }


    // Get whether coords in X diagonal
    public static bool CoordsInXDiagonal(Vector3Int coords1, Vector3Int coords2)
    {
        Vector3Int absDiff = GetAbsoluteDifferenceCoords(coords1, coords2);
        return absDiff.y == absDiff.x / 2 && absDiff.z == absDiff.x / 2;
    }


    // Get whether coords in Y diagonal
    public static bool CoordsInYDiagonal(Vector3Int coords1, Vector3Int coords2)
    {
        Vector3Int absDiff = GetAbsoluteDifferenceCoords(coords1, coords2);
        return absDiff.y == 1 * absDiff.x && absDiff.z == 2 * absDiff.x;
    }


    // Get whether coords in Z diagonal
    public static bool CoordsInZDiagonal(Vector3Int coords1, Vector3Int coords2)
    {
        Vector3Int absDiff = GetAbsoluteDifferenceCoords(coords1, coords2);
        return absDiff.y == 2 * absDiff.x && absDiff.z == 1 * absDiff.x;
    }


    // Get coords line direction
    public static Vector3Int GetLineDirection(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (!CoordsInLine(startCoords, targetCoords))
            return Vector3Int.zero;

        if (CoordsInXLine(startCoords, targetCoords))
        {
            if (startCoords.y > targetCoords.y)
                return U;
            else
                return D;
        }
        else if (CoordsInYLine(startCoords, targetCoords))
        {
            if (startCoords.x > targetCoords.x)
                return UL;
            else
                return DR;
        }
        else
        {
            if (startCoords.x > targetCoords.x)
                return DL;
            else
                return UR;
        }
    }


    // Get coords direction in ring
    public static (Vector3Int, Vector3Int) GetRingDirection(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (!CoordsInLine(startCoords, targetCoords))
            return (Vector3Int.zero, Vector3Int.zero);

        if (CoordsInXLine(startCoords, targetCoords))
        {
            if (startCoords.y > targetCoords.y)
                return (DR, DL);
            else
                return (UL, UR);
        }
        else if (CoordsInYLine(startCoords, targetCoords))
        {
            if (startCoords.x > targetCoords.x)
                return (UR, D);
            else
                return (DL, U);
        }
        else
        {
            if (startCoords.x > targetCoords.x)
                return (U, DR);
            else
                return (D, UL);
        }
    }


    // Get coords direction in wall
    public static (Vector3Int, Vector3Int) GetWallDirection(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (!CoordsInDiagonalLine(startCoords, targetCoords))
            return (Vector3Int.zero, Vector3Int.zero);

        if (CoordsInXDiagonal(startCoords, targetCoords))
            return (U, D);
        else if (CoordsInYDiagonal(startCoords, targetCoords))
            return (UR, DL);
        else
            return (UL, DR);
    }


    // Get coords direction in V shape
    public static (Vector3Int, Vector3Int) GetVDirection(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (!CoordsInDiagonalLine(startCoords, targetCoords))
            return (Vector3Int.zero, Vector3Int.zero);

        if (CoordsInXDiagonal(startCoords, targetCoords))
        {
            if (startCoords.y > targetCoords.y)
                return (UR, DR);
            else
                return (DL, UL);
        }
        else if (CoordsInYDiagonal(startCoords, targetCoords))
        {
            if (startCoords.x > targetCoords.x)
                return (UL, U);
            else
                return (DR, D);
        }
        else
        {
            if (startCoords.x > targetCoords.x)
                return (D, DL);
            else
                return (U, UR);
        }
    }


    // Get coords direction in T shape
    public static (Vector3Int, Vector3Int, Vector3Int) GetTDirection(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (!CoordsInLine(startCoords, targetCoords))
            return (Vector3Int.zero, Vector3Int.zero, Vector3Int.zero);

        if (CoordsInXLine(startCoords, targetCoords))
        {
            if (startCoords.y > targetCoords.y)
                return (UL, U, UR);
            else
                return (DR, D, DL);
        }
        else if (CoordsInYLine(startCoords, targetCoords))
        {
            if (startCoords.x > targetCoords.x)
                return (DL, UL, U);
            else
                return (UR, DR, D);
        }
        else
        {
            if (startCoords.x > targetCoords.x)
                return (D, DL, UL);
            else
                return (U, UR, DR);
        }
    }


    // Get whether target is above or below
    public static bool IsTargetUp(Vector3Int startCoords, Vector3Int targetCoords)
    {
        if (startCoords == null || targetCoords == null)
            return false;

        Vector3Int diff = targetCoords - startCoords;
        if (startCoords.x == targetCoords.x)
            return startCoords.y > targetCoords.y;
        else if (startCoords.x < targetCoords.x)
            return Math.Abs(diff.y) > Math.Abs(diff.z);
        else
            return Math.Abs(diff.y) < Math.Abs(diff.z);
    }


    // Get whether hexes in line
    public static bool HexesInLine(Hex hex1, Hex hex2)
    {
        return
            HexesInXLine(hex1, hex2) ||
            HexesInYLine(hex1, hex2) ||
            HexesInZLine(hex1, hex2);
    }


    // Get whether hexes in X line
    public static bool HexesInXLine(Hex hex1, Hex hex2)
    {
        return CoordsInXLine(hex1.hexCoords, hex2.hexCoords);
    }


    // Get whether hexes in Y line
    public static bool HexesInYLine(Hex hex1, Hex hex2)
    {
        return CoordsInYLine(hex1.hexCoords, hex2.hexCoords);
    }


    // Get whether hexes in Z line
    public static bool HexesInZLine(Hex hex1, Hex hex2)
    {
        return CoordsInZLine(hex1.hexCoords, hex2.hexCoords);
    }


    // Get whether hexes in diagonal line
    public static bool HexesInDiagonalLine(Hex hex1, Hex hex2)
    {
        return CoordsInDiagonalLine(hex1.hexCoords, hex2.hexCoords);
    }
}
