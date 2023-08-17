using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Start Resources", menuName = "Player Start Resources")]
public class PlayerStartResources : ScriptableObject
{
    public Dictionary<ResourceType, int> resources => GetResources();
    public ResourceType resourceType1;
    public int resourceAmount1;
    public ResourceType resourceType2;
    public int resourceAmount2;
    public ResourceType resourceType3;
    public int resourceAmount3;
    public ResourceType resourceType4;
    public int resourceAmount4;
    public ResourceType resourceType5;
    public int resourceAmount5;


    // Load start resources from path
    public static PlayerStartResources LoadStartResources(string startResourcePath)
    {
        PlayerStartResources startResources = Instantiate(Resources.Load<PlayerStartResources>(startResourcePath));
        return startResources;
    }


    // Return resources as dictionary
    public Dictionary<ResourceType, int> GetResources()
    {
        Dictionary<ResourceType, int> resourcesDict = new Dictionary<ResourceType, int>()
        {
            { resourceType1, resourceAmount1 },
            { resourceType2, resourceAmount2 },
            { resourceType3, resourceAmount3 },
            { resourceType4, resourceAmount4 },
            { resourceType5, resourceAmount5 },
        };
        return resourcesDict;
    }
}
