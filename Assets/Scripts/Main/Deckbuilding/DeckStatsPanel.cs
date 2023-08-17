using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DeckStatsPanel : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Resources
    private Dictionary<ResourceType, TextMeshProUGUI> resourceHaveLabels;
    private Dictionary<ResourceType, TextMeshProUGUI> resourceCostLabels;

    // UI elements
    public TextMeshProUGUI deckCountLabel;
    public TextMeshProUGUI unitCountLabel;
    public TextMeshProUGUI buildingCountLabel;
    public TextMeshProUGUI resourceCountLabel;
    public TextMeshProUGUI boneHaveLabel;
    public TextMeshProUGUI corpseHaveLabel;
    public TextMeshProUGUI manaHaveLabel;
    public TextMeshProUGUI stoneHaveLabel;
    public TextMeshProUGUI woodHaveLabel;
    public TextMeshProUGUI boneCostLabel;
    public TextMeshProUGUI corpseCostLabel;
    public TextMeshProUGUI manaCostLabel;
    public TextMeshProUGUI stoneCostLabel;
    public TextMeshProUGUI woodCostLabel;


    // Instantiate
    public static DeckStatsPanel CreateDeckStatsPanel()
    {
        DeckStatsPanel panel = Instantiate(Resources.Load<DeckStatsPanel>("Prefabs/Deckbuilding/Deck Stats Panel"));
        panel.Initialize();
        return panel;
    }


    // Initialize
    public void Initialize()
    {
        resourceHaveLabels = new Dictionary<ResourceType, TextMeshProUGUI>()
        {
            { ResourceType.Bone, boneHaveLabel },
            { ResourceType.Corpse, corpseHaveLabel },
            { ResourceType.Mana, manaHaveLabel },
            { ResourceType.Stone, stoneHaveLabel },
            { ResourceType.Wood, woodHaveLabel },
        };
        resourceCostLabels = new Dictionary<ResourceType, TextMeshProUGUI>()
        {
            { ResourceType.Bone, boneCostLabel },
            { ResourceType.Corpse, corpseCostLabel },
            { ResourceType.Mana, manaCostLabel },
            { ResourceType.Stone, stoneCostLabel },
            { ResourceType.Wood, woodCostLabel },
        };
        initialized = true;
    }


    // Set count label
    public void SetCountLabel(CardType cardType, int numCards)
    {
        if (numCards < 0)
            throw new System.ArgumentException("Cannot set number of cards less than 0.");

        switch (cardType)
        {
            case CardType.Building:
                buildingCountLabel.text = numCards.ToString();
                break;
            case CardType.Resource:
                resourceCountLabel.text = numCards.ToString();
                break;
            case CardType.Unit:
                unitCountLabel.text = numCards.ToString();
                break;
            default:
                throw new System.ArgumentException("Cannot set count for None card type.");
        }
    }


    // Set total number of deck cards label
    public void SetDeckCountLabel(int numCards)
    {
        if (numCards < 0)
            throw new System.ArgumentException("Cannot set number of deck cards less than 0.");

        deckCountLabel.text = numCards.ToString();
    }


    // Set resources label
    public void SetResourceProvidedLabel(ResourceType resource, int amount)
    {
        SetResourceLabel(resourceHaveLabels, resource, amount);
    }
    public void SetResourceCostLabel(ResourceType resource, int amount)
    {
        SetResourceLabel(resourceCostLabels, resource, amount);
    }
    private void SetResourceLabel(Dictionary<ResourceType, TextMeshProUGUI> labels, ResourceType resource, int amount)
    {
        if (!labels.ContainsKey(resource))
            return;

        labels[resource].text = amount.ToString();
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
