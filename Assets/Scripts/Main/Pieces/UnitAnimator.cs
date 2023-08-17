using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : PieceAnimator
{
    // Piece info
    public override Piece piece => unit;
    public Unit unit;


    // Instantiate
    public static UnitAnimator CreateUnitAnimator()
    {
        UnitAnimator animator = Instantiate(Resources.Load<UnitAnimator>("Prefabs/Pieces/Unit Animator"));
        return animator;
    }


    // Set piece
    public override void SetPiece(Piece piece)
    {
        if (piece.pieceType != PieceType.Unit)
            throw new System.ArgumentException("Cannot set unit for unit animator to non-unit piece.");

        unit = (Unit)piece;
        unit.unitData.eventManager.onSetHasActions.Subscribe(HandlePieceHasActions);
    }


    // Move unit along path
    public void MoveUnit(List<Vector3> movePath)
    {
        if (movePath.Count <= 0)
            return;

        StartCoroutine(AnimateMove(movePath));        
    }


    // Animate move
    private IEnumerator AnimateMove(List<Vector3> movePath)
    {
        for (int i = 1; i < movePath.Count; i++)
        {
            float t = 0;
            Vector3 startPosition = unit.transform.position;

            while (t < 1)
            {
                unit.transform.position = Vector3.Lerp(startPosition, movePath[i], t);
                t += Time.deltaTime * pauseCoroutine;
                yield return null;
            }
            unit.transform.position = movePath[i];
        }
        FinishMoveAnimation();
    }


    // Finish move
    public void FinishMoveAnimation()
    {
        unit.unitEventManager.onFinishMoveAnimation.OnEvent(unit);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
