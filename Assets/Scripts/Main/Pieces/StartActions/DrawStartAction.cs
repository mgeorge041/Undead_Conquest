using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawStartAction : StartAction
{
    public override StartActionType actionType => StartActionType.Draw;
    public override StartActionInfo actionInfo => drawActionInfo;
    public DrawStartActionInfo drawActionInfo { get; private set; }


    // Constructor
    public DrawStartAction(DrawStartActionInfo drawActionInfo) 
    {
        this.drawActionInfo = drawActionInfo;
        turnCooldown = drawActionInfo.turnCooldown;
    }


    // Perform draw card start action
    public override void PerformStartAction(Piece piece, PlayerItemManager itemManager)
    {
        if (!CanPerformStartAction())
            return;

        this.itemManager = itemManager;
        for (int i = 0; i < drawActionInfo.numCards; i++)
        {
            itemManager.AddDrawCardAction();
        }
        itemManager.eventManager.onFinishAnimatingDrawCards.Subscribe(FinishDrawingCards);
        itemManager.DrawNextCard();
    }


    // Finish drawing all cards for this action
    private void FinishDrawingCards()
    {
        itemManager.eventManager.onFinishAnimatingDrawCards.Unsubscribe(FinishDrawingCards);
        eventManager.onFinishStartAction.OnEvent(this);
    }
}
