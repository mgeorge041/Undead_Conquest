using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStartAction : StartAction
{
    public override StartActionType actionType => StartActionType.Resources;
    public override StartActionInfo actionInfo => resourceActionInfo;
    public ResourceStartActionInfo resourceActionInfo { get; private set; }
    private Dictionary<ResourceType, int> resourceInfo = new Dictionary<ResourceType, int>();
    
    private ResourceCountContainer resourceContainer;
    private Queue<KeyValuePair<ResourceType, int>> resourceQueue = new Queue<KeyValuePair<ResourceType, int>>();



    // Constructor
    public ResourceStartAction(ResourceStartActionInfo resourceActionInfo)
    {
        this.resourceActionInfo = resourceActionInfo;
        turnCooldown = resourceActionInfo.turnCooldown;
        resourceInfo[resourceActionInfo.resourceType1] = resourceActionInfo.resourceAmount1;
        resourceInfo[resourceActionInfo.resourceType2] = resourceActionInfo.resourceAmount2;
        resourceInfo[resourceActionInfo.resourceType3] = resourceActionInfo.resourceAmount3;
        resourceInfo[resourceActionInfo.resourceType4] = resourceActionInfo.resourceAmount4;
        resourceInfo[resourceActionInfo.resourceType5] = resourceActionInfo.resourceAmount5;

        resourceContainer = ResourceCountContainer.CreateResourceCountContainer();
        resourceContainer.eventManager.onHide.Subscribe(ShowNextResourceContainer);
        resourceContainer.gameObject.SetActive(false);
    }


    // Perform resource start action
    public override void PerformStartAction(Piece piece, PlayerItemManager itemManager)
    {
        if (!CanPerformStartAction())
            return;

        this.piece = piece;
        this.itemManager = itemManager;
        foreach (KeyValuePair<ResourceType, int> pair in resourceInfo)
        {
            if (pair.Key != ResourceType.None)
            {
                itemManager.AddResource(pair.Key, pair.Value);
                resourceQueue.Enqueue(pair);
            }
        }
        ShowNextResourceContainer(null);
    }


    // Show next resource container
    private void ShowNextResourceContainer(ResourceCountContainer container)
    {
        if (resourceQueue.Count <= 0)
        {
            eventManager.onFinishStartAction.OnEvent(this);
            return;
        }

        KeyValuePair<ResourceType, int> pair = resourceQueue.Dequeue();
        resourceContainer.SetResourceInformation(pair.Key, "+" + pair.Value);
        resourceContainer.ShowGainingResource(piece.transform.position + new Vector3(0, 0.25f));
        itemManager.ShowAddResourceUI(pair.Key);
    }
}
