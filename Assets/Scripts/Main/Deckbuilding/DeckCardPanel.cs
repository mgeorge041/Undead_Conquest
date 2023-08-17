using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCardPanel : MonoBehaviour
{
    // Card info
    private CardInfo cardInfo;

    // UI elements
    public TextMeshProUGUI cardNameLabel;
    public TextMeshProUGUI cardCountLabel;
    public PixelButton addCardButton;
    public PixelButton removeCardButton;

    // Event manager
    public DeckCardPanelEventManager eventManager { get; private set; } = new DeckCardPanelEventManager();

    
    // Instantiate
    public static DeckCardPanel CreateDeckCardPanel()
    {
        DeckCardPanel panel = Instantiate(Resources.Load<DeckCardPanel>("Prefabs/Deckbuilding/Deck Card Panel"));
        return panel;
    }


    // Set card panel info
    public void SetCardPanelInfo(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        cardNameLabel.text = cardInfo.cardName;
    }


    // Get current card count
    public int GetCardCount()
    {
        return int.Parse(cardCountLabel.text);
    }


    // Increase card count
    public void IncrementCardCount(int amount)
    {
        int currentAmount = int.Parse(cardCountLabel.text);
        int newAmount = Mathf.Max(0, currentAmount + amount);
        cardCountLabel.text = newAmount.ToString();
    }


    // Click add button
    public void ClickAddCardButton()
    {
        eventManager.onAddCard.OnEvent(cardInfo);
    }
    public void ClickRemoveCardButton()
    {
        eventManager.onRemoveCard.OnEvent(cardInfo);
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
