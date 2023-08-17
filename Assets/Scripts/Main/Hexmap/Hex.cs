using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hex
{
    // Sizing
    public const int HEX_WIDTH = 32;
    public const int HEX_HEIGHT = 32;
    public const float HEX_WIDTH_F = HEX_WIDTH / 100f;
    public const float HEX_HEIGHT_F = HEX_HEIGHT / 100f;

    // Coords
    public Vector3Int hexCoords { get; private set; }
    public Vector3Int tileCoords { get; private set; }

    // Tile
    public Tile tile;

    // Piece info
    public Piece piece { get; private set; }
    public bool hasPiece => piece != null;
    public Unit unit => hasUnit ? (Unit)piece : null;
    public bool hasUnit => hasPiece && piece.pieceType == PieceType.Unit;
    public Building building => hasBuilding ? (Building)piece : null;
    public bool hasBuilding => hasPiece && piece.pieceType == PieceType.Building;

    // Pathfinding variables
    public HexPathNode pathNode = new HexPathNode();

    // Event manager
    public HexEventManager eventManager { get; private set; } = new HexEventManager();


    // Constructor
    public Hex() { }
    public Hex(Vector3Int hexCoords)
    {
        this.hexCoords = hexCoords;
        tileCoords = Direction.HexToTileCoords(hexCoords);
        pathNode = new HexPathNode(this);
    }


    // Reset
    public void Reset()
    {
        piece = null;
        hexCoords = Vector3Int.zero;
        tileCoords = Vector3Int.zero;
        pathNode.Reset();
    }


    // Get path nodes
    public static List<HexPathNode> GetPathNodes(List<Hex> hexes)
    {
        List<HexPathNode> pathNodes = new List<HexPathNode>();
        foreach (Hex hex in hexes)
        {
            pathNodes.Add(hex.pathNode);
        }
        return pathNodes;
    }


    // Set hex neighbor
    public void SetNeighbor(Vector3Int neighborCoords, Hex neighborHex)
    {
        pathNode.SetNeighbor(neighborCoords, neighborHex.pathNode);
    }


    // Add piece to hex that is new to the map
    public void AddNewPiece(Piece piece)
    {
        SetPiece(piece);
        eventManager.onAddPiece.OnEvent(piece);
    }


    // Set piece at hex
    public void SetPiece(Piece piece)
    {
        this.piece = piece;
        eventManager.onSetPiece.OnEvent(piece);
    }
    public void ClearPiece()
    {
        piece = null;
        eventManager.onSetPiece.OnEvent(piece);
    }


    // Set hex tile
    public void SetTile(Tile tile)
    {
        this.tile = tile;
        eventManager.onSetTile.OnEvent(this);
    }
}
