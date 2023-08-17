using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceStartActionTestScene : MonoBehaviour
{
    public Player player;
    public Building building;
    public Hexmap hexmap;

    
    // Start scene
    public void StartScene()
    {
        player.Initialize();
        Building building = Building.CreateBuilding(CardPaths.testBuildingResourceStartAction);
        hexmap.hexmapData.AddPiece(building, Vector3Int.zero);
        player.itemManager.pieceManager.AddPiece(building);
    }


    // Start start action phase
    public void StartStartActionPhase()
    {
        player.turnPhaseHandler.SetNextPhase(TurnPhaseType.StartAction);
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
