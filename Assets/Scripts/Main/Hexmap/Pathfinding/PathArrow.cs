using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class PathArrow
    {
        // Sprite dictionary
        private static Dictionary<Vector3Int, string> pathSprites = new Dictionary<Vector3Int, string>()
        {
            { Direction.U, "Path Arrows_4" },
            { Direction.UR, "Path Arrows_5" },
            { Direction.DR, "Path Arrows_5" },
            { Direction.D, "Path Arrows_4" },
            { Direction.DL, "Path Arrows_5" },
            { Direction.UL, "Path Arrows_5" },
            { Vector3Int.zero, "Path Arrows_0" },
            { Vector3Int.one, "Path Arrows_1" },
        };
        private static Dictionary<Vector3Int, string> arrowSprites = new Dictionary<Vector3Int, string>()
        {
            { Direction.U, "Path Arrows_3" },
            { Direction.UR, "Path Arrows_2" },
            { Direction.DR, "Path Arrows_2" },
            { Direction.D, "Path Arrows_3" },
            { Direction.DL, "Path Arrows_2" },
            { Direction.UL, "Path Arrows_2" },
        };
        private static Dictionary<Vector3Int, Vector3> pathScales = new Dictionary<Vector3Int, Vector3>()
        {
            { Direction.U, new Vector3(1, -1, 1) },
            { Direction.UR, new Vector3(1, 1, 1) },
            { Direction.DR, new Vector3(1, -1, 1) },
            { Direction.D, new Vector3(1, 1, 1) },
            { Direction.DL, new Vector3(-1, -1, 1) },
            { Direction.UL, new Vector3(-1, 1, 1) },
            { Vector3Int.zero, new Vector3(1, 1, 1) },
        };
        private static Dictionary<Vector3Int, Vector3> arrowScales = new Dictionary<Vector3Int, Vector3>()
        {
            { Direction.U, new Vector3(1, -1, 1) },
            { Direction.UR, new Vector3(-1, -1, 1) },
            { Direction.DR, new Vector3(-1, 1, 1) },
            { Direction.D, new Vector3(1, 1, 1) },
            { Direction.DL, new Vector3(1, 1, 1) },
            { Direction.UL, new Vector3(1, -1, 1) },
        };


        // Load path arrow sprite
        private static Sprite LoadPathSegmentSprite(string segmentName)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Map/Tiles/Path Arrows");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == segmentName)
                {
                    return sprite;
                }
            }
            return null;
        }


        // Load sprite object
        public static SpriteRenderer LoadPathSegment(Vector3Int directionIn, Vector3Int directionOut)
        {
            SpriteRenderer pathSegment = GameObject.Instantiate(Resources.Load<SpriteRenderer>("Prefabs/Map/Move Path Segment"));
            Sprite sprite;
            Vector3Int sum = directionIn + directionOut;
            if (directionOut != Vector3Int.zero)
            {
                // Straight through
                if (sum == Vector3Int.zero)
                {
                    if (directionIn == Direction.U || directionIn == Direction.D)
                    {
                        sprite = LoadPathSegmentSprite(pathSprites[sum]);
                        pathSegment.transform.localScale = pathScales[sum];
                    }
                    else if (directionIn == Direction.UR || directionIn == Direction.DL)
                    {
                        sprite = LoadPathSegmentSprite(pathSprites[Vector3Int.one]);
                        pathSegment.transform.localScale = pathScales[sum];
                    }
                    else
                    {
                        sprite = LoadPathSegmentSprite(pathSprites[Vector3Int.one]);
                        pathSegment.transform.localScale = pathScales[Direction.DR];
                    }
                }
                else
                {
                    sprite = LoadPathSegmentSprite(pathSprites[sum]);
                    pathSegment.transform.localScale = pathScales[sum];
                }
            }
            else
            {
                sprite = LoadPathSegmentSprite(arrowSprites[directionIn]);
                pathSegment.transform.localScale = arrowScales[directionIn];
            }

            pathSegment.sprite = sprite;
            return pathSegment;
        }
    }
}