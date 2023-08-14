using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnPhaseHandler
{
    public TurnPhase currentPhase { get; private set; }
    public Dictionary<TurnPhaseType, TurnPhase> turnPhases { get; private set; }

    // Event manager
    public TurnPhaseHandlerEventManager eventManager { get; private set; } = new TurnPhaseHandlerEventManager();


    // Constructor
    public TurnPhaseHandler() 
    {
        turnPhases = new Dictionary<TurnPhaseType, TurnPhase>()
        {
            { TurnPhaseType.Draw, new DrawPhase() },
            { TurnPhaseType.StartAction, new StartActionPhase() },
            { TurnPhaseType.PlayCards, new PlayCardsPhase() },
            { TurnPhaseType.MapAction, new MapActionPhase() },
            { TurnPhaseType.EndTurn, new EndTurnPhase() },
        };
    }
    public TurnPhaseHandler(PlayerItemManager itemManager)
    {
        turnPhases = new Dictionary<TurnPhaseType, TurnPhase>()
        {
            { TurnPhaseType.Draw, new DrawPhase(itemManager) },
            { TurnPhaseType.StartAction, new StartActionPhase(itemManager) },
            { TurnPhaseType.PlayCards, new PlayCardsPhase(itemManager) },
            { TurnPhaseType.MapAction, new MapActionPhase(itemManager) },
            { TurnPhaseType.EndTurn, new EndTurnPhase() },
        };
        SubscribePhaseEvents();
        SetNextPhase(TurnPhaseType.EndTurn);
    }


    // Subscribe to turn phase events
    private void SubscribePhaseEvents()
    {
        foreach (TurnPhase phase in turnPhases.Values)
        {
            phase.eventManager.onEndPhase.Subscribe(SetNextPhase);
        }
    }


    // Get phase 
    public TurnPhase GetPhase(TurnPhaseType phaseType)
    {
        TurnPhase phase;
        if (turnPhases.TryGetValue(phaseType, out phase))
            return phase;
        return null;
    }
    public T GetPhase<T>(TurnPhaseType phaseType) where T : TurnPhase
    {
        return (T)GetPhase(phaseType);
    }


    // Handle ending of phase
    public void SetNextPhase(TurnPhaseType nextPhase)
    {
        eventManager.onSetNextPhase.OnEvent(nextPhase);
        currentPhase = turnPhases[nextPhase];
        currentPhase.StartPhase();
    }


    // Start next phase in turn
    public void StartNextPhase()
    {
        currentPhase.EndPhase();
    }


    // Player input
    public void LeftClick(Hex hex)
    {
        currentPhase.LeftClick(hex);
    }
    public void RightClick(Hex hex)
    {
        currentPhase.RightClick(hex);
    }
    public void Hover(Hex hex)
    {
        currentPhase.Hover(hex);
    }
}
