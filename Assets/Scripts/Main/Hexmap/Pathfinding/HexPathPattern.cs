using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public static class HexPathPattern
    {
        /*
        // Get hexes for effect pattern
        public static List<Hex> GetHexesInEffectPattern(EffectPattern effectPattern, Hex startHex, Hex targetHex, int size)
        {
            List<Hex> patternHexes = new List<Hex>();

            switch (effectPattern)
            {
                case EffectPattern.Line:
                    return GetHexesInLine(startHex, targetHex);
                case EffectPattern.Ring:
                    return GetHexesInRing(startHex, targetHex, size);
                case EffectPattern.Wall:
                    return GetHexesInWall(startHex, targetHex, size);
                case EffectPattern.Fan:
                    return GetHexesInFan(startHex, targetHex, size);
                case EffectPattern.SpreadV:
                    return GetHexesInT(startHex, targetHex, size, false);
                case EffectPattern.TShape:
                    return GetHexesInT(startHex, targetHex, size);
                case EffectPattern.VShape:
                    return GetHexesInV(startHex, targetHex, size);
                case EffectPattern.Area:
                    return GetHexesInRange(startHex, size);
                case EffectPattern.Pierce:
                    Vector3Int direction = Direction.GetDirectionHexes(startHex, targetHex);
                    return GetHexesInDirection(targetHex, direction, size);
                case EffectPattern.Target:
                    return new List<Hex>() { targetHex };
                default:
                    return patternHexes;
            }
        }


        // Get hexes matching target pattern within a certain range
        public static List<Hex> GetTargetHexesInRange(TargetPattern targetPattern, Hex startHex, int range)
        {
            List<Hex> targetHexes = new List<Hex>();

            if (startHex == null || targetPattern == TargetPattern.None)
                return targetHexes;

            List<Hex> potentialTargets = GetHexesInRange(startHex, range);
            foreach (Hex potentialTarget in potentialTargets)
            {
                if (potentialTarget.IsTargetPattern(targetPattern, startHex, range))
                    targetHexes.Add(potentialTarget);
            }

            return targetHexes;
        }
        */

        // Get hexes in range
        public static List<Hex> GetHexesInRange(Hex startHex, int range, bool includeCenter = true)
        { 
            List<Hex> hexes = new List<Hex>();

            // Return start hex
            if (range <= 0)
            {
                hexes.Add(startHex);
                return hexes;
            }

            // Go around rings to get hexes
            HexPathNode currentHexNode = startHex.pathNode;
            for (int i = 1; i <= range; i++)
            {
                Vector3Int startDirection = Direction.U;
                bool goodDirection = true;

                // Try going out each direction to get ring 
                for (int j = 0; j < Direction.directions.Length; j++)
                {
                    startDirection = Direction.directions[j];
                    currentHexNode = startHex.pathNode;
                    goodDirection = true;
                    
                    for (int k = 0; k < i; k++)
                    {
                        currentHexNode = currentHexNode.GetNeighbor(startDirection);
                        if (currentHexNode == null)
                        {
                            goodDirection = false;
                            break;
                        }
                    }
                    if (goodDirection)
                    {
                        break;
                    }
                }

                // Return list if can't make it out to range in any direction
                if (!goodDirection)
                    break;

                // Get each ring of hexes
                Vector3Int ringDirection = Direction.GetNextClockwiseDirection(startDirection, 2);
                hexes.AddRange(GetHexesInFullRing(currentHexNode.hex, ringDirection, i));
            }

            if (includeCenter && !hexes.Contains(startHex))
                hexes.Add(startHex);

            return hexes;
        }


        // Get hexes in line
        public static List<Hex> GetHexesInLine(Hex startHex, Hex targetHex, bool includeStart = true)
        {
            List<Hex> lineHexes = new List<Hex>();

            if (startHex == null || targetHex == null || startHex == targetHex ||
                !Direction.CoordsInLine(startHex.hexCoords, targetHex.hexCoords))
                return lineHexes;

            // Determine direction
            Vector3Int direction = Direction.GetLineDirection(startHex.hexCoords, targetHex.hexCoords);
            int dist = Direction.GetDistanceHexes(startHex, targetHex);
            return GetHexesInDirection(startHex, direction, dist, includeStart);
        }


        // Get hexes in line
        public static List<Hex> GetHexesInDirection(Hex startHex, Hex targetHex, int lineLength, bool includeStart = true)
        {
            if (startHex == null || targetHex == null || startHex == targetHex ||
                !Direction.CoordsInLine(startHex.hexCoords, targetHex.hexCoords))
                return new List<Hex>(); ;

            Vector3Int direction = Direction.GetDirectionHexes(startHex, targetHex);
            return GetHexesInDirection(startHex, direction, lineLength, includeStart);
        }
        public static List<Hex> GetHexesInDirection(Hex startHex, Vector3Int direction, int lineLength, bool includeStart = true)
        {
            List<Hex> lineHexes = new List<Hex>();

            // Determine direction
            HexPathNode currentHexNode = startHex.pathNode;

            if (includeStart)
                lineHexes.Add(currentHexNode.hex);

            // Move along line
            for (int i = 0; i < lineLength; i++)
            {
                currentHexNode = currentHexNode.GetNeighbor(direction);
                if (currentHexNode == null)
                    break;
                lineHexes.Add(currentHexNode.hex);
            }
            return lineHexes;
        }


        // Get hexes in full ring
        public static List<Hex> GetHexesInFullRing(Hex startHex, Vector3Int startDirection, int ringSize)
        {
            bool turnClockwise = true;
            Vector3Int currentDirection = startDirection;

            Vector3Int GetNextDirection(int numRotations = 1)
            {
                if (turnClockwise)
                    return Direction.GetNextClockwiseDirection(currentDirection, numRotations);
                else
                    return Direction.GetNextCounterClockwiseDirection(currentDirection, numRotations);
            }

            // Get each ring of hexes
            List<Hex> hexes = new List<Hex>();
            Vector3Int nextStartDirection = GetNextDirection(2);
            HexPathNode currentHexNode = startHex.pathNode;
            bool foundFirstEdge = false;
            bool foundSecondEdge = false;

            for (int i = 0; i < 6; i++)
            {
                bool foundEdge = false;
                for (int j = 0; j < ringSize; j++)
                {
                    currentHexNode = currentHexNode.GetNeighbor(currentDirection);

                    // Turn around if find edge
                    if (currentHexNode == null)
                    {
                        currentHexNode = startHex.pathNode;
                        currentDirection = nextStartDirection;
                        turnClockwise = !turnClockwise;
                        foundEdge = true;

                        // Hit first or second edge
                        if (!foundFirstEdge)
                            foundFirstEdge = true;
                        else if (foundFirstEdge)
                            foundSecondEdge = true;

                        break;
                    }
                    hexes.Add(currentHexNode.hex);
                }
                if (!foundEdge)
                currentDirection = GetNextDirection();

                // Break on finding both edges
                if (foundFirstEdge && foundSecondEdge)
                    break;
            }

            if (!hexes.Contains(startHex))
                hexes.Add(startHex);

            return hexes;
        }


        // Get hexes in ring
        public static List<Hex> GetHexesInRing(Hex startHex, Hex targetHex, int ringWidth)
        {
            List<Hex> hexes = new List<Hex>();

            if (startHex == null || targetHex == null || startHex == targetHex ||
                !Direction.HexesInLine(startHex, targetHex))
                return hexes;

            HexPathNode currentHexNode1 = targetHex.pathNode;
            HexPathNode currentHexNode2 = targetHex.pathNode;
            Vector3Int direction1;
            Vector3Int direction2;
            hexes.Add(targetHex);
            (direction1, direction2) = Direction.GetRingDirection(startHex.hexCoords, targetHex.hexCoords);

            // Get coords
            for (int i = 0; i < ringWidth; i++)
            {
                if (currentHexNode1 != null)
                {
                    currentHexNode1 = currentHexNode1.GetNeighbor(direction1);
                    if (currentHexNode1 != null)
                        hexes.Add(currentHexNode1.hex);
                }
                if (currentHexNode2 != null)
                {
                    currentHexNode2 = currentHexNode2.GetNeighbor(direction2);
                    if (currentHexNode2 != null)
                        hexes.Add(currentHexNode2.hex);
                }
                    
                if (currentHexNode1 == null && currentHexNode2 == null)
                    break;
            }

            return hexes;
        }


        // Get hexes in wall
        public static List<Hex> GetHexesInWall(Hex startHex, Hex targetHex, int wallWidth)
        {
            List<Hex> wallHexes = new List<Hex>();

            if (startHex == targetHex || startHex == null || targetHex == null)
                return wallHexes;

            if (!Direction.HexesInDiagonalLine(startHex, targetHex))
                return wallHexes;

            HexPathNode currentHexNode1 = targetHex.pathNode;
            HexPathNode currentHexNode2 = targetHex.pathNode;
            Vector3Int direction1;
            Vector3Int direction2;
            wallHexes.Add(targetHex);
            (direction1, direction2) = Direction.GetWallDirection(startHex.hexCoords, targetHex.hexCoords);

            // Get coords
            for (int i = 0; i < wallWidth; i++)
            {
                if (currentHexNode1 != null)
                {
                    currentHexNode1 = currentHexNode1.GetNeighbor(direction1);
                    if (currentHexNode1 != null)
                        wallHexes.Add(currentHexNode1.hex);
                }
                if (currentHexNode2 != null)
                {
                    currentHexNode2 = currentHexNode2.GetNeighbor(direction2);
                    if (currentHexNode2 != null)
                        wallHexes.Add(currentHexNode2.hex);
                }

                if (currentHexNode1 == null && currentHexNode2 == null)
                    break;
            }

            return wallHexes;
        }


        // Get hexes in V shape
        public static List<Hex> GetHexesInV(Hex startHex, Hex targetHex, int vLength)
        {
            List<Hex> vHexes = new List<Hex>();

            if (startHex == targetHex)
                return vHexes;

            if (!Direction.HexesInDiagonalLine(startHex, targetHex))
                return vHexes;

            HexPathNode currentHexNode1 = targetHex.pathNode;
            HexPathNode currentHexNode2 = targetHex.pathNode;
            Vector3Int direction1;
            Vector3Int direction2;
            vHexes.Add(targetHex);
            (direction1, direction2) = Direction.GetVDirection(startHex.hexCoords, targetHex.hexCoords);

            // Get coords
            for (int i = 0; i < vLength; i++)
            {
                if (currentHexNode1 != null)
                {
                    currentHexNode1 = currentHexNode1.GetNeighbor(direction1);
                    if (currentHexNode1 != null)
                        vHexes.Add(currentHexNode1.hex);
                }
                if (currentHexNode2 != null)
                {
                    currentHexNode2 = currentHexNode2.GetNeighbor(direction2);
                    if (currentHexNode2 != null)
                        vHexes.Add(currentHexNode2.hex);
                }

                if (currentHexNode1 == null && currentHexNode2 == null)
                    break;
            }

            return vHexes;
        }


        // Get hexes in T shape
        public static List<Hex> GetHexesInT(Hex startHex, Hex targetHex, int tLength, bool includeMiddle = true)
        {
            List<Hex> tHexes = new List<Hex>();

            if (startHex == targetHex || startHex == null || targetHex == null)
                return tHexes;

            if (!Direction.HexesInLine(startHex, targetHex))
                return tHexes;

            HexPathNode currentHexNode1 = targetHex.pathNode;
            HexPathNode currentHexNode2 = targetHex.pathNode;
            HexPathNode currentHexNode3 = targetHex.pathNode;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            tHexes.Add(targetHex);
            (direction1, direction2, direction3) = Direction.GetTDirection(startHex.hexCoords, targetHex.hexCoords);

            // Get coords
            for (int i = 0; i < tLength; i++)
            {
                if (currentHexNode1 != null)
                {
                    currentHexNode1 = currentHexNode1.GetNeighbor(direction1);
                    if (currentHexNode1 != null)
                        tHexes.Add(currentHexNode1.hex);
                }
                if (currentHexNode2 != null)
                {
                    currentHexNode2 = currentHexNode2.GetNeighbor(direction2);
                    if (includeMiddle && currentHexNode2 != null)
                        tHexes.Add(currentHexNode2.hex);
                }
                if (currentHexNode3 != null)
                {
                    currentHexNode3 = currentHexNode3.GetNeighbor(direction3);
                    if (currentHexNode3 != null)
                        tHexes.Add(currentHexNode3.hex);
                }

                if (currentHexNode1 == null && currentHexNode2 == null && currentHexNode3 == null)
                    break;
            }

            return tHexes;
        }


        // Get hexes in fan
        public static List<Hex> GetHexesInFan(Hex startHex, Hex targetHex, int fanRadius, bool addTail = false)
        {
            List<Hex> fanHexes = new List<Hex>();

            if (startHex == targetHex || startHex == null || targetHex == null)
                return fanHexes;

            if (!Direction.HexesInLine(startHex, targetHex))
                return fanHexes;

            HexPathNode currentHexNode1 = targetHex.pathNode;
            HexPathNode currentHexNode2 = targetHex.pathNode;
            HexPathNode currentHexNode3 = targetHex.pathNode;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;

            if (addTail)
                fanHexes.AddRange(GetHexesInLine(startHex, targetHex, false));
            else
                fanHexes.Add(targetHex);

            (direction1, direction2, direction3) = Direction.GetTDirection(startHex.hexCoords, targetHex.hexCoords);

            // Get coords
            for (int i = 0; i < fanRadius; i++)
            {
                for (int j = i; j < fanRadius; j++)
                {
                    if (currentHexNode1 != null)
                    {
                        currentHexNode1 = currentHexNode1.GetNeighbor(direction1);
                        if (currentHexNode1 != null)
                            fanHexes.Add(currentHexNode1.hex);
                    }
                    if (currentHexNode3 != null)
                    {
                        currentHexNode3 = currentHexNode3.GetNeighbor(direction3);
                        if (currentHexNode3 != null)
                            fanHexes.Add(currentHexNode3.hex);
                    }
                }
                if (currentHexNode2 != null)
                {
                    currentHexNode2 = currentHexNode2.GetNeighbor(direction2);
                    if (currentHexNode2 != null)
                        fanHexes.Add(currentHexNode2.hex);
                }
                currentHexNode1 = currentHexNode2;
                currentHexNode3 = currentHexNode2;
            }

            return fanHexes;
        }

    }
}