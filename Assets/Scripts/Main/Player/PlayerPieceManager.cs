using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerPieceManager
{
    // Map items
    public List<Piece> pieces = new List<Piece>();
    public List<Hex> domainHexes = new List<Hex>();
    public LineRenderer domainEdgeLine;
    private Piece selectedPiece;

    // Event manager
    public PlayerPieceManagerEventManager eventManager { get; private set; } = new PlayerPieceManagerEventManager();


    // Constructor
    public PlayerPieceManager()
    {
        domainEdgeLine = PathEdge.CreateEdgeLine(HexListType.Ability);
        domainEdgeLine.gameObject.SetActive(false);
    }


    // Reset
    public void Reset()
    {
        pieces.Clear();
        domainHexes.Clear();
        domainEdgeLine.gameObject.SetActive(false);
        selectedPiece = null;
    }


    // End turn
    public void EndTurn()
    {
        foreach (Piece piece in pieces)
        {
            piece.EndTurn();
        }
    }


    // Get whether has piece
    public bool HasPiece(Piece piece)
    {
        return pieces.Contains(piece);
    }


    // Set selected piece
    public void SetSelectedPiece(Piece piece)
    {
        selectedPiece = piece;
        eventManager.onSetSelectedPiece.OnEvent(piece);
    }


    // Add or remove piece
    public void AddPiece(Piece piece)
    {
        if (piece == null)
            throw new ArgumentNullException("Cannot add null piece.");

        pieces.Add(piece);
        eventManager.onAddPiece.OnEvent(piece);
        piece.eventManager.onDeath.Subscribe(HandlePieceDeath);

        if (piece.isUnit)
        {
            piece.unit.unitEventManager.onFinishMove.Subscribe(HandleUnitFinishMove);
            HandleHexSetPiece(piece);
        }

        // Set domain hexes
        if (piece.pieceType != PieceType.Building)
            return;

        UpdateDomainHexes((Building)piece);
    }
    public void RemovePiece(Piece piece)
    {
        if (piece == null || !pieces.Contains(piece))
            throw new ArgumentNullException("Cannot remove null nor unknown piece.");

        pieces.Remove(piece);
        piece.eventManager.onDeath.Unsubscribe(HandlePieceDeath);
        
        if (piece.isUnit)
            piece.unit.unitEventManager.onFinishMove.Unsubscribe(HandleUnitFinishMove);

        if (piece.isBuilding)
            SetDomainHexes();

        eventManager.onRemovePiece.OnEvent(piece);
    }


    // Handle piece death
    public void HandlePieceDeath(Piece piece)
    {
        if (piece == null || !pieces.Contains(piece))
            throw new ArgumentException("Cannot handle death for null or unknown piece.");

        RemovePiece(piece);
    }


    // Update player domain hexes
    private void SetDomainHexes()
    {
        // Unsubscribe from previous domain hexes
        foreach (Hex hex in domainHexes)
        {
            hex.eventManager.onSetPiece.Unsubscribe(HandleHexSetPiece);
        }

        // Set new domain hexes
        domainHexes.Clear();
        foreach (Piece piece in pieces)
        {
            if (piece.pieceType != PieceType.Building)
                continue;

            Building building = (Building)piece;
            UpdateDomainHexes(building);
        }
    }
    private void UpdateDomainHexes(Building building)
    {
        if (building == null)
            throw new ArgumentNullException("Building cannot be null when updating domain hexes.");

        if (building.hex == null)
            throw new ArgumentNullException("Building cannot have null hex when updating domain hexes.");

        List<Hex> buildingDomainHexes = HexPathPattern.GetHexesInRange(building.hex, building.buildingData.domainRange);
        foreach (Hex domainHex in buildingDomainHexes)
        {
            if (!domainHexes.Contains(domainHex))
            {
                domainHexes.Add(domainHex);
                domainHex.eventManager.onSetPiece.Subscribe(HandleHexSetPiece);
            }
        }

        // Set new edge line for player domain
        List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(domainHexes);
        PathEdge.DisplayEdgeLine(domainEdgeLine, edgePoints[0]);
    }


    // Handle when a unit moves (potentially into or out of domain)
    public void HandleUnitFinishMove(Unit unit)
    {
        if (unit == null)
            throw new ArgumentNullException("Cannot handle unit move for null unit.");

        unit.SetDomainBuff(domainHexes.Contains(unit.hex));
    }


    // Handle when a domain hex sets a piece
    public void HandleHexSetPiece(Piece piece)
    {
        if (!pieces.Contains(piece) || !piece.isUnit || !domainHexes.Contains(piece.hex))
            return;

        piece.unit.SetDomainBuff(true);
    }
}
