using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapActionTestScene : MonoBehaviour
{
    public Player player;
    private HexmapData hexmapData;
    public Unit unit;
    public Unit enemyUnit;


    // Start scene
    public void StartScene()
    {
        player.Initialize();
        player.turnPhaseHandler.SetNextPhase(TurnPhaseType.MapAction);

        if (unit == null)
            unit = Unit.CreateUnit(CardPaths.testUnit);
        if (enemyUnit == null)
            enemyUnit = Unit.CreateUnit(CardPaths.testUnit);
        enemyUnit.pieceData.playerId = 2;

        hexmapData = new HexmapData();
        hexmapData.AddPiece(unit, Vector3Int.zero);
        hexmapData.AddPiece(enemyUnit, new Vector3Int(1, -1, 0));
        player.inputController.SetHexmapData(hexmapData);
        player.itemManager.pieceManager.AddPiece(unit);
    }


    // Reset scene
    public void ResetScene()
    {
        player.Reset();
        unit.Reset();
        StartScene();
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
