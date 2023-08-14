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
    public Image resource1Image;
    public Image resource2Image;
    public Image resource3Image;
    public TextMeshProUGUI resource1ProvidedLabel;
    public TextMeshProUGUI resource2ProvidedLabel;
    public TextMeshProUGUI resource3ProvidedLabel;


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


    // Set card info
    public void SetInfo(ResourceCardInfo cardInfo)
    {
        resourceCardInfo = cardInfo;
        base.SetInfo(cardInfo);

        resources[resourceCardInfo.resourceType1] = resourceCardInfo.resourceAmount1;
        resources[resourceCardInfo.resourceType2] = resourceCardInfo.resourceAmount2;
        resources[resourceCardInfo.resourceType3] = resourceCardInfo.resourceAmount3;

        resource1Image.sprite = LoadResourceSprite(resourceCardInfo.resourceType1);
        resource2Image.sprite = LoadResourceSprite(resourceCardInfo.resourceType2);
        resource3Image.sprite = LoadResourceSprite(resourceCardInfo.resourceType3);
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
