using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCardInfo : CardInfo
{
    // Resource info
    public Dictionary<ResourceType, int> resources => GetResources();
    public ResourceType resourceType1;
    public int resourceCost1;
    public ResourceType resourceType2;
    public int resourceCost2;
    public ResourceType resourceType3;
    public int resourceCost3;

    // Start action
    public StartActionInfo startActionInfo;


    // Get resources
    public Dictionary<ResourceType, int> GetResources()
    {
        Dictionary<ResourceType, int> resourceDict = new Dictionary<ResourceType, int>();

        void SetResource(ResourceType resource, int cost)
        {
            if (resource == ResourceType.None || resourceDict.ContainsKey(resource))
                return;

            resourceDict[resource] = cost;
        }

        SetResource(resourceType1, resourceCost1);
        SetResource(resourceType2, resourceCost2);
        SetResource(resourceType3, resourceCost3);
        return resourceDict;
    }
}
