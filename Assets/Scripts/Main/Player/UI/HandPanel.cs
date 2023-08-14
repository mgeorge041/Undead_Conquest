using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandPanel : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Card container
    public int numCards => cardContainer.childCount;
    public Transform cardContainer;
    public TextMeshProUGUI numDeckCardsLabel;
    public TextMeshProUGUI numHandCardsLabel;
    public TextMeshProUGUI numDiscardCardsLabel;
    public Button openHandButton;
    public Button closeHandButton;
    public Transform deckCardContainer;

    // Opening hand
    private bool isOpen = true;


    // Instantiate
    public static HandPanel CreateHandPanel()
    {
        HandPanel panel = Instantiate(Resources.Load<HandPanel>("Prefabs/Player/Player UI/Hand Panel"));
        panel.Initialize();
        return panel;
    }


    // Initialize
    public void Initialize()
    {
        initialized = true;
    }


    // Reset
    public void Reset()
    {
        cardContainer.DetachChildren();
        numDeckCardsLabel.text = 0.ToString();
        numHandCardsLabel.text = 0.ToString();
    }


    // Add or remove card
    public void AddCard(Card card)
    {
        card.transform.SetParent(cardContainer);
        card.gameObject.SetActive(true);
        SetNumHandCards(numCards);
    }
    public void RemoveCard(Card card)
    {
        if (card.transform.parent != cardContainer)
            throw new System.ArgumentException("Cannot remove card from hand when not already in hand.");

        card.transform.SetParent(null);
        card.gameObject.SetActive(false);
        SetNumHandCards(numCards);
    }


    // Set number of cards in the hand
    public void SetNumHandCards(int numCards)
    {
        numHandCardsLabel.text = numCards.ToString();
    }


    // Add card to the deck
    public void AddCardToDeck(Card card)
    {
        card.transform.SetParent(deckCardContainer);
    }


    // Set number of cards in the deck
    public void SetNumDeckCards(int numCards, int totalNumCards)
    {
        numDeckCardsLabel.text = numCards.ToString() + "/" + totalNumCards.ToString();
    }
    public void SetNumTotalDeckCards(int numTotalCards)
    {
        string[] numCardsLabelSplit = numDeckCardsLabel.text.Split('/');
        numDeckCardsLabel.text = numCardsLabelSplit[0] + "/" + numTotalCards.ToString();
    }


    // Set number of cards in the discard pile
    public void SetNumDiscardCards(int numCards)
    {
        numDiscardCardsLabel.text = numCards.ToString();
    }


    // Show hand
    public void ShowHand(bool showHand)
    {
        gameObject.SetActive(showHand);
    }
    public void SetShowHand(bool showHand) 
    {
        openHandButton.gameObject.SetActive(!showHand);
        closeHandButton.gameObject.SetActive(showHand);
    }


    // Minimize hand
    public void OpenHand(bool open)
    {
        Vector3 hideOffset = new Vector3(0, 110);

        if (open && !isOpen)
            transform.position += hideOffset;
        else if (!open && isOpen)
            transform.position -= hideOffset;
        isOpen = open;

        openHandButton.gameObject.SetActive(!open);
        closeHandButton.gameObject.SetActive(open);
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
