using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckListManager
{
    public int numTotalCards => GetNumCards();
    public Dictionary<CardInfo, int> deckList = new Dictionary<CardInfo, int>();
    public Dictionary<CardInfo, DeckCardPanel> cardPanels = new Dictionary<CardInfo, DeckCardPanel>();
    public Dictionary<ResourceType, int> cardResourceCosts = new Dictionary<ResourceType, int>();
    public Dictionary<ResourceType, int> cardResourceProvides = new Dictionary<ResourceType, int>();


    // Constructor
    public DeckListManager() { }


    // Add card to deck list
    public void HandleAddCardInfo(CardInfo cardInfo)
    {
        if (cardInfo == null)
            throw new System.ArgumentNullException("Cannot add null card info to deck list.");

        int numCards;
        DeckCardPanel cardPanel;
        if (deckList.TryGetValue(cardInfo, out numCards))
        {
            numCards++;
            deckList[cardInfo] = numCards;

            cardPanel = cardPanels[cardInfo];
        }
        else
        {
            deckList[cardInfo] = 1;
            
            cardPanel = DeckCardPanel.CreateDeckCardPanel();
            cardPanel.SetCardPanelInfo(cardInfo);
            cardPanel.eventManager.onAddCard.Subscribe(HandleAddCardInfo);
            cardPanel.eventManager.onRemoveCard.Subscribe(HandleRemoveCardInfo);
            cardPanel.gameObject.SetActive(true);
            cardPanels[cardInfo] = cardPanel;
        }
        cardPanel.IncrementCardCount(1);

        // Update costs and provisions of card
        UpdateResourceCostsProvisions(cardInfo, 1);
    }


    // Remove card from list
    public void HandleRemoveCardInfo(CardInfo cardInfo)
    {
        if (cardInfo == null)
            throw new System.ArgumentNullException("Deck list cannot remove null card info.");

        if (!deckList.ContainsKey(cardInfo))
            throw new System.ArgumentException("Deck list cannot remove card info it does not contain.");

        int numCards = deckList[cardInfo] - 1;
        cardPanels[cardInfo].IncrementCardCount(-1);

        if (numCards <= 0)
        {
            deckList[cardInfo] = 0;
            cardPanels[cardInfo].gameObject.SetActive(false);
        }
        else
        {
            deckList[cardInfo] = numCards;
        }

        // Update costs and provisions of card
        UpdateResourceCostsProvisions(cardInfo, -1);
    }


    // Update costs and provisions of card
    private void UpdateResourceCostsProvisions(CardInfo cardInfo, int multiplier)
    {
        if (multiplier != 1 && multiplier != -1)
            throw new System.ArgumentException("Multiplier must be either 1 or -1.");

        if (cardInfo.isPlayableType)
        {
            foreach (KeyValuePair<ResourceType, int> pair in cardInfo.playableCardInfo.resources)
            {
                AddResourceAmount(cardResourceCosts, pair.Key, pair.Value * multiplier);
            }
        }
        else if (cardInfo.isResourceType)
        {
            foreach (KeyValuePair<ResourceType, int> pair in cardInfo.resourceCardInfo.resources)
            {
                AddResourceAmount(cardResourceProvides, pair.Key, pair.Value * multiplier);
            }
        }
    }


    // Add resource cost or provided
    private void AddResourceAmount(Dictionary<ResourceType, int> resources, ResourceType resourceType, int cost)
    {
        if (resourceType == ResourceType.None)
            return;

        int currentCost;
        int newCost;
        if (resources.TryGetValue(resourceType, out currentCost))
            newCost = currentCost + cost;
        else
            newCost = cost;

        resources[resourceType] = newCost;
    }


    // Get number of cards
    public int GetNumCards()
    {
        int numTotalCards = 0;
        foreach (int numCards in deckList.Values)
            numTotalCards += numCards;
        
        return numTotalCards;
    }
}
