using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceActionDataHandler
{
    public Queue<PieceActionData> actionsQueue { get; private set; } = new Queue<PieceActionData>();
    public Stack<PieceActionData> performedActions { get; private set; } = new Stack<PieceActionData>();

    // Constructor
    public PieceActionDataHandler() { }


    // Add new action
    public void QueuePieceActionData(PieceActionData actionData)
    {
        actionsQueue.Enqueue(actionData);
    }


    // Perform all actions in queue
    public void PerformAllQueuedActions()
    {
        if (actionsQueue.Count > 0)
            PerformNextQueuedAction(null);
    }


    // Perform next queued action
    public void PerformNextQueuedAction(PieceActionData actionData)
    {
        if (actionData != null)
            actionData.eventManager.onFinishPieceAction.Unsubscribe(PerformNextQueuedAction);

        if (actionsQueue.Count <= 0)
        {
            return;
        }

        PieceActionData nextActionData = actionsQueue.Dequeue();
        nextActionData.eventManager.onFinishPieceAction.Subscribe(PerformNextQueuedAction);
        nextActionData.PerformAction();
        performedActions.Push(nextActionData);
    }
}
