using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    Diamond,
    Hexagon,
    Rectangle,
    None
}


public abstract class MapPattern
{
    // Map info
    public virtual int mapWidth { get; protected set; }
    public virtual int mapHeight { get; protected set; }
    public int mapPixelWidth { get; protected set; }
    public int mapPixelHeight { get; protected set; }
    public MapType mapType { get; protected set; }
    public int numHexes { get { return hexCoords.Count; } }
    public List<Vector3Int> hexCoords { get; protected set; } = new List<Vector3Int>();
    public Dictionary<Vector3Int, Vector3Int> worldPositionDict { get; protected set; } = new Dictionary<Vector3Int, Vector3Int>();


    // Constructor
    public MapPattern() { }

    public abstract void CreateMap();
    public abstract void CalculateMapSize();


    // Create map pattern
    public static MapPattern CreateMapPattern(MapType mapType, int mapRadius)
    {
        switch (mapType)
        {
            case MapType.Diamond:
                return new DiamondMapPattern(mapRadius, mapRadius);
            case MapType.Hexagon:
                return new HexagonMapPattern(mapRadius);
            case MapType.Rectangle:
                return new RectangleMapPattern(mapRadius, mapRadius);
             default:
                return new HexagonMapPattern(mapRadius);
        }
    }
    public static MapPattern CreateMapPattern(MapType mapType, int mapWidth, int mapHeight)
    {
        switch (mapType)
        {
            case MapType.Diamond:
                return new DiamondMapPattern(mapWidth, mapHeight);
            case MapType.Hexagon:
                return new HexagonMapPattern(mapWidth);
            case MapType.Rectangle:
                return new RectangleMapPattern(mapWidth, mapHeight);
            default:
                return new HexagonMapPattern(mapWidth);
        }
    }
    public static MapPattern CreateMapPattern(MapType mapType, int mapRadius, int mapWidth, int mapHeight)
    {
        switch (mapType)
        {
            case MapType.Diamond:
                return new DiamondMapPattern(mapWidth, mapHeight);
            case MapType.Hexagon:
                return new HexagonMapPattern(mapRadius);
            case MapType.Rectangle:
                return new RectangleMapPattern(mapWidth, mapHeight);
            default:
                return new HexagonMapPattern(mapRadius);
        }
    }


    // Print hex coords
    public void PrintMapPattern()
    {
        foreach (Vector3Int coords in hexCoords)
        {
            Debug.Log("Hex coords: " + coords);
        }
    }


    // Calculate map width
    protected int CalculateMapPixelWidth(int numColumns)
    {
        return (numColumns - 1) * (3 * Hex.HEX_WIDTH / 4) + Hex.HEX_WIDTH;
    }
}


public class HexagonMapPattern : MapPattern
{
    public int mapRadius { get; private set; }
    public override int mapWidth { get { return mapRadius * 2 + 1; } }
    public override int mapHeight { get { return mapRadius * 2 + 1; } }


    // Constructor
    public HexagonMapPattern(int mapRadius)
    {
        mapType = MapType.Hexagon;
        this.mapRadius = mapRadius;
        CreateMap();
        CalculateMapSize();
    }


    // Create map
    public override void CreateMap()
    {
        for (int i = -mapRadius; i <= mapRadius; i++)
        {
            // Get upper and lower bounds for map columns
            int lowerBound = Mathf.Max(-i - mapRadius, -mapRadius);
            int upperBound = Mathf.Min(mapRadius, -i + mapRadius);

            for (int j = lowerBound; j <= upperBound; j++)
            {
                // Get z coordinate
                int z = -i - j;
                Vector3Int newHexCoords = new Vector3Int(i, j, z);
                hexCoords.Add(newHexCoords);
                worldPositionDict[newHexCoords] = new Vector3Int(
                    24 * newHexCoords.x,
                    16 * (newHexCoords.z - newHexCoords.y),
                    0
                );
            }
        }
    }


    // Calculate map size
    public override void CalculateMapSize()
    {
        mapPixelWidth = CalculateMapPixelWidth(mapRadius * 2 + 1);
        mapPixelHeight = Hex.HEX_HEIGHT * (2 * mapRadius + 1);
    }
}


public class RectangleMapPattern : MapPattern
{
    // Constructor
    public RectangleMapPattern(int mapWidth, int mapHeight)
    {
        mapType = MapType.Rectangle;
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        CreateMap();
        CalculateMapSize();
    }


    // Create map
    public override void CreateMap()
    {
        // Get coordinate ranges
        int xMin = -(mapWidth - 1) / 2;
        int xMax = mapWidth / 2;
        int yMinCenter;
        int yMaxCenter;
        if (mapHeight % 2 != 0)
        {
            yMinCenter = -(mapHeight - 1) / 2;
            yMaxCenter = -yMinCenter;
        }
        else
        {
            yMinCenter = -(mapHeight - 2) / 2;
            yMaxCenter = -yMinCenter + 1;
        }

        // Create hex coords
        for (int i = xMin; i <= xMax; i++)
        {
            int yMinOffset = Mathf.FloorToInt((-i + 1) / 2f);
            int yMaxOffset = Mathf.FloorToInt((i + 1) / 2f);
            int lowerBound = yMinOffset + yMinCenter;
            int upperBound = -yMaxOffset + yMaxCenter;

            for (int j = lowerBound; j <= upperBound; j++)
            {
                int z = -i - j;
                Vector3Int newHexCoords = new Vector3Int(i, j, z);
                hexCoords.Add(newHexCoords);
                worldPositionDict[newHexCoords] = new Vector3Int(
                    24 * newHexCoords.x,
                    16 * (newHexCoords.z - newHexCoords.y),
                    0
                );
            }
        }
    }


    // Calculate map size
    public override void CalculateMapSize()
    {
        //mapPixelWidth = (mapWidth + 1) / 2 * Hex.HEX_WIDTH + (mapWidth / 2 * Hex.HEX_WIDTH / 2);
        mapPixelWidth = CalculateMapPixelWidth(mapWidth);
        mapPixelHeight = Hex.HEX_HEIGHT * mapHeight;
    }
}


public class DiamondMapPattern : MapPattern
{
    public bool slopeUp { get; private set; }


    // Constructor
    public DiamondMapPattern(int mapWidth, int mapHeight, bool slopeUp = true)
    {
        mapType = MapType.Diamond;
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
        this.slopeUp = slopeUp;
        CreateMap();
        CalculateMapSize();
    }


    // Create map
    public override void CreateMap()
    {
        // Get coordinate ranges
        int xMin = -(mapWidth - 1) / 2;
        int xMax = mapWidth / 2;
        int lowerBound;
        int upperBound;
        if (slopeUp)
        {
            lowerBound = -(mapHeight - 1) / 2;
            upperBound = mapHeight / 2;
        }
        else
        {
            lowerBound = -(mapHeight / 2);
            upperBound = (mapHeight - 1) / 2;
        }

        // Create hex coords
        for (int i = xMin; i <= xMax; i++)
        {
            for (int j = lowerBound; j <= upperBound; j++)
            {
                int z = -i - j;
                Vector3Int newHexCoords;
                if (slopeUp)
                {
                    newHexCoords = new Vector3Int(i, z, j);
                }
                else
                {
                    newHexCoords = new Vector3Int(i, j, z);
                }
                hexCoords.Add(newHexCoords);
                worldPositionDict[newHexCoords] = new Vector3Int(
                    24 * newHexCoords.x,
                    16 * (newHexCoords.z - newHexCoords.y),
                    0
                );
            }
        }
    }


    // Calculate map size
    public override void CalculateMapSize()
    {
        mapPixelWidth = CalculateMapPixelWidth(mapWidth);
        mapPixelHeight = mapHeight * Hex.HEX_HEIGHT + (mapWidth - 1) * Hex.HEX_HEIGHT / 2;
    }
}