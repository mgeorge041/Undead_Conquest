using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Card Info", menuName = "Resource Card Info")]
public class ResourceCardInfo : CardInfo
{
    // Base info
    public override CardType cardType => CardType.Resource;

    // Resource info
    public ResourceType resourceType1 = ResourceType.None;
    public int resourceAmount1;
    public ResourceType resourceType2 = ResourceType.None;
    public int resourceAmount2;
    public ResourceType resourceType3 = ResourceType.None;
    public int resourceAmount3;

}
