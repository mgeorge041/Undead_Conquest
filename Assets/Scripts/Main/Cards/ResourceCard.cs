using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class ResourceCard : Card
{
    // Card info
    public override CardInfo cardInfo => resourceCardInfo;
    public ResourceCardInfo resourceCardInfo { get; private set; }

    // Stats
    public Dictionary<ResourceType, int> resources { get; private set; } = new Dictionary<ResourceType, int>();

    // Card stats
    public Transform resourceContainer1;
    public Transform resourceContainer2;
    public Transform resourceContainer3;
    public Image resourceIcon1;
    public Image resourceIcon2;
    public Image resourceIcon3;
    public TextMeshProUGUI resourceAmount1Label;
    public TextMeshProUGUI resourceAmount2Label;
    public TextMeshProUGUI resourceAmount3Label;


    // Load resource image
    public static Sprite LoadResourceSprite(ResourceType resource)
    {
        switch (resource)
        {
            case ResourceType.Bone:
                return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Resources/Bone Icon.png");
            case ResourceType.Corpse:
                return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Resources/Corpse Icon.png");
            case ResourceType.Mana:
                return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Resources/Mana Icon.png");
            case ResourceType.Stone:
                return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Resources/Stone Icon.png");
            case ResourceType.Wood:
                return AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Art/Resources/Wood Icon.png");
        }
        return null;
    }


    // Instantiate resource card
    public static ResourceCard CreateResourceCard()
    {
        ResourceCard card = Instantiate(Resources.Load<ResourceCard>("Prefabs/Cards/Resource Card"));
        return card;
    }
    public static ResourceCard CreateResourceCard(ResourceCardInfo resourceCardInfo)
    {
        ResourceCard card = CreateResourceCard();
        card.SetInfo(resourceCardInfo);
        return card;
    }
    public static ResourceCard CreateResourceCard(string cardPath)
    {
        ResourceCardInfo cardInfo = CardInfo.LoadCardInfo<ResourceCardInfo>(cardPath);
        return CreateResourceCard(cardInfo);
    }


    // Set card info
    public void SetInfo(ResourceCardInfo cardInfo)
    {
        resourceCardInfo = cardInfo;
        base.SetInfo(cardInfo);

        void SetCardItem(ResourceType resource, int amount, Image icon, TextMeshProUGUI label, Transform container)
        {
            if (resource == ResourceType.None)
            {
                container.gameObject.SetActive(false);
                return;
            }

            container.gameObject.SetActive(true);
            icon.sprite = LoadResourceSprite(resource);
            label.text = amount.ToString();
        }

        // Set data
        resources[resourceCardInfo.resourceType1] = resourceCardInfo.resourceAmount1;
        resources[resourceCardInfo.resourceType2] = resourceCardInfo.resourceAmount2;
        resources[resourceCardInfo.resourceType3] = resourceCardInfo.resourceAmount3;

        // Set card items
        SetCardItem(resourceCardInfo.resourceType1, resourceCardInfo.resourceAmount1, resourceIcon1, resourceAmount1Label, resourceContainer1);
        SetCardItem(resourceCardInfo.resourceType2, resourceCardInfo.resourceAmount2, resourceIcon2, resourceAmount2Label, resourceContainer2);
        SetCardItem(resourceCardInfo.resourceType3, resourceCardInfo.resourceAmount3, resourceIcon3, resourceAmount3Label, resourceContainer3);
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
