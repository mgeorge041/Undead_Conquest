using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace MapActionPhaseStates
{
    public class ActionState : PhaseState
    {
        // State info
        public override StateType stateType => StateType.Action;

        // Piece info
        private Piece piece;
        public List<Hex> reachableHexes { get; private set; } = new List<Hex>();
        public List<Hex> moveHexes { get; private set; } = new List<Hex>();
        public List<Hex> attackHexes { get; private set; } = new List<Hex>();
        public List<Hex> attackableHexes { get; private set; } = new List<Hex>();

        // Hover info
        private Hex currentHoverHex;
        public LineRenderer hoverLine { get; private set; }


        // Constructor
        public ActionState(PlayerItemManager itemManager)
        {
            this.itemManager = itemManager;
        }


        // Start state
        public override void StartState(StateStartInfo startInfo)
        {
            CreateHoverLine();
            ClearState();
            
            piece = startInfo.piece;

            if (piece == null)
                throw new System.ArgumentNullException("Cannot set info state for null piece.");

            reachableHexes = new List<Hex>() { piece.hex };
            if (piece.isUnit)
            {
                SetMoveHexes();
                reachableHexes.AddRange(moveHexes);

                // Show hex when done moving but can still attack
                if (moveHexes.Count > 0)
                    CreateHexListEdgeLines(HexListType.Move, moveHexes);
                else
                    CreateHexListEdgeLines(HexListType.Move, reachableHexes);
            }
            else
            {
                CreateHexListEdgeLines(HexListType.Select, reachableHexes);
            }

            SetAttackHexes();
            CreateHexListEdgeLines(HexListType.Attack, attackHexes);
            itemManager.SetSelectedPiece(piece);
        }


        // End state
        public override void EndState(StateStartInfo startInfo)
        {
            base.EndState(startInfo);
        }


        // Clear state
        public override void ClearState()
        {
            piece = null;
            reachableHexes.Clear();
            moveHexes.Clear();
            attackHexes.Clear();
            ClearHexListEdgeLines();
            ClearHoverLine();
        }


        // Clicks
        public override void LeftClick(Hex clickHex)
        {
            if (clickHex == null || !clickHex.hasPiece || clickHex.piece == piece)
            {
                EndState(new StateStartInfo(StateType.Neutral));
                return;
            }

            if (itemManager.HasPiece(clickHex.piece) && clickHex.piece.pieceData.hasActions)
                EndState(new StateStartInfo(StateType.Action, clickHex.piece));
            else
                EndState(new StateStartInfo(StateType.Info, clickHex.piece));
        }

        public override void RightClick(Hex clickHex)
        {
            if (clickHex == null)
                return;

            if (moveHexes.Contains(clickHex))
            {
                PieceActionData actionData = new PieceActionData(piece, PieceActionType.Move, piece.hex, clickHex);
                eventManager.OnCreatePieceActionData(actionData);
            }
            else if (attackHexes.Contains(clickHex))
            {
                CheckAttackMoveHex(clickHex);
            }
            else
            {
                return;
            }
            eventManager.OnPerformPieceActions();
            itemManager.SetSelectedPiece(piece);
            EndState(new StateStartInfo(StateType.Neutral));
        }


        // Hover
        public override void Hover(Hex hoverHex)
        {
            // TODO
            if (hoverHex == null || (!moveHexes.Contains(hoverHex) && !attackHexes.Contains(hoverHex)))
            {
                currentHoverHex = hoverHex;
                ClearHoverLine();
                return;
            }

            currentHoverHex = hoverHex;
            ShowHoverLine();
        }


        // Set move hexes for unit
        private void SetMoveHexes()
        {
            moveHexes = HexActionCalculator.GetMoveHexes((Unit)piece);
        }


        // Set attack hexes for piece
        private void SetAttackHexes()
        {
            attackableHexes = HexActionCalculator.GetAttackableHexes(piece);
            attackHexes = HexActionCalculator.GetAttackHexes(piece);
        }


        // Check whether attack hex is within range or unit needs to move to attack
        private void CheckAttackMoveHex(Hex targetHex)
        {
            List<Hex> attackRangeHexes = HexActionCalculator.GetAttackRangeHexes(targetHex, piece.pieceData.range);
            if (attackRangeHexes.Contains(piece.hex))
            {
                PieceActionData actionData = new PieceActionData(piece, PieceActionType.Attack, piece.hex, targetHex);
                eventManager.OnCreatePieceActionData(actionData);
            }
            else
            {
                // Get attack move hexes
                List<Hex> attackMoveHexes = new List<Hex>();
                foreach (Hex attackRangeHex in attackRangeHexes)
                {
                    if (moveHexes.Contains(attackRangeHex))
                        attackMoveHexes.Add(attackRangeHex);
                }

                // Get closest hex
                List<Hex> minPath = HexPathfinding.GetMinPath(piece.hex.pathNode, Hex.GetPathNodes(attackMoveHexes));
                PieceActionData moveActionData = new PieceActionData(piece, PieceActionType.Move, piece.hex, minPath[minPath.Count - 1]);
                PieceActionData attackActionData = new PieceActionData(piece, PieceActionType.Attack, minPath[minPath.Count - 1], targetHex);
                eventManager.OnCreatePieceActionData(moveActionData);
                eventManager.OnCreatePieceActionData(attackActionData);
            }
        }


        // Create hover line
        private void CreateHoverLine()
        {
            // First time creation
            if (hoverLine == null)
                hoverLine = PathEdge.CreateEdgeLine(HexListType.Deploy);

            ClearHoverLine();
        }


        // Hide hover line
        private void ShowHoverLine()
        {
            List<List<Vector3>> edgePoints = PathEdge.GetPathEdge(new List<Hex>() { currentHoverHex });
            foreach (List<Vector3> linePoints in edgePoints)
            {
                PathEdge.DisplayEdgeLine(hoverLine, linePoints);
            }
            hoverLine.gameObject.SetActive(true);
        }
        private void ClearHoverLine()
        {
            if (hoverLine != null)
                hoverLine.gameObject.SetActive(false);
        }
    }
}