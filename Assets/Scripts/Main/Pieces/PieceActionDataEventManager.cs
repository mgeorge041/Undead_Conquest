using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceActionDataEventManager
{
    public void Clear()
    {
        onFinishPieceAction.Clear();
    }


    public GameEvent<PieceActionData> onFinishPieceAction { get; private set; } = new GameEvent<PieceActionData>();
}
