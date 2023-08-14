using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MapActionPhaseStates
{
    public class StateEventManager
    {
        public void Clear()
        {
            onEndPhase = null;
            onCreatePieceActionData = null;
            onPerformPieceActions = null;
        }


        private event Action<StateStartInfo> onEndPhase;
        public void OnEndPhase(StateStartInfo startInfo) => onEndPhase?.Invoke(startInfo);
        public void SubscribeEndPhase(Action<StateStartInfo> action) => onEndPhase += action;
        public void UnsubscribeEndPhase(Action<StateStartInfo> action) => onEndPhase -= action;

        private event Action<PieceActionData> onCreatePieceActionData;
        public void OnCreatePieceActionData(PieceActionData actionData) => onCreatePieceActionData?.Invoke(actionData);
        public void SubscribeCreatePieceActionData(Action<PieceActionData> action) => onCreatePieceActionData += action;
        public void UnsubscribeCreatePieceActionData(Action<PieceActionData> action) => onCreatePieceActionData -= action;

        private event Action onPerformPieceActions;
        public void OnPerformPieceActions() => onPerformPieceActions?.Invoke();
        public void SubscribePerformPieceActions(Action action) => onPerformPieceActions += action;
        public void UnsubscribePerformPieceActions(Action action) => onPerformPieceActions -= action;
    }
}