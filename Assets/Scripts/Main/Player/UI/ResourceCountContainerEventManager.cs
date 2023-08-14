using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCountContainerEventManager
{
    public void Clear() 
    {
        onHide.Clear();
    }


    public GameEvent<ResourceCountContainer> onHide { get; private set; } = new GameEvent<ResourceCountContainer>();
}
