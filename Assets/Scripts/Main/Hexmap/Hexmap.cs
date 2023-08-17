using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hexmap : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Hexes
    public HexmapData hexmapData { get; private set; } = new HexmapData();
    public MapType mapType = MapType.Hexagon;
    public int mapRadius = 5;
    public int mapWidth = 5;
    public int mapHeight = 5;

    // Tilemap
    public Tile hexagon;
    public Grid tileGrid;
    public Tilemap hexagonMap;


    // Instantiate hexmap
    public static Hexmap CreateHexmap()
    {
        Hexmap hexmap = Instantiate(Resources.Load<Hexmap>("Prefabs/Hexmap/Hexmap"));
        hexmap.Initialize();
        return hexmap;
    }


    // Initialize
    public void Initialize()
    {
        UpdateMapPattern();
        foreach (Hex hex in hexmapData.hexes.Values)
        {
            PaintTile(hex.tileCoords, hexagon);
            SubscribeHexEvents(hex);
        }
        initialized = true;
    }


    // Reset
    public void Reset()
    {
        hexmapData.Reset();
    }


    // Update map pattern
    public void UpdateMapPattern()
    {
        MapPattern mapPattern = MapPattern.CreateMapPattern(mapType, mapRadius, mapWidth, mapHeight);
        hexmapData.SetMapPattern(mapPattern);
    }


    // Set hexmap data
    public void SetHexmapData(HexmapData hexmapData)
    {
        // Unsubscribe from previous hex events
        if (this.hexmapData != null)
        {
            foreach (Hex hex in this.hexmapData.hexes.Values)
            {
                UnsubscribeHexEvents(hex);
            }
        }

        // Set new hex tiles
        this.hexmapData = hexmapData;
        foreach (Hex hex in hexmapData.hexes.Values)
        {
            PaintTile(hex.tileCoords, hexagon);
            SubscribeHexEvents(hex);
        }
    }


    // Subscribe to hex events
    private void SubscribeHexEvents(Hex hex)
    {
        if (hex == null)
            throw new System.ArgumentNullException("Cannot subscribe to null hex events.");

        hex.eventManager.onSetTile.Subscribe(HandleHexSetTile);
    }
    private void UnsubscribeHexEvents(Hex hex)
    {
        if (hex == null)
            return;

        hex.eventManager.onSetTile.Unsubscribe(HandleHexSetTile);
    }


    // Get hex at world position
    public Hex GetHexAtWorldPosition(Vector3 worldPosition)
    {
        Vector3Int tileCoords = tileGrid.WorldToCell(worldPosition);
        tileCoords.z = 0;
        return GetHexAtTileCoords(tileCoords);
    }


    // Get hex from tile coords
    public Hex GetHexAtTileCoords(Vector3Int tileCoords)
    {
        return hexmapData.GetHexAtTileCoords(tileCoords);
    }


    // Get hex from hex coords
    public Hex GetHexAtHexCoords(Vector3Int hexCoords)
    {
        return hexmapData.GetHexAtHexCoords(hexCoords);
    }


    // Get tile coords from world coords
    public Vector3Int WorldToTileCoords(Vector3 worldPosition)
    {
        return tileGrid.WorldToCell(worldPosition);
    }


    // Get world coords from hex coords
    public Vector3 HexToWorldCoords(Vector3Int hexCoords)
    {
        return tileGrid.CellToWorld(Direction.HexToTileCoords(hexCoords));
    }


    // Get world coords from tile coords
    public Vector3 TileToWorldCoords(Vector3Int tileCoords)
    {
        return tileGrid.CellToWorld(tileCoords);
    }


    // Paint tile
    public void PaintTile(Vector3Int tileCoords, TileBase tile)
    {
        hexagonMap.SetTile(tileCoords, tile);
        hexagonMap.RefreshTile(tileCoords);
    }


    // Handle when a hex sets its tile
    public void HandleHexSetTile(Hex hex)
    {
        PaintTile(hex.tileCoords, hex.tile);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
