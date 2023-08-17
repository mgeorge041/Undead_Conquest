using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexmapData
{
    // Hexes
    public int numHexes => hexes.Count;
    public Dictionary<Vector3Int, Hex> hexes = new Dictionary<Vector3Int, Hex>();
    public Dictionary<Vector3Int, Vector3Int> tileHexCoords = new Dictionary<Vector3Int, Vector3Int>();

    // Map info
    public MapPattern mapPattern;

    // Map pieces
    public List<Piece> pieces = new List<Piece>();


    // Constructor
    public HexmapData() 
    {
        if (mapPattern == null)
        {
            mapPattern = MapPattern.CreateMapPattern(MapType.Hexagon, 5);
        }
        CreateHexes();
    }


    // Reset
    public void Reset()
    {
        ClearHexEvents();
        foreach (Hex hex in hexes.Values)
        {
            hex.Reset();
        }
        hexes.Clear();
        tileHexCoords.Clear();
        pieces.Clear();
    }


    // Clear hex events
    private void ClearHexEvents()
    {
        foreach (Hex hex in hexes.Values)
        {
            hex.eventManager.onAddPiece.Unsubscribe(HandleHexAddNewPiece);
        }
    }


    // Set map pattern
    public void SetMapPattern(MapPattern mapPattern)
    {
        this.mapPattern = mapPattern;
        CreateHexes();
    }


    // Create hexes
    private void CreateHexes()
    {
        // Create hexes
        ClearHexEvents();
        hexes.Clear();
        tileHexCoords.Clear();
        foreach (Vector3Int hexCoords in mapPattern.hexCoords)
        {
            Hex hex = new Hex(hexCoords);
            hexes[hexCoords] = hex;
            tileHexCoords[hex.tileCoords] = hex.hexCoords;
            hex.eventManager.onAddPiece.Subscribe(HandleHexAddNewPiece);
        }

        // Set hex neighbors
        foreach (Hex hex in hexes.Values)
        {
            foreach (Vector3Int direction in Direction.directions)
            {
                Hex neighbor;
                Vector3Int neighborCoords = hex.pathNode.GetNeighborCoords(direction);
                if (hexes.TryGetValue(neighborCoords, out neighbor))
                    hex.SetNeighbor(direction, neighbor);
            }
        }
    }


    // Get hexes
    public Hex GetHexAtHexCoords(Vector3Int hexCoords)
    {
        Hex hex;
        if (hexes.TryGetValue(hexCoords, out hex))
            return hex;

        return null;
    }

    public Hex GetHexAtTileCoords(Vector3Int tileCoords)
    {
        Hex hex;
        Vector3Int hexCoords;
        if (!tileHexCoords.TryGetValue(tileCoords, out hexCoords))
            return null;

        if (hexes.TryGetValue(hexCoords, out hex))
            return hex;

        return null;
    }


    // Add piece to map
    public void AddPiece(Piece piece, Vector3Int hexCoords)
    {
        if (piece == null || !hexes.ContainsKey(hexCoords))
            return;

        Hex hex = GetHexAtHexCoords(hexCoords);
        hex.SetPiece(piece);
        piece.SetHex(hex);
        piece.SetHexPosition(hex);
        pieces.Add(piece);
    }
    public void RemovePiece(Piece piece)
    {
        piece.hex.ClearPiece();
        piece.SetHex(null);
        pieces.Remove(piece);
    }


    // Handle when a hex adds a new piece to the map
    private void HandleHexAddNewPiece(Piece piece)
    {
        if (piece == null)
            throw new System.ArgumentNullException("Cannot add null piece to hexmap data.");

        if (pieces.Contains(piece))
            return;

        pieces.Add(piece);
        piece.eventManager.onDeath.Subscribe(HandlePieceDeath);
    }


    // Handle when a piece dies
    public void HandlePieceDeath(Piece piece)
    {
        if (piece == null || !pieces.Contains(piece))
            throw new System.ArgumentException("Cannot handle death for null or unknown piece.");

        RemovePiece(piece);
        piece.eventManager.onDeath.Unsubscribe(HandlePieceDeath);
    }
}
