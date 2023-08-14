using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StartActionInfo : ScriptableObject
{
    [field: SerializeField] public virtual StartActionType actionType { get; protected set; }
    public int turnCooldown;


    // Load start action info
    public static StartActionInfo LoadStartActionInfo(string actionPath)
    {
        return Instantiate(Resources.Load<StartActionInfo>(actionPath));
    }
    public static T LoadStartActionInfo<T>(string actionPath) where T : StartActionInfo
    {
        return Instantiate(Resources.Load<T>(actionPath));
    }
}
