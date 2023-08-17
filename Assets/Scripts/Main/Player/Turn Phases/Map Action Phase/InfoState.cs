using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace MapActionPhaseStates
{
    public class InfoState : PhaseState
    {
        public override StateType stateType => StateType.Info;

        // Piece info
        private Piece piece;
        private Unit unit => piece.isUnit ? (Unit)piece : null;
        public List<Hex> moveHexes { get; private set; } = new List<Hex>();


        // Constructor
        public InfoState() { }
        public InfoState(PlayerItemManager itemManager)
        {
            this.itemManager = itemManager;
        }


        // Start phase
        public override void StartState(StateStartInfo startInfo)
        {
            ClearState();
            piece = startInfo.piece;

            if (piece == null)
                throw new System.ArgumentNullException("Cannot set info state for null piece.");

            if (piece.isUnit)
            {
                SetMoveHexes();
                if (moveHexes.Count > 0)
                    CreateHexListEdgeLines(HexListType.Select, moveHexes);
                else
                    CreateHexListEdgeLines(HexListType.Select, new List<Hex>() { piece.hex });
            }
            else
            {
                CreateHexListEdgeLines(HexListType.Select, new List<Hex>() { piece.hex });
            }
            itemManager.pieceManager.SetSelectedPiece(piece);
        }


        // Clear phase
        public override void ClearState()
        {
            piece = null;
            moveHexes.Clear();
            ClearHexListEdgeLines();
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
            {
                EndState(new StateStartInfo(StateType.Action, clickHex.piece));
            }
            else
            {
                EndState(new StateStartInfo(StateType.Info, clickHex.piece));
            }
        }


        // Set move hexes
        private void SetMoveHexes()
        {
            moveHexes.Clear();
            ClearHexListEdgeLines();
            moveHexes = HexActionCalculator.GetMoveHexes(unit);
        }
    }
}