using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Pathfinding
{
    public class HexPathfinding
    {
        const int MAX_MAP_SIZE = 500;
        public static bool activeDebug = false;

        // Pathfinding
        public static List<Hex> GetPath(HexPathNode startHexNode, HexPathNode targetHexNode)
        {
            List<Hex> path = new List<Hex>();

            // Exit if not possible to pathfind
            if (targetHexNode == null || startHexNode == null || !targetHexNode.moveable)
            {
                //Debug.Log("target hex not moveable: " + targetHex.piece);
                return path;
            }
            Debug.Log("Start hex: " + startHexNode.hexCoords);
            Debug.Log("Target hex: " + targetHexNode.hexCoords);

            // Create lists for tracking hexes
            //ResetHexPathfinding();
            MinHeap<HexPathNode> openHexNodes = new MinHeap<HexPathNode>(MAX_MAP_SIZE);
            List<HexPathNode> closedHexNodes = new List<HexPathNode>();
            openHexNodes.Add(startHexNode);

            // Loop through hexes
            while (openHexNodes.Count > 0)
            {
                // Get shortest path hex
                HexPathNode currentHexNode = openHexNodes.Pop();
                closedHexNodes.Add(currentHexNode);

                // Return when found target
                if (currentHexNode == targetHexNode)
                {
                    ResetHexPathing(openHexNodes, closedHexNodes);
                    return GetPathBack(startHexNode, targetHexNode);
                }

                // Process neighbor move costs
                List<HexPathNode> neighbors = currentHexNode.GetAllNeighbors();
                foreach (HexPathNode neighbor in neighbors)
                {
                    // Do nothing for unmoveable or already processed hexes
                    if (!neighbor.moveable || closedHexNodes.Contains(neighbor))
                    {
                        continue;
                    }

                    // Update neighbor costs
                    int newDist = currentHexNode.gCost + (currentHexNode.moveCost - 1) + Direction.GetDistanceHexNodes(currentHexNode, neighbor);
                    if (newDist < neighbor.gCost || !openHexNodes.Contains(neighbor))
                    {
                        neighbor.gCost = newDist;
                        neighbor.hCost = Direction.GetDistanceHexNodes(neighbor, targetHexNode);
                        neighbor.pathParent = currentHexNode;

                        // Add new neighbor to process
                        if (!openHexNodes.Contains(neighbor))
                        {
                            openHexNodes.Add(neighbor);
                        }
                    }
                }
            }

            // Clear if no path found
            return path;
        }


        // Reset hex pathfinding and node costs
        private static void ResetHexPathing(MinHeap<HexPathNode> openHexNodes, List<HexPathNode> closedHexNodes)
        {
            while (openHexNodes.Count > 0)
            {
                HexPathNode hexNode = openHexNodes.Pop();
                hexNode.gCost = 0;
                hexNode.hCost = 0;
            }

            foreach (HexPathNode hexNode in closedHexNodes)
            {
                hexNode.gCost = 0;
                hexNode.hCost = 0;
            }
        }


        // Get shortest path from start to one of target hexes
        public static List<Hex> GetMinPath(HexPathNode startHexNode, List<HexPathNode> targetHexNodes)
        {
            List<Hex> path = new List<Hex>();

            if (targetHexNodes == null || targetHexNodes.Count == 0 || startHexNode == null)
                return path;

            int minLength = 0;
            foreach (HexPathNode hex in targetHexNodes) 
            {
                List<Hex> hexPath = GetPath(startHexNode, hex);
                int pathLength = hexPath.Count;
                if (pathLength < minLength || minLength == 0)
                {
                    minLength = pathLength;
                    path = hexPath;
                }
            }

            return path;
        }


        // Get max path from start hex
        public static List<HexPathNode> GetMaxPath(HexPathNode startHexNode)
        {
            List<HexPathNode> path = new List<HexPathNode>();

            if (startHexNode.hex.unit != null)
                return GetMaxPath(startHexNode, startHexNode.hex.unit.unitData.currentSpeed);
            return path;
        }
        public static List<Hex> GetMaxPath(Hex startHex, int unitMove)
        {
            List<Hex> path = new List<Hex>();

            if (startHex.hasUnit)
            {
                List<HexPathNode> pathNodes = GetMaxPath(startHex.pathNode, unitMove);
                foreach (HexPathNode pathNode in pathNodes)
                {
                    path.Add(pathNode.hex);
                }
            }
            return path;
        }


        // Get max path
        public static List<HexPathNode> GetMaxPath(HexPathNode startHexNode, int range)
        {
            List<HexPathNode> path = new List<HexPathNode>();

            if (startHexNode == null || range <= 0)
                return path;

            List<HexPathNode> openHexNodes = new List<HexPathNode>();
            List<HexPathNode> closedHexNodes = new List<HexPathNode>();
            List<HexPathNode> addHexNodes = new List<HexPathNode>();
            openHexNodes.Add(startHexNode);

            // Add current hex to path
            // Add current hex to closed
            // Get current neighbors
            // Add each current neighbor to open
            // Loop through open list
            for (int i = 0; i <= range; i++)
            {
                foreach (HexPathNode hexNode in openHexNodes)
                {
                    path.Add(hexNode);
                    closedHexNodes.Add(hexNode);

                    foreach (HexPathNode neighbor in hexNode.GetAllNeighbors())
                    {
                        if (neighbor.moveable && !openHexNodes.Contains(neighbor) && !closedHexNodes.Contains(neighbor) && !addHexNodes.Contains(neighbor))
                        {
                            addHexNodes.Add(neighbor);
                        }
                    }
                }
                openHexNodes.Clear();
                openHexNodes.AddRange(addHexNodes);
                addHexNodes.Clear();
            }
            path.Remove(startHexNode);
            return path;
        }


        // Get path back
        public static List<Hex> GetPathBack(HexPathNode startHexNode, HexPathNode targetHexNode)
        {
            List<Hex> hexes = new List<Hex>();
            while (targetHexNode != startHexNode)
            {
                hexes.Add(targetHexNode.hex);
                targetHexNode = targetHexNode.pathParent;
            }
            hexes.Add(startHexNode.hex);
            hexes.Reverse();
            return hexes;
        }


        // Get coords for target pattern
        public static List<Vector3Int> GetCoordsInTargetPattern(EffectPattern targetPattern, Vector3Int startCoords, Vector3Int targetCoords, int size)
        {
            List<Vector3Int> patternCoords = new List<Vector3Int>();

            switch (targetPattern)
            {
                case EffectPattern.Line:
                    return GetCoordsInLine(startCoords, targetCoords);
                case EffectPattern.Ring:
                    return GetCoordsInRing(startCoords, targetCoords, size);
                case EffectPattern.Wall:
                    return GetCoordsInWall(startCoords, targetCoords, size);
                case EffectPattern.Fan:
                    return GetCoordsInFan(startCoords, targetCoords, size);
                case EffectPattern.SpreadV:
                    return GetCoordsInT(startCoords, targetCoords, size, false);
                case EffectPattern.TShape:
                    return GetCoordsInT(startCoords, targetCoords, size);
                case EffectPattern.VShape:
                    return GetCoordsInV(startCoords, targetCoords, size);
                case EffectPattern.Area:
                    return GetCoordsInRange(startCoords, size);
                default:
                    return patternCoords;
            }
        }


        // Get coords for projectile pattern
        public static List<Vector3Int> GetCoordsInProjectilePattern(ProjectilePattern projectilePattern, Vector3Int startCoords, Vector3Int targetCoords, int size)
        {
            List<Vector3Int> patternCoords = new List<Vector3Int>();

            switch (projectilePattern)
            {
                case ProjectilePattern.AOE:
                    return GetCoordsInRange(startCoords, size);
                case ProjectilePattern.Line:
                    return GetCoordsInLine(startCoords, targetCoords, false);
                case ProjectilePattern.Pierce:
                    Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoords);
                    return GetCoordsInLineFromPoint(startCoords, direction, size, false);
                case ProjectilePattern.TShape:
                    return GetCoordsInT(startCoords, targetCoords, size);
                case ProjectilePattern.VShape:
                    return GetCoordsInV(startCoords, targetCoords, size);
                default:
                    return patternCoords;
            }
        }


        // Get coords in line
        public static List<Vector3Int> GetCoordsInLine(Vector3Int startCoords, Vector3Int targetCoords, bool includeStart = true)
        {
            List<Vector3Int> lineCoords = new List<Vector3Int>();

            if (!Direction.CoordsInLine(startCoords, targetCoords) || startCoords == targetCoords)
                return lineCoords;

            // Determine direction
            Vector3Int currentCoords = startCoords;
            Vector3Int direction = Direction.GetLineDirection(startCoords, targetCoords);

            if (!includeStart)
                currentCoords += direction;

            // Move along line
            while (currentCoords != targetCoords)
            {
                lineCoords.Add(currentCoords);
                currentCoords += direction;
            }
            lineCoords.Add(targetCoords);
            return lineCoords;
        }


        // Get coords in line
        public static List<Vector3Int> GetCoordsInLineFromPoint(Vector3Int startCoords, Vector3Int direction, int lineLength, bool includeStart = true)
        {
            List<Vector3Int> lineCoords = new List<Vector3Int>();

            // Determine direction
            Vector3Int currentCoords = startCoords;

            if (includeStart)
                lineCoords.Add(currentCoords);

            // Move along line
            for (int i = 0; i < lineLength; i++)
            {
                currentCoords += direction;
                lineCoords.Add(currentCoords);
            }
            return lineCoords;
        }


        // Get coords in ring
        public static List<Vector3Int> GetCoordsInRing(Vector3Int startCoords, Vector3Int targetCoords, int ringWidth)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (!Direction.CoordsInLine(startCoords, targetCoords) || startCoords == targetCoords)
                return coords;

            Vector3Int currentCoords1 = targetCoords;
            Vector3Int currentCoords2 = targetCoords;
            Vector3Int direction1;
            Vector3Int direction2;
            coords.Add(targetCoords);
            (direction1, direction2) = Direction.GetRingDirection(startCoords, targetCoords);
            int dist = Direction.GetDistanceHexCoords(startCoords, targetCoords);

            // Get coords
            for (int i = 0; i < ringWidth; i++)
            {
                currentCoords1 += direction1;
                currentCoords2 += direction2;
                coords.Add(currentCoords1);
                coords.Add(currentCoords2);
            }

            return coords;
        }


        // Get coords in wall
        public static List<Vector3Int> GetCoordsInWall(Vector3Int startCoords, Vector3Int targetCoords, int wallWidth)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (startCoords == targetCoords)
                return coords;

            if (!Direction.CoordsInDiagonalLine(startCoords, targetCoords))
                return coords;

            Vector3Int currentCoords1 = targetCoords;
            Vector3Int currentCoords2 = targetCoords;
            Vector3Int direction1;
            Vector3Int direction2;
            coords.Add(targetCoords);
            (direction1, direction2) = Direction.GetWallDirection(startCoords, targetCoords);
            int dist = Direction.GetDistanceHexCoords(startCoords, targetCoords);

            // Get coords
            for (int i = 0; i < wallWidth; i++)
            {
                currentCoords1 += direction1;
                currentCoords2 += direction2;
                coords.Add(currentCoords1);
                coords.Add(currentCoords2);
            }

            return coords;
        }


        // Get coords in V shape
        public static List<Vector3Int> GetCoordsInV(Vector3Int startCoords, Vector3Int targetCoords, int vLength)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (startCoords == targetCoords)
                return coords;

            if (!Direction.CoordsInDiagonalLine(startCoords, targetCoords))
                return coords;

            Vector3Int currentCoords1 = targetCoords;
            Vector3Int currentCoords2 = targetCoords;
            Vector3Int direction1;
            Vector3Int direction2;
            coords.Add(targetCoords);
            (direction1, direction2) = Direction.GetVDirection(startCoords, targetCoords);

            // Get coords
            for (int i = 0; i < vLength; i++)
            {
                currentCoords1 += direction1;
                currentCoords2 += direction2;
                coords.Add(currentCoords1);
                coords.Add(currentCoords2);
            }

            return coords;
        }


        // Get coords in T shape
        public static List<Vector3Int> GetCoordsInT(Vector3Int startCoords, Vector3Int targetCoords, int tLength, bool includeMiddle = true)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (startCoords == targetCoords)
                return coords;

            if (!Direction.CoordsInLine(startCoords, targetCoords))
                return coords;

            Vector3Int currentCoords1 = targetCoords;
            Vector3Int currentCoords2 = targetCoords;
            Vector3Int currentCoords3 = targetCoords;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;
            coords.Add(targetCoords);
            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoords);

            // Get coords
            for (int i = 0; i < tLength; i++)
            {
                currentCoords1 += direction1;
                currentCoords2 += direction2;
                currentCoords3 += direction3;
                coords.Add(currentCoords1);
                if (includeMiddle)
                    coords.Add(currentCoords2);
                coords.Add(currentCoords3);
            }

            return coords;
        }


        // Get coords in fan
        public static List<Vector3Int> GetCoordsInFan(Vector3Int startCoords, Vector3Int targetCoords, int fanRadius, bool addTail = true)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (startCoords == targetCoords)
                return coords;

            if (!Direction.CoordsInLine(startCoords, targetCoords))
                return coords;

            Vector3Int currentCoords1 = targetCoords;
            Vector3Int currentCoords2 = targetCoords;
            Vector3Int currentCoords3 = targetCoords;
            Vector3Int direction1;
            Vector3Int direction2;
            Vector3Int direction3;

            if (addTail)
                coords.AddRange(GetCoordsInLine(startCoords, targetCoords, false));

            (direction1, direction2, direction3) = Direction.GetTDirection(startCoords, targetCoords);

            // Get coords
            for (int i = 0; i < fanRadius; i++)
            {
                for (int j = i; j < fanRadius; j++)
                {
                    currentCoords1 += direction1;
                    currentCoords3 += direction3;
                    coords.Add(currentCoords1);
                    coords.Add(currentCoords3);
                }
                currentCoords2 += direction2;
                coords.Add(currentCoords2);
                currentCoords1 = currentCoords2;
                currentCoords3 = currentCoords2;
            }

            return coords;
        }


        // Get coords in lines from point
        public static List<Vector3Int> GetAllCoordsInLineFromPoint(Vector3Int startCoords, int radius)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (radius <= 0)
                return coords;

            Vector3Int currentCoords;
            foreach (Vector3Int direction in Direction.directions)
            {
                currentCoords = startCoords;
                for (int i = 0; i < radius; i++)
                {
                    currentCoords += direction;
                    coords.Add(currentCoords);
                }
            }

            return coords;
        }


        // Get coords in diagonal lines from point
        public static List<Vector3Int> GetAllCoordsInDiagonalFromPoint(Vector3Int startCoords, int radius)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (radius <= 0)
                return coords;

            Vector3Int currentCoords;
            foreach (Vector3Int direction in Direction.diagDirections)
            {
                currentCoords = startCoords;
                for (int i = 0; i < radius; i++)
                {
                    currentCoords += direction;
                    coords.Add(currentCoords);
                }
            }
            return coords;
        }


        // Get coords within range
        public static List<Vector3Int> GetCoordsInRange(Vector3Int startCoords, int range, bool addStart = false)
        {
            List<Vector3Int> coords = new List<Vector3Int>();

            if (range == 0)
                return coords;

            for (int i = -range; i <= range; i++)
            {

                // Get upper and lower bounds for map columns
                int lowerBound = Math.Max(-i - range, -range);
                int upperBound = Math.Min(range, -i + range);

                for (int j = lowerBound; j <= upperBound; j++)
                {
                    int z = -i - j;
                    Vector3Int hexCoords = new Vector3Int(i, j, z) + startCoords;
                    coords.Add(hexCoords);
                }
            }

            // Remove center coords
            if (!addStart)
                coords.Remove(startCoords);

            return coords;
        }
    }
}