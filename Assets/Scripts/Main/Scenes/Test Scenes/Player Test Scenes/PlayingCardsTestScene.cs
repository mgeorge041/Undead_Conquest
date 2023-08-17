using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingCardsTestScene : MonoBehaviour
{
    public Hexmap hexmap;
    public Player player;


    // Start scene
    public void StartScene()
    {
        // Set resources
        hexmap.Initialize();
        player.Initialize();
        player.inputController.hexmap = hexmap;
        player.itemManager.resourceManager.AddResource(ResourceType.Bone, 10);
        player.itemManager.resourceManager.AddResource(ResourceType.Stone, 10);

        // Add card to hand
        for (int i = 0; i < 5; i++)
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            player.itemManager.deck.AddNewCardToDeck(card);
            //player.itemManager.DrawCard();
        }
        

        // Create test building
        Building building = Piece.CreatePiece<Building>(CardPaths.testBuilding);
        hexmap.hexmapData.AddPiece(building, Vector3Int.zero);
        player.itemManager.pieceManager.AddPiece(building);

        // Start phase
        player.turnPhaseHandler.SetNextPhase(TurnPhaseType.PlayCards);
    }


    // Reset
    public void ResetScene()
    {
        hexmap.Reset();
        player.Reset();
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
