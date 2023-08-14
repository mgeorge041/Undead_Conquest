using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingCardsTestScene : MonoBehaviour
{
    public Player player;
    private List<Card> cards = new List<Card>();


    // Create cards for player
    private void CreateCards()
    {
        for (int i = 0; i < 5; i++)
        {
            ResourceCard resourceCard = Card.CreateCard<ResourceCard>(CardPaths.testBoneResource);
            resourceCard.gameObject.SetActive(false);
            player.itemManager.deck.AddNewCardToDeck(resourceCard);
            cards.Add(resourceCard);
        }

        for (int i = 0; i < 5; i++)
        {
            ResourceCard resourceCard2 = Card.CreateCard<ResourceCard>(CardPaths.stone);
            resourceCard2.gameObject.SetActive(false);
            player.itemManager.deck.AddNewCardToDeck(resourceCard2);
            cards.Add(resourceCard2);
        }

        UnitCard unitCard = Card.CreateCard<UnitCard>(CardPaths.testUnit);
        player.itemManager.deck.AddNewCardToDeck(unitCard);
        unitCard.gameObject.SetActive(false);
        cards.Add(unitCard);
    }


    // Start scene
    public void StartScene()
    {
        player.Initialize();
        CreateCards();
    }


    // Reset scene
    public void ResetScene()
    {
        player.Reset();
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        CreateCards();
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
