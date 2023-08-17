using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Card Info", menuName = "Resource Card Info")]
public class ResourceCardInfo : CardInfo
{
    // Base info
    public override CardType cardType => CardType.Resource;

    // Resource info
    public Dictionary<ResourceType, int> resources => GetResources();
    public ResourceType resourceType1 = ResourceType.None;
    public int resourceAmount1;
    public ResourceType resourceType2 = ResourceType.None;
    public int resourceAmount2;
    public ResourceType resourceType3 = ResourceType.None;
    public int resourceAmount3;


    // Get resources provided dictionary
    public Dictionary<ResourceType, int> GetResources()
    {
        Dictionary<ResourceType, int> resourcesDict = new Dictionary<ResourceType, int>();

        void SetResource(ResourceType resource, int amount)
        {
            if (resource == ResourceType.None || resourcesDict.ContainsKey(resource))
                return;

            resourcesDict[resource] = amount;
        }

        SetResource(resourceType1, resourceAmount1);
        SetResource(resourceType2, resourceAmount2);
        SetResource(resourceType3, resourceAmount3);
        return resourcesDict;
    }
}
