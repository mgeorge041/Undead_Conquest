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
        while (actionsQueue.Count > 0)
        {
            PieceActionData actionData = actionsQueue.Dequeue();
            actionData.PerformAction();
            performedActions.Push(actionData);
        }
    }
}
