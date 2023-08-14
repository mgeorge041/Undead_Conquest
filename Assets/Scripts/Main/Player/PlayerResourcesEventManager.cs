using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResourcesEventManager
{
    public void Clear() 
    {
        onChangeResource.Clear();
    }


    public GameEvent<ResourceType, int> onChangeResource { get; private set; } = new GameEvent<ResourceType, int>();
}
