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
        public List<Hex> movePath { get; private set; } = new List<Hex>();
        public List<SpriteRenderer> pathSegments { get; private set; } = new List<SpriteRenderer>();


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
            itemManager.pieceManager.SetSelectedPiece(piece);
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
            HideMovePathArrow();
        }


        // Clicks
        public override void LeftClick(Hex clickHex)
        {
            if (clickHex == null || !clickHex.hasPiece || clickHex.piece == piece)
            {
                EndState(new StateStartInfo(StateType.Neutral));
                return;
            }

            if (itemManager.pieceManager.HasPiece(clickHex.piece) && clickHex.piece.pieceData.hasActions)
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
                AttackHex(clickHex);
            }
            else
            {
                return;
            }
            eventManager.OnPerformPieceActions();
            itemManager.pieceManager.SetSelectedPiece(piece);
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
                HideMovePathArrow();
                return;
            }

            currentHoverHex = hoverHex;
            ShowHoverLine();
            if (moveHexes.Contains(hoverHex))
            {
                CreateMovePath();
                ShowMovePathArrow();
            }
            if (attackHexes.Contains(hoverHex))
            {
                CheckAttackMoveHex(hoverHex);
                ShowMovePathArrow();
            }
        }


        // Set move hexes for unit
        private void SetMoveHexes()
        {
            moveHexes = HexActionCalculator.GetMoveHexes((Unit)piece);
        }


        // Create move path
        private void CreateMovePath()
        {
            movePath = HexPathfinding.GetPath(piece.hex.pathNode, currentHoverHex.pathNode);
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
                movePath.Clear();
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
                movePath = HexPathfinding.GetMinPath(piece.hex.pathNode, Hex.GetPathNodes(attackMoveHexes));
            }
        }


        // Attack hex
        private void AttackHex(Hex targetHex)
        {
            if (movePath.Count > 0)
            {
                PieceActionData moveActionData = new PieceActionData(piece, PieceActionType.Move, piece.hex, movePath[movePath.Count - 1]);
                PieceActionData attackActionData = new PieceActionData(piece, PieceActionType.Attack, movePath[movePath.Count - 1], targetHex);
                eventManager.OnCreatePieceActionData(moveActionData);
                eventManager.OnCreatePieceActionData(attackActionData);
            }
            else
            {
                PieceActionData attackActionData = new PieceActionData(piece, PieceActionType.Attack, piece.hex, targetHex);
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


        // Show move path arrow
        private void ShowMovePathArrow()
        {
            HideMovePathArrow();

            // Create necessary path segments
            int numNeededSegments = movePath.Count - pathSegments.Count;
            for (int i = 0; i < numNeededSegments; i++)
            {
                SpriteRenderer pathSegment = PathArrow.CreatePathSegment();
                pathSegments.Add(pathSegment);
            }

            // Set sprites for path
            for (int i = 0; i < movePath.Count - 1; i++)
            {
                // Create path segment
                Vector3Int directionIn = Direction.GetDirectionEnterHex(movePath[i], movePath[i + 1]);

                // Next hex is target
                Vector3Int directionOut;
                if (i + 2 == movePath.Count)
                {
                    directionOut = Vector3Int.zero;
                }
                else
                {
                    directionOut = Direction.GetDirectionHexes(movePath[i + 1], movePath[i + 2]);
                }

                // Add path segment
                SpriteRenderer pathSegment = pathSegments[i];
                pathSegment.sprite = PathArrow.LoadPathSegmentSprite(directionIn, directionOut);
                PathArrow.SetPathSegmentLocalScale(pathSegment, directionIn, directionOut);
                pathSegment.transform.position = movePath[i + 1].pathNode.worldPosition;
                pathSegment.gameObject.SetActive(true);
            }
        }
        private void HideMovePathArrow()
        {
            foreach (SpriteRenderer pathSegment in pathSegments)
            {
                pathSegment.gameObject.SetActive(false);
            }
        }
    }
}