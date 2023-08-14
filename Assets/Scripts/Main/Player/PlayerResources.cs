using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Bone,
    Corpse,
    Mana,
    Stone,
    Wood,
    None
}


public class PlayerResources
{
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();

    // Event manager
    public PlayerResourcesEventManager eventManager { get; private set; } = new PlayerResourcesEventManager();


    // Constructor
    public PlayerResources() { }


    // Reset
    public void Reset()
    {
        resources = new Dictionary<ResourceType, int>();
    }


    // Get or set resource
    public int GetResource(ResourceType resource)
    {
        int amount;
        if (resources.TryGetValue(resource, out amount))
            return amount;
        return 0;
    }
    public void SetResource(ResourceType resource, int amount)
    {
        resources[resource] = amount;
        eventManager.onChangeResource.OnEvent(resource, amount);
    }


    // Add or remove resource
    public void AddResource(ResourceType resource, int amount)
    {
        int newAmount = Mathf.Max(0, GetResource(resource) + amount);
        SetResource(resource, newAmount);
    }
    public void RemoveResource(ResourceType resource, int amount)
    {
        int newAmount = Mathf.Max(0, GetResource(resource) - amount);
        SetResource(resource, newAmount);
    }


    // Get whether a card can be played
    public bool CanPlayCard(Card card)
    {
        if (card == null || card.cardType == CardType.Resource)
            return false;

        // Check each resource amount
        PlayableCard playableCard = (PlayableCard)card;
        if (playableCard.resourceType1 != ResourceType.None &&
            playableCard.resourceCost1 > GetResource(playableCard.resourceType1))
            return false;

        if (playableCard.resourceType2 != ResourceType.None &&
            playableCard.resourceCost2 > GetResource(playableCard.resourceType2))
            return false;

        if (playableCard.resourceType3 != ResourceType.None &&
            playableCard.resourceCost3 > GetResource(playableCard.resourceType3))
            return false;
        return true;
    }


    // Play a card and subtract the resources
    public void PlayCard(Card card)
    {
        if (card == null || card.cardType == CardType.Resource)
            return;

        // Check each resource amount
        PlayableCard playableCard = (PlayableCard)card;
        if (playableCard.resourceType1 != ResourceType.None)
            RemoveResource(playableCard.resourceType1, playableCard.resourceCost1);

        if (playableCard.resourceType2 != ResourceType.None)
            RemoveResource(playableCard.resourceType2, playableCard.resourceCost2);

        if (playableCard.resourceType3 != ResourceType.None)
            RemoveResource(playableCard.resourceType3, playableCard.resourceCost3);
    }

}
