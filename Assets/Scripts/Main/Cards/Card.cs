using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // Card info
    public virtual CardInfo cardInfo { get; protected set; }
    public Image cardImage;
    public RectTransform cardFront;
    public RectTransform cardBack;
    public TextMeshProUGUI cardNameLabel;
    public TextMeshProUGUI cardDescLabel;
    public CardType cardType => cardInfo.cardType;

    // Card items
    public Image cardBackground;

    // Playable
    public bool playable { get; protected set; }

    // Event manager
    public CardEventManager eventManager { get; protected set; } = new CardEventManager();


    // Instantiate card
    public static Card CreateCard(string cardPath)
    {
        CardInfo cardInfo = CardInfo.LoadCardInfo(cardPath);
        return CreateCard(cardInfo);
    }
    public static T CreateCard<T>(string cardPath) where T : Card
    {
        return (T)CreateCard(cardPath);
    }
    public static Card CreateCard(CardInfo cardInfo)
    {
        switch (cardInfo.cardType)
        {
            case CardType.Building:
                BuildingCardInfo buildingCardInfo = (BuildingCardInfo)cardInfo;
                BuildingCard buildingCard = BuildingCard.CreateBuildingCard(buildingCardInfo);
                return buildingCard;

            case CardType.Resource:
                ResourceCardInfo resourceCardInfo = (ResourceCardInfo)cardInfo;
                ResourceCard resourceCard = ResourceCard.CreateResourceCard(resourceCardInfo);
                return resourceCard;

            case CardType.Spell:
                break;

            case CardType.Unit:
                UnitCardInfo unitCardInfo = (UnitCardInfo)cardInfo;
                UnitCard unitCard = UnitCard.CreateUnitCard(unitCardInfo);
                return unitCard;

            default:
                throw new System.Exception("Card is not of any type.");
        }
        return null;
    }


    // Set card info
    public void SetInfo(CardInfo cardInfo)
    {
        this.cardInfo = cardInfo;
        cardImage.sprite = cardInfo.cardSprite;
        cardNameLabel.text = cardInfo.cardName;
        cardDescLabel.text = cardInfo.cardDesc;
    }


    // Set whether card can be played
    public void SetPlayable(bool playable)
    {
        this.playable = playable;
        if (playable)
            cardBackground.color = Color.green;
        else
            cardBackground.color = Color.white;
    }


    // Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
        eventManager.onStartHover.OnEvent(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        eventManager.onEndHover.OnEvent(this);
    }


    // Click
    public void OnPointerClick(PointerEventData eventData)
    {
        eventManager.onLeftClick.OnEvent(this);
    }


    // Rotate card
    public void RotateCard()
    {
        gameObject.SetActive(true);
        StartCoroutine(AnimateRotate());
    }


    // Rotate card
    private IEnumerator AnimateRotate()
    {
        float t = 0;
        float rotationTime = 0.5f;

        cardFront.gameObject.SetActive(false);
        cardBack.gameObject.SetActive(true);
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        // Rotate
        while (t < 1)
        {
            // Scale card up
            transform.localScale = Vector3.Lerp(startScale, endScale, t);

            // Rotate back side then front side
            if (cardBack.eulerAngles.y < 89)
            {
                float backAngle = Mathf.Lerp(0, 90, t / rotationTime);
                cardBack.eulerAngles = new Vector3(0, backAngle);
            }
            else
            {
                cardFront.gameObject.SetActive(true);
                cardBack.gameObject.SetActive(false);
                float frontAngle = Mathf.Lerp(-90, 0, (t - 0.5f) / rotationTime);
                cardFront.eulerAngles = new Vector3(0, frontAngle);
            }

            t += Time.deltaTime;
            yield return null;
        }

        cardFront.eulerAngles = Vector3.zero;
        cardBack.eulerAngles = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);

        // Show still after rotate
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            yield return null;
        }
        eventManager.onFinishRotate.OnEvent(this);
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
