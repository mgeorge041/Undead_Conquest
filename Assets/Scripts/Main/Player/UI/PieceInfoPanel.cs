using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class PieceInfoPanel : MonoBehaviour
{
    // Initialization
    public bool initialized { get; private set; }

    // Piece
    private Piece setPiece;

    // UI elements
    public Image pieceArt;
    public Image attackIcon;
    public Image speedIcon;
    public Image manaIcon;
    public TextMeshProUGUI pieceNameLabel;
    public TextMeshProUGUI attackLabel;
    public TextMeshProUGUI defenseLabel;
    public TextMeshProUGUI rangeLabel;
    public TextMeshProUGUI sightLabel;
    public TextMeshProUGUI healthLabel;
    public TextMeshProUGUI speedLabel;
    public TextMeshProUGUI manaLabel;


    // Instantiate
    public static PieceInfoPanel CreatePieceInfoPanel()
    {
        PieceInfoPanel panel = Instantiate(Resources.Load<PieceInfoPanel>("Prefabs/Player/Player UI/Piece Info Panel"));
        panel.Initialize();
        return panel;
    }


    // Initialize
    public void Initialize() 
    {
        attackIcon.material = Instantiate(AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Outline Material.mat"));
        speedIcon.material = Instantiate(AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Outline Material.mat"));
        initialized = true;
    }


    // Reset
    public void Reset()
    {
        setPiece = null;
        pieceArt.sprite = null;
        pieceNameLabel.text = "";
        attackLabel.text = 0.ToString();
        defenseLabel.text = 0.ToString();
        rangeLabel.text = 0.ToString();
        sightLabel.text = 0.ToString();
        healthLabel.text = 0.ToString() + "/" + 0.ToString();
        manaLabel.text = 0.ToString() + "/" + 0.ToString();
        speedLabel.text = 0.ToString() + "/" + 0.ToString();
    }


    // Set piece info
    public void SetPiece(Piece piece)
    {
        if (piece == null || piece == setPiece)
        {
            return;
        }

        UnsubscribePieceEvents();
        setPiece = piece;
        SubscribePieceEvents();

        // Set sprite
        pieceArt.sprite = piece.playableCardInfo.cardSprite;
        SetHasActionsInfo(piece.pieceData.hasActions);

        // Set stats
        pieceNameLabel.text = piece.playableCardInfo.cardName;
        attackLabel.text = piece.pieceData.attack.ToString();
        defenseLabel.text = piece.pieceData.defense.ToString();
        rangeLabel.text = piece.pieceData.range.ToString();
        sightLabel.text = piece.pieceData.range.ToString();
        healthLabel.text = piece.pieceData.currentHealth.ToString() + "/" + piece.pieceData.health.ToString();

        // Set mana
        if (piece.pieceData.mana == 0)
        {
            ShowManaInfo(false);
        }
        else
        {
            ShowManaInfo(true);
            SetManaInfo();
        }

        // Set speed
        if (piece.isUnit)
        {
            Unit unit = (Unit)piece;
            SetSpeedInfo(unit.unitData.currentSpeed, unit.unitData.speed);
        }
        else
        {
            ShowSpeedInfo(false);
        }
    }


    // Subscribe to piece events
    private void SubscribePieceEvents()
    {
        setPiece.pieceData.eventManager.onSetHasActions.Subscribe(HandlePieceSetHasActions);
        setPiece.eventManager.onChangeHealth.Subscribe(HandlePieceChangeHealth);
        if (setPiece.isUnit)
        {
            Unit unit = (Unit)setPiece;
            unit.unitEventManager.onChangeSpeed.Subscribe(HandleUnitChangeSpeed);
        }
    }
    private void UnsubscribePieceEvents()
    {
        if (setPiece == null)
            return;

        setPiece.pieceData.eventManager.onSetHasActions.Unsubscribe(HandlePieceSetHasActions);
        setPiece.eventManager.onChangeHealth.Unsubscribe(HandlePieceChangeHealth);
        if (setPiece.isUnit)
        {
            Unit unit = (Unit)setPiece;
            unit.unitEventManager.onChangeSpeed.Unsubscribe(HandleUnitChangeSpeed);
        }
    }


    // Set mana info
    private void SetManaInfo()
    {
        manaLabel.text = setPiece.pieceData.currentMana.ToString() + "/" + setPiece.pieceData.mana.ToString();
    }
    private void ShowManaInfo(bool showInfo)
    {
        manaIcon.transform.parent.gameObject.SetActive(showInfo);
    }


    // Set speed info
    private void SetSpeedInfo(int currentSpeed, int speed)
    {
        speedLabel.text = currentSpeed + "/" + speed.ToString();
        if (currentSpeed > 0)
        {
            speedIcon.material.SetColor("Outline_Color", Color.green);
            speedIcon.material.SetFloat("Outside", -1);
        }
        else
        {
            speedIcon.material.SetColor("Outline_Color", Color.clear);
            speedIcon.material.SetFloat("Outside", 1);
        }
        ShowSpeedInfo(true);
    }
    private void ShowSpeedInfo(bool showInfo)
    {
        speedIcon.transform.parent.gameObject.SetActive(showInfo);
    }


    // Set has actions info
    private void SetHasActionsInfo(bool hasActions)
    {
        if (hasActions)
        {
            attackIcon.material.SetColor("Outline_Color", Color.green);
            attackIcon.material.SetFloat("Outside", -1);
        }
        else
        {
            attackIcon.material.SetFloat("Outside", 0);
        }

        if (setPiece.isUnit)
        {
            Unit unit = (Unit)setPiece;
            SetSpeedInfo(unit.unitData.currentSpeed, unit.unitData.speed);
        }
    }


    // Handle piece has actions
    private void HandlePieceSetHasActions(bool hasActions)
    {
        SetHasActionsInfo(hasActions);
    }


    // Handle change in piece health
    private void HandlePieceChangeHealth(int currentHealth, int health)
    {
        healthLabel.text = currentHealth.ToString() + "/" + health.ToString();
    }


    // Handle change in unit speed
    private void HandleUnitChangeSpeed(int currentSpeed, int speed)
    {
        SetSpeedInfo(currentSpeed, speed);
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
