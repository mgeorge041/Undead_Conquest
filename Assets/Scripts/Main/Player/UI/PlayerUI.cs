using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // UI Elements
    public ResourcePanel resourcePanel;
    public HandPanel handPanel;
    public SelectedCardInfoPanel selectedCardPanel;
    public PieceInfoPanel pieceInfoPanel;
    public TextMeshProUGUI phaseNameLabel;
    public Transform nextPhaseButtonContainer;
    public Button nextPhaseButton;
    public TextMeshProUGUI nextPhaseButtonLabel;
    public Transform cardDrawContainer;
    public Transform newCardContainer;

    // Draw card event handling
    private Queue<Tuple<Card, int, int, int>> drawCardQueue;
    private Queue<Tuple<Card, int, int>> addCardQueue;

    // Event manager
    public PlayerUIEventManager eventManager { get; private set; } = new PlayerUIEventManager();


    // Instantiate player UI
    public static PlayerUI CreatePlayerUI()
    {
        PlayerUI ui = Instantiate(Resources.Load<PlayerUI>("Prefabs/Player/Player UI/Player UI"));
        ui.Initialize();
        return ui;
    }


    // Initialize
    public void Initialize()
    {
        resourcePanel.Initialize();
        handPanel.Initialize();
        pieceInfoPanel.Initialize();
        //selectedCardPanel.Initialize();
        //selectedCardPanel.eventManager.SubscribeClickShowHandButton(ShowHand);
        initialized = true;
    }


    // Subscribe to player item (cards and resources) events
    public void SubscribePlayerItemEvents(PlayerItemManager itemManager)
    {
        itemManager.deck.eventManager.onAddCard.Subscribe(HandleDeckAddCard);
        itemManager.deck.eventManager.onChangeDeck.Subscribe(HandleChangeDeck);
        itemManager.deck.eventManager.onChangeDiscardPile.Subscribe(HandleDiscardPileEvents);
        //itemManager.hand.eventManager.SubscribeChangeHand(HandleHandEvents);
        itemManager.hand.eventManager.onChangeHand.Subscribe(HandleHandEvents);
        itemManager.eventManager.onLeftClickCard.Subscribe(HandleLeftClickCardEvents);
        itemManager.eventManager.onChangeResource.Subscribe(HandleResourceEvents);
        itemManager.eventManager.onFinishDrawingCards.Subscribe(HandleDrawCardEvents);
        itemManager.eventManager.onFinishAddingCards.Subscribe(HandleAddNewCardToDeck);
        itemManager.eventManager.onPlayCard.Subscribe(HandlePlayCard);
        itemManager.eventManager.onSetSelectedPiece.Subscribe(HandleSetSelectedPiece);
    }


    // Subscribe to turn phase handler events (changing turn phases)
    public void SubscribeTurnPhaseHandlerEvents(TurnPhaseHandler turnPhaseHandler)
    {
        turnPhaseHandler.eventManager.onSetNextPhase.Subscribe(HandleTurnPhaseHandlerEvents);
    }


    // Reset
    public void Reset()
    {
        resourcePanel.Reset();
        handPanel.Reset();
    }


    // Handle deck events
    private void HandleDeckAddCard(Card card)
    {
        handPanel.AddCardToDeck(card);
    }
    private void HandleChangeDeck(int numCards, int totalNumCards)
    {
        handPanel.SetNumDeckCards(numCards, totalNumCards);
    }
    private void HandleDiscardPileEvents(int numCards)
    {
        handPanel.SetNumDiscardCards(numCards);
    }


    // Handle hand events
    private void HandleHandEvents(int numCards)
    {
        handPanel.SetNumHandCards(numCards);
    }
    private void HandleLeftClickCardEvents(PlayableCard card)
    {
        //bool hideHand = selectedCardPanel.SetSelectedCardInfo(card);
        handPanel.OpenHand(false);
    }


    // Handle resource events
    private void HandleResourceEvents(ResourceType resource, int amount)
    {
        resourcePanel.SetResource(resource, amount);
    }


    /// <summary>Handle draw card events.</summary>
    /// <param name="drawCardQueue">Contains draw card event info 
    /// (Card, int, int, int) => (drawCard, numDeckCards, numTotalCards, numHandCards)</param>
    private void HandleDrawCardEvents(Queue<Tuple<Card, int, int, int>> drawCardQueue)
    {
        if (drawCardQueue.Count <= 0)
        {
            eventManager.onFinishDrawAnimations.OnEvent();
            return;
        }

        // Create copy of draw card queue
        this.drawCardQueue = new Queue<Tuple<Card, int, int, int>>(drawCardQueue);
        Tuple<Card, int, int, int> drawCardInfo = this.drawCardQueue.Dequeue();

        // Show rotation animation
        cardDrawContainer.parent.gameObject.SetActive(true);
        Card drawCard = drawCardInfo.Item1;
        drawCard.transform.SetParent(cardDrawContainer);
        drawCard.eventManager.onFinishRotate.Subscribe(AddCardToHand);
        drawCard.RotateCard();
        
        // Update hand panel stats
        handPanel.SetNumHandCards(drawCardInfo.Item4);
        handPanel.SetNumDeckCards(drawCardInfo.Item2, drawCardInfo.Item3);
    }


    // Add card to hand
    private void AddCardToHand(Card card)
    {
        card.eventManager.onFinishRotate.Unsubscribe(AddCardToHand);

        // Handle if card is resource card
        if (card.cardType == CardType.Resource)
        {
            ResourceCard resourceCard = (ResourceCard)card;
            foreach (KeyValuePair<ResourceType, int> pair in resourceCard.resources)
            {
                if (pair.Key != ResourceType.None)
                {
                    int currentAmount = resourcePanel.GetResource(pair.Key);
                    resourcePanel.SetResource(pair.Key, pair.Value + currentAmount);
                }
            }

            card.gameObject.SetActive(false);
        }
        else
        {
            handPanel.AddCard(card);
        }

        cardDrawContainer.parent.gameObject.SetActive(false);
        HandleDrawCardEvents(drawCardQueue);
    }


    // Handle adding new card to deck
    private void HandleAddNewCardToDeck(Queue<Tuple<Card, int, int>> addCardQueue)
    {
        if (addCardQueue.Count <= 0)
        {
            newCardContainer.gameObject.SetActive(false);
            eventManager.onFinishAddCardAnimations.OnEvent();
            return;
        }

        this.addCardQueue = new Queue<Tuple<Card, int, int>>(addCardQueue);
        Tuple<Card, int, int> addInfo = this.addCardQueue.Dequeue();

        // Show rotation animation
        newCardContainer.gameObject.SetActive(true);
        cardDrawContainer.parent.gameObject.SetActive(true);
        Card drawCard = addInfo.Item1;
        drawCard.transform.SetParent(cardDrawContainer);
        drawCard.eventManager.onFinishRotate.Subscribe(AddNewCardToDeck);
        drawCard.RotateCard();

        // Update hand panel stats
        handPanel.SetNumDiscardCards(addInfo.Item2);
        handPanel.SetNumTotalDeckCards(addInfo.Item3);
    }


    // Add new card to deck
    private void AddNewCardToDeck(Card card) 
    {
        card.eventManager.onFinishRotate.Unsubscribe(AddNewCardToDeck);
        card.gameObject.SetActive(false);
        
        HandleAddNewCardToDeck(addCardQueue);
    }


    // Handle playing card
    private void HandlePlayCard(PlayableCard card, PlayerResources resources)
    {
        // Check each resource amount
        handPanel.AddCardToDeck(card);
        resourcePanel.SetResource(ResourceType.Bone, resources.GetResource(ResourceType.Bone));
        resourcePanel.SetResource(ResourceType.Corpse, resources.GetResource(ResourceType.Corpse));
        resourcePanel.SetResource(ResourceType.Mana, resources.GetResource(ResourceType.Mana));
        resourcePanel.SetResource(ResourceType.Stone, resources.GetResource(ResourceType.Stone));
        resourcePanel.SetResource(ResourceType.Wood, resources.GetResource(ResourceType.Wood));
    }


    // Handle setting the selected piece
    private void HandleSetSelectedPiece(Piece piece)
    {
        pieceInfoPanel.SetPiece(piece);
    }





    // Show hand
    public void ShowHand(bool showHand)
    {
        handPanel.ShowHand(showHand);
        //handPanel.SetShowHand(showHand);
        //selectedCardPanel.SetShowHand(showHand);
    }


    // Show UI based on turn phase
    private void HandleTurnPhaseHandlerEvents(TurnPhaseType nextPhase)
    {
        SetPhaseNameLabel(nextPhase);
        SetNextPhaseButtonInfo(nextPhase);
        ShowPhaseHand(nextPhase);
    }


    // Set name of the phase
    private void SetPhaseNameLabel(TurnPhaseType nextPhase)
    {
        switch (nextPhase)
        {
            case TurnPhaseType.Draw:
                phaseNameLabel.text = "Draw Phase";
                break;
            case TurnPhaseType.StartAction:
                phaseNameLabel.text = "Start Action Phase";
                break;
            case TurnPhaseType.PlayCards:
                phaseNameLabel.text = "Play Cards Phase";
                break;
            case TurnPhaseType.MapAction:
                phaseNameLabel.text = "Map Action Phase";
                break;
            case TurnPhaseType.EndTurn:
                phaseNameLabel.text = "Enemy Turn";
                break;
        }
    }


    // Set next phase button info
    private void SetNextPhaseButtonInfo(TurnPhaseType nextPhase)
    {
        // Set next phase button text
        if (nextPhase != TurnPhaseType.MapAction)
            nextPhaseButtonLabel.text = "Next Phase";
        else
            nextPhaseButtonLabel.text = "End Turn";

        // Enable next phase button
        if (nextPhase == TurnPhaseType.PlayCards || nextPhase == TurnPhaseType.MapAction)
            nextPhaseButtonContainer.gameObject.SetActive(true);
        else
            nextPhaseButtonContainer.gameObject.SetActive(false);
    }


    // Show hand
    private void ShowPhaseHand(TurnPhaseType nextPhase)
    {
        if (nextPhase == TurnPhaseType.Draw || nextPhase == TurnPhaseType.PlayCards)
        {
            ShowHand(true);
            handPanel.OpenHand(true);
        }
        else
            ShowHand(false);
    }


    // Click next phase button
    public void ClickNextPhaseButton()
    {
        eventManager.onClickNextPhaseButton.OnEvent();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
