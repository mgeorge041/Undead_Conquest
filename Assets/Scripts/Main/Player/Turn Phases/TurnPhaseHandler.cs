using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TurnPhaseHandler
{
    // Turn phases
    public TurnPhase currentPhase { get; private set; }
    public Dictionary<TurnPhaseType, TurnPhase> turnPhases { get; private set; }
    public Dictionary<TurnPhaseType, PhaseInputControllerSettings> phaseControllerSettings = new Dictionary<TurnPhaseType, PhaseInputControllerSettings>();

    // Item manager
    private PlayerItemManager itemManager;

    // Event manager
    public TurnPhaseHandlerEventManager eventManager { get; private set; } = new TurnPhaseHandlerEventManager();


    // Constructor
    public TurnPhaseHandler(PlayerItemManager itemManager)
    {
        this.itemManager = itemManager;
        turnPhases = new Dictionary<TurnPhaseType, TurnPhase>()
        {
            { TurnPhaseType.StartAction, new StartActionPhase(itemManager) },
            { TurnPhaseType.Draw, new DrawPhase(itemManager) },
            { TurnPhaseType.PlayCards, new PlayCardsPhase(itemManager) },
            { TurnPhaseType.MapAction, new MapActionPhase(itemManager) },
            { TurnPhaseType.EndTurn, new EndTurnPhase() },
        };

        phaseControllerSettings = new Dictionary<TurnPhaseType, PhaseInputControllerSettings>()
        {
            { TurnPhaseType.StartAction, new EmptyPhaseControllerSettings() },
            { TurnPhaseType.Draw, new EmptyPhaseControllerSettings() },
            { TurnPhaseType.PlayCards, new PlayCardsPhaseControllerSettings() },
            { TurnPhaseType.MapAction, new MapActionPhaseControllerSettings() },
            { TurnPhaseType.EndTurn, new EmptyPhaseControllerSettings() },
        };

        SubscribePhaseEvents();        
        SetNextPhase(TurnPhaseType.EndTurn);
    }


    // Subscribe to turn phase events
    private void SubscribePhaseEvents()
    {
        foreach (TurnPhase phase in turnPhases.Values)
        {
            phase.eventManager.onEndPhase.Subscribe(HandleSetNextPhase);
        }
    }


    // Subscribe to player input controller events
    public void SubscribeInputControllerEvents(PlayerInputController inputController)
    {
        inputController.eventManager.onLeftClick.Subscribe(LeftClick);
        inputController.eventManager.onRightClick.Subscribe(RightClick);
        inputController.eventManager.onHover.Subscribe(Hover);
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
    private void HandleSetNextPhase(TurnPhaseType nextPhase)
    {
        WaitEvent wait = WaitEvent.CreateWaitEvent(1, () => SetNextPhase(nextPhase));
        wait.StartWait();
    }
    public void SetNextPhase(TurnPhaseType nextPhase)
    {
        itemManager.hand.DisableCardActions(nextPhase == TurnPhaseType.Draw);
        eventManager.onSetNextPhase.OnEvent(nextPhase);
        currentPhase = turnPhases[nextPhase];
        currentPhase.StartPhase();
    }


    // Start next phase in turn
    public void StartNextPhase()
    {
        currentPhase.EndPhase();
    }


    // Get phase input controller settings
    public PhaseInputControllerSettings GetPhaseControllerSettings(TurnPhaseType phaseType)
    {
        if (!phaseControllerSettings.ContainsKey(phaseType))
            throw new System.ArgumentException("Phase settings do not exist for phase type.");

        return phaseControllerSettings[phaseType];
    }


    // Handle input
    public void LeftClick(Hex clickHex)
    {
        currentPhase.LeftClick(clickHex);
    }
    public void RightClick(Hex clickHex)
    {
        currentPhase.RightClick(clickHex);
    }
    public void Hover(Hex hoverHex)
    {
        currentPhase.Hover(hoverHex);
    }
}
