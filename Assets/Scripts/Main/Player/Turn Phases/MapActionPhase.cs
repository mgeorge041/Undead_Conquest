using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapActionPhaseStates;

public class MapActionPhase : TurnPhase
{
    public override TurnPhaseType phaseType => TurnPhaseType.MapAction;
    public override TurnPhaseType nextPhaseType => TurnPhaseType.EndTurn;

    // State handler
    public PhaseState currentPhaseState { get; private set; }
    public Dictionary<StateType, PhaseState> phaseStates = new Dictionary<StateType, PhaseState>();
    public PieceActionDataHandler actionDataHandler { get; private set; } = new PieceActionDataHandler();


    // Constructor
    public MapActionPhase() { }
    public MapActionPhase(PlayerItemManager itemManager) 
    {
        this.itemManager = itemManager;
        phaseStates = new Dictionary<StateType, PhaseState>() 
        {
            { StateType.Neutral, new NeutralState(itemManager) },
            { StateType.Info, new InfoState(itemManager) },
            { StateType.Action, new ActionState(itemManager) },
            { StateType.Ability, null },
            { StateType.End, null },
        };
        SubscribeStateEvents();
        SetNextState(new StateStartInfo(StateType.Neutral));
    }


    // Subscribe to state events
    private void SubscribeStateEvents()
    {
        foreach (PhaseState state in phaseStates.Values)
        {
            if (state == null)
                continue;
            state.eventManager.SubscribeEndPhase(SetNextState);
            state.eventManager.SubscribeCreatePieceActionData(CreatePieceActionData);
            state.eventManager.SubscribePerformPieceActions(PerformPieceActions);
        }
    }


    // End phase
    public override void EndPhase()
    {
        itemManager.EndTurn();
        base.EndPhase();
    }


    // Set next state
    public void SetNextState(StateStartInfo startInfo)
    {
        currentPhaseState = phaseStates[startInfo.nextStateType];
        currentPhaseState.StartState(startInfo);
    }


    // Handle creation of piece action data
    private void CreatePieceActionData(PieceActionData actionData)
    {
        actionDataHandler.QueuePieceActionData(actionData);
    }
    private void PerformPieceActions()
    {
        actionDataHandler.PerformAllQueuedActions();
    }


    // Player input
    public override void LeftClick(Hex clickHex)
    {
        currentPhaseState.LeftClick(clickHex);
    }
    public override void RightClick(Hex clickHex)
    {
        currentPhaseState.RightClick(clickHex);
    }
    public override void Hover(Hex hoverHex)
    {
        currentPhaseState.Hover(hoverHex);
    }
}
