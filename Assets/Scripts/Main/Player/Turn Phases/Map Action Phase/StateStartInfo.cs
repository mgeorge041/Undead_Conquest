using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapActionPhaseStates
{
    public class StateStartInfo
    {
        public StateType nextStateType { get; private set; }
        public Piece piece { get; private set; }


        public StateStartInfo() { }
        public StateStartInfo(StateType nextStateType)
        {
            this.nextStateType = nextStateType;
        }
        public StateStartInfo(StateType nextStateType, Piece piece)
        {
            this.nextStateType = nextStateType;
            this.piece = piece;

        }
    }
}