using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnPhasesTestScene : MonoBehaviour
{
    public Player player;
    private HexmapData hexmapData;


    // Start scene
    public void StartScene()
    {
        player.Initialize();
        player.turnPhaseHandler.SetNextPhase(TurnPhaseType.EndTurn);

        hexmapData = new HexmapData();
        hexmapData.SetMapPattern(MapPattern.CreateMapPattern(MapType.Hexagon, 10));

        // Set main base
        Building building = Building.CreateBuilding(CardPaths.mainBase);
        hexmapData.AddPiece(building, Vector3Int.zero);
        player.itemManager.AddPiece(building);
        player.SetHexmapData(hexmapData);

        // Add card to hand
        for (int i = 0; i < 2; i++)
        {
            Card card = Card.CreateCard(CardPaths.testUnit);
            player.itemManager.deck.AddNewCardToDeck(card);
        }
        for (int i = 0; i < 2; i++)
        {
            Card card = Card.CreateCard(CardPaths.humanTrap);
            player.itemManager.deck.AddNewCardToDeck(card);
        }

        /*
        for (int i = 0; i < 5; i++)
        {
            ResourceCard resourceCard = Card.CreateCard<ResourceCard>(CardPaths.testBoneResource);
            resourceCard.gameObject.SetActive(false);
            player.itemManager.deck.AddCard(resourceCard);
        }

        for (int i = 0; i < 5; i++)
        {
            ResourceCard resourceCard2 = Card.CreateCard<ResourceCard>(CardPaths.stone);
            resourceCard2.gameObject.SetActive(false);
            player.itemManager.deck.AddCard(resourceCard2);
        }
        */

        player.itemManager.deck.ShuffleDeck();
    }


    // Reset scene
    public void ResetScene()
    {

    }


    // Have player start turn
    public void StartPlayerTurn()
    {
        player.turnPhaseHandler.SetNextPhase(TurnPhaseType.EndTurn);
        player.turnPhaseHandler.StartNextPhase();
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
