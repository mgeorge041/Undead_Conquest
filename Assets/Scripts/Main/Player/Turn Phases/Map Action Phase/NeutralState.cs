using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapActionPhaseStates
{
    public class NeutralState : PhaseState
    {
        public override StateType stateType => StateType.Neutral;


        // Constructor
        public NeutralState() { }
        public NeutralState(PlayerItemManager itemManager)
        {
            this.itemManager = itemManager;
        }


        // Clicks
        public override void LeftClick(Hex clickHex)
        {
            if (clickHex == null || !clickHex.hasPiece)
                return;

            if (itemManager.pieceManager.HasPiece(clickHex.piece) && clickHex.piece.pieceData.hasActions)
            {
                EndState(new StateStartInfo(StateType.Action, clickHex.piece));
            }
            else
            {
                EndState(new StateStartInfo(StateType.Info, clickHex.piece));
            }
        }
    }
}