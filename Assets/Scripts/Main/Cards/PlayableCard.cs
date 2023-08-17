using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayableCard : Card
{
    // Card info
    public override CardInfo cardInfo => playableCardInfo;
    public virtual PlayableCardInfo playableCardInfo { get; protected set; }

    // Resource info
    public Dictionary<ResourceType, int> resourceCosts = new Dictionary<ResourceType, int>();
    public ResourceType resourceType1 => playableCardInfo.resourceType1;
    public ResourceType resourceType2 => playableCardInfo.resourceType2;
    public ResourceType resourceType3 => playableCardInfo.resourceType3;
    public int resourceCost1 => playableCardInfo.resourceCost1;
    public int resourceCost2 => playableCardInfo.resourceCost2;
    public int resourceCost3 => playableCardInfo.resourceCost3;

    // Card items
    public Image resource1Image;
    public Image resource2Image;
    public Image resource3Image;
    public TextMeshProUGUI resource1CostLabel;
    public TextMeshProUGUI resource2CostLabel;
    public TextMeshProUGUI resource3CostLabel;


    // Set info
    public void SetInfo(PlayableCardInfo cardInfo)
    {
        playableCardInfo = cardInfo;
        base.SetInfo(cardInfo);

        // Set resource data
        resourceCosts = new Dictionary<ResourceType, int>()
        {
            { resourceType1, resourceCost1 },
            { resourceType2, resourceCost2 },
            { resourceType3, resourceCost3 },
        };

        // Set resource icons
        resource1Image.sprite = ResourceCard.LoadResourceSprite(resourceType1);
        resource2Image.sprite = ResourceCard.LoadResourceSprite(resourceType2);
        resource3Image.sprite = ResourceCard.LoadResourceSprite(resourceType3);

        // Set resource cost labels
        resource1CostLabel.text = cardInfo.resourceCost1.ToString();
        if (cardInfo.resourceType2 != ResourceType.None)
        {
            resource2CostLabel.text = cardInfo.resourceCost2.ToString();
            resource2Image.gameObject.SetActive(true);
            resource2CostLabel.gameObject.SetActive(true);
        }
        else
        {
            resource2Image.gameObject.SetActive(false);
            resource2CostLabel.gameObject.SetActive(false);
        }
        if (cardInfo.resourceType3 != ResourceType.None)
        {
            resource3CostLabel.text = cardInfo.resourceCost3.ToString();
            resource3Image.gameObject.SetActive(true);
            resource3CostLabel.gameObject.SetActive(true);
        }
        else
        {
            resource3Image.gameObject.SetActive(false);
            resource3CostLabel.gameObject.SetActive(false);
        }
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
