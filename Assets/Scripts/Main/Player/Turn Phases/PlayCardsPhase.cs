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
        CreateHoverLine();
        ClearPhase();
        SetPlayableHexes();
    }
    public override void ClearPhase()
    {
        selectedCard = null;
        ClearHoverLine();
        ClearPlayableHexes();
    }


    // Clicks
    public override void LeftClick(Hex clickHex)
    {
        if (clickHex == null || !clickHex.hasPiece)
            itemManager.SetSelectedPiece(null);
        else
            itemManager.SetSelectedPiece(clickHex.piece);
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
        ClearHoverLine();
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

    
    // Create hover line for mouse position
    private void CreateHoverLine()
    {
        // First time creation
        if (hoverLine == null)
            hoverLine = PathEdge.CreateEdgeLine(HexListType.Deploy);

        ClearHoverLine();
    }


    // Hide hover line game object
    private void ClearHoverLine()
    {
        hoverLine.gameObject.SetActive(false);
    }


    // Set playable hexes
    private void SetPlayableHexes()
    {
        playableHexes.Clear();

        // Remove domain hexes with pieces
        foreach (Hex domainHex in itemManager.domainHexes)
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
        hex.SetPiece(piece);
        piece.SetHexAndPosition(hex);
        itemManager.PlayCard(selectedCard);
        itemManager.AddPiece(piece);
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


    // Clear playable hexes for selected card
    private void ClearPlayableHexes()
    {
        playableHexes.Clear();
        ClearHexListEdgeLines();
    }
}
