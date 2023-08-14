using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StartActionType
{
    AddCard,
    Draw,
    Heal,
    Resources,
    None
}


public abstract class StartAction
{
    // Start action info
    public abstract StartActionType actionType { get; }
    public virtual StartActionInfo actionInfo { get; protected set; }

    // Item manager
    public Piece piece { get; protected set; }
    public PlayerItemManager itemManager { get; protected set; }

    // Start action
    public int turnCooldown { get; protected set; }
    public abstract void PerformStartAction(Piece piece, PlayerItemManager itemManager);

    // Event manager
    public virtual StartActionEventManager eventManager { get; protected set; } = new StartActionEventManager();


    // Create start action
    public static StartAction CreateStartAction(StartActionInfo actionInfo)
    {
        switch (actionInfo.actionType)
        {
            case StartActionType.AddCard:
                AddCardStartActionInfo addCardActionInfo = (AddCardStartActionInfo)actionInfo;
                AddCardStartAction addCardStartAction = new AddCardStartAction(addCardActionInfo);
                return addCardStartAction;

            case StartActionType.Draw:
                DrawStartActionInfo drawActionInfo = (DrawStartActionInfo)actionInfo;
                DrawStartAction drawStartAction = new DrawStartAction(drawActionInfo);
                return drawStartAction;

            case StartActionType.Heal:
                break;

            case StartActionType.Resources:
                ResourceStartActionInfo resourceActionInfo = (ResourceStartActionInfo)actionInfo;
                ResourceStartAction resourceStartAction = new ResourceStartAction(resourceActionInfo);
                return resourceStartAction;

            default:
                throw new System.ArgumentException("Cannot create start action for None type action.");
        }
        return null;
    }
    public static StartAction CreateStartAction<T>(StartActionInfo actionInfo) where T : StartAction
    {
        return (T)CreateStartAction(actionInfo);
    }
    public static StartAction CreateStartAction(string actionPath)
    {
        StartActionInfo actionInfo = StartActionInfo.LoadStartActionInfo(actionPath);
        return CreateStartAction(actionInfo);
    }
    public static T CreateStartAction<T>(string actionPath) where T : StartAction
    {
        StartActionInfo actionInfo = StartActionInfo.LoadStartActionInfo(actionPath);
        return (T)CreateStartAction<T>(actionInfo);
    }


    // Set turn cooldown
    public void SetTurnCooldown(int turnCooldown)
    {
        this.turnCooldown = turnCooldown;
    }


    // Check whether can perform action
    protected bool CanPerformStartAction()
    {
        if (turnCooldown == 0)
        {
            turnCooldown = actionInfo.turnCooldown;
            return true;
        }
        else
        {
            turnCooldown--;
            return false;
        }
    }
}
