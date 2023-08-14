using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectedCardInfoPanel : MonoBehaviour
{
    // Card
    private PlayableCard selectedCard;

    // UI elements
    public TextMeshProUGUI cardNameLabel;
    public ResourceCountContainer resourceContainer1;
    public ResourceCountContainer resourceContainer2;
    public ResourceCountContainer resourceContainer3;
    public Button showHandButton;
    public Button hideHandButton;

    // Event manager
    public SelectedCardInfoPanelEventManager eventManager { get; private set; } = new SelectedCardInfoPanelEventManager();


    // Instantiate
    public static SelectedCardInfoPanel CreateSelectedCardInfoPanel()
    {
        SelectedCardInfoPanel panel = Instantiate(Resources.Load<SelectedCardInfoPanel>("Prefabs/Player/Player UI/Selected Card Info Panel"));
        return panel;
    }


    // Initialize
    public void Initialize() { }


    // Reset
    public void Reset() { }


    // Set selected card
    public bool SetSelectedCardInfo(PlayableCard card)
    {
        void SetResourceInformation(ResourceCountContainer container, ResourceType resource, int amount)
        {
            container.SetResourceInformation(resource, amount);
            if (resource != ResourceType.None)
                container.gameObject.SetActive(true);
            else
                container.gameObject.SetActive(false);
        }

        // Hide panel
        if (card == null || card == selectedCard)
        {
            selectedCard = null;
            gameObject.SetActive(false);
            return false;
        }

        // Set new card info
        selectedCard = card;
        cardNameLabel.text = card.cardInfo.cardName;
        SetResourceInformation(resourceContainer1, card.resourceType1, card.resourceCost1);
        SetResourceInformation(resourceContainer2, card.resourceType2, card.resourceCost2);
        SetResourceInformation(resourceContainer3, card.resourceType3, card.resourceCost3);
        gameObject.SetActive(true);
        SetShowHand(false);
        return true;
    }


    // Click show hand button
    public void ShowHand(bool showHand)
    {
        showHandButton.gameObject.SetActive(!showHand);
        hideHandButton.gameObject.SetActive(showHand);
        eventManager.onClickShowHandButton.OnEvent(showHand);
    }
    public void SetShowHand(bool showHand)
    {
        showHandButton.gameObject.SetActive(!showHand);
        hideHandButton.gameObject.SetActive(showHand);
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
