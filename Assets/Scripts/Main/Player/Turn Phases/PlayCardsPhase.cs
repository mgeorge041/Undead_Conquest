using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayCardsPhase : TurnPhase
{
    // Phase info
    public override TurnPhaseType phaseType => TurnPhaseType.PlayCards;
    public override TurnPhaseType nextPhaseType => TurnPhaseType.MapAction;

    // Card info
    private PlayableCard selectedCard;

    // Player
    public List<Hex> playableHexes { get; private set; } = new List<Hex>();

    // Playable area outlines
    private Hex currentHoverHex;
    public LineRenderer hoverLine { get; private set; }
    public LineRenderer pieceHoverLine { get; private set; }
    public List<LineRenderer> edgeLines { get; protected set; } = new List<LineRenderer>();

    // Event manager
    public override TurnPhaseEventManager eventManager => playCardsEventManager;
    public PlayCardsPhaseEventManager playCardsEventManager { get; private set; } = new PlayCardsPhaseEventManager();


    // Constructor
    public PlayCardsPhase() { }
    public PlayCardsPhase(PlayerItemManager itemManager)
    {
        this.itemManager = itemManager;
        itemManager.eventManager.onLeftClickCard.Subscribe(SetSelectedCard);
    }


    // Start phase
    public override void StartPhase()
    {
        Debug.Log("Starting play cards phase.");
        CreateHoverLines();
        ClearPhase();
        SetPlayableHexes();
    }
    public override void ClearPhase()
    {
        selectedCard = null;
        ShowHoverLine(false);
        ShowPieceHoverLine(false);
        ClearPlayableHexes();
        itemManager.hand.SetSelectedCard(null);
    }


    // Clicks
    public override void LeftClick(Hex clickHex)
    {
        if (clickHex == null || !clickHex.hasPiece)
        {
            itemManager.pieceManager.SetSelectedPiece(null);
            ShowPieceHoverLine(false);
        }
        else
        {
            itemManager.pieceManager.SetSelectedPiece(clickHex.piece);
            SetPieceHoverLine(new List<Hex>() { clickHex });
            ShowPieceHoverLine(true);
        }
    }

    public override void RightClick(Hex clickHex)
    {
        if (playableHexes.Contains(clickHex))
        {
            PlayPieceAtHex(clickHex);
            return;
        }
    }


    // Hover
    public override void Hover(Hex hoverHex) 
    {
        if (hoverHex == null || !playableHexes.Contains(hoverHex) || selectedCard == null)
        {
            hoverLine.gameObject.SetActive(false);
            currentHoverHex = hoverHex;
            return;
        }

        if (hoverHex == currentHoverHex)
            return;

        // Show hex outline
        currentHoverHex = hoverHex;
        List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(new List<Hex>() { currentHoverHex });
        foreach (List<Vector3> linePoints in edgePoints)
        {
            PathEdge.DisplayEdgeLine(hoverLine, linePoints);
        }
    }


    // Set selected card
    public void SetSelectedCard(Card card)
    {
        ShowHoverLine(false);
        ClearPlayableHexes();
        if (card == null || card == selectedCard) 
        {
            selectedCard = null;
            return;
        }

        // Show playable hexes
        selectedCard = (PlayableCard)card;
        if (card.playable)
        {
            SetPlayableHexes();
            ShowPlayableHexes();
        }       
    }

    
    // Create hover lines for mouse position and selecting pieces
    private void CreateHoverLines()
    {
        // First time creation
        if (hoverLine == null)
            hoverLine = PathEdge.CreateEdgeLine(HexListType.Deploy);
        if (pieceHoverLine == null)
            pieceHoverLine = PathEdge.CreateEdgeLine(HexListType.Select);

        ShowHoverLine(false);
    }


    // Hide hover line game object
    private void ShowHoverLine(bool show)
    {
        hoverLine.gameObject.SetActive(show);
    }
    private void ShowPieceHoverLine(bool show)
    {
        pieceHoverLine.gameObject.SetActive(show);
    }


    // Set playable hexes
    private void SetPlayableHexes()
    {
        playableHexes.Clear();

        // Remove domain hexes with pieces
        foreach (Hex domainHex in itemManager.pieceManager.domainHexes)
        {
            if (!domainHex.hasPiece)
                playableHexes.Add(domainHex);
        }
    }


    // Show playable hexes for selected card
    private void ShowPlayableHexes()
    {
        if (selectedCard == null)
            return;

        CreateHexListEdgeLines(HexListType.Deploy, playableHexes);
    }


    // Play piece at hex
    private void PlayPieceAtHex(Hex hex)
    {
        Piece piece = Piece.CreatePiece(selectedCard.playableCardInfo);
        hex.AddNewPiece(piece);
        piece.SetHexAndPosition(hex);
        itemManager.PlayCard(selectedCard);
        itemManager.pieceManager.AddPiece(piece);
        ClearPlayableHexes();
    }


    // Create edge lines
    private void CreateHexListEdgeLines(HexListType hexListType, List<Hex> hexList)
    {
        List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(hexList);
        
        // Create needed lines
        int numNeededLines = edgePoints.Count - edgeLines.Count;
        for (int i = 0; i < numNeededLines; i++) 
        {
            LineRenderer line = PathEdge.CreateEdgeLine(hexListType);
            edgeLines.Add(line);
        }

        // Set line points
        for (int i = 0; i < edgeLines.Count; i++)
        {
            if (i < edgePoints.Count)
            {
                List<Vector3> linePoints = edgePoints[i];
                PathEdge.DisplayEdgeLine(edgeLines[i], linePoints);
            }
            else
            {
                edgeLines[i].gameObject.SetActive(false);
            }
            
        }
    }


    // Hide game object for each edge line
    private void ClearHexListEdgeLines()
    {
        foreach (LineRenderer line in edgeLines)
        {
            line.gameObject.SetActive(false);
        }
    }


    // Set selected piece hover line
    private void SetPieceHoverLine(List<Hex> hex)
    {
        List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(hex);
        PathEdge.DisplayEdgeLine(pieceHoverLine, edgePoints[0]);
    }


    // Clear playable hexes for selected card
    private void ClearPlayableHexes()
    {
        playableHexes.Clear();
        ClearHexListEdgeLines();
    }
}
