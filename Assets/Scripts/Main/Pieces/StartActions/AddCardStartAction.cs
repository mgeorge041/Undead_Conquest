using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCardStartAction : StartAction
{
    public override StartActionType actionType => StartActionType.AddCard;
    public override StartActionInfo actionInfo => addCardActionInfo;
    public AddCardStartActionInfo addCardActionInfo { get; private set; }


    // Constructor
    public AddCardStartAction(AddCardStartActionInfo addCardActionInfo)
    {
        this.addCardActionInfo = addCardActionInfo;
        turnCooldown = addCardActionInfo.turnCooldown;
    }


    // Perform add of card to deck
    public override void PerformStartAction(Piece piece, PlayerItemManager itemManager)
    {
        if (!CanPerformStartAction())
            return;

        this.itemManager = itemManager;
        Card card = Card.CreateCard(addCardActionInfo.cardInfo);
        for (int i = 0; i < addCardActionInfo.numCards; i++)
        {
            itemManager.addCardQueue.Enqueue(card);
        }

        itemManager.eventManager.onFinishAnimatingAddCards.Subscribe(FinishAddingCards);
        itemManager.AddNextCard();
    }


    // Finish adding cards
    private void FinishAddingCards()
    {
        itemManager.eventManager.onFinishAnimatingAddCards.Unsubscribe(FinishAddingCards);
        eventManager.onFinishStartAction.OnEvent(this);
    }
}
