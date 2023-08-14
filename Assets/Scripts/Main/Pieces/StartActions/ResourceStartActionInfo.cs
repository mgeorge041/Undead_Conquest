using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Start Action", menuName = "Resource Start Action")]
public class ResourceStartActionInfo : StartActionInfo
{
    public override StartActionType actionType => StartActionType.Resources;


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
}
