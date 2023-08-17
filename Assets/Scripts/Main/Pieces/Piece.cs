using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    Building,
    Unit,
    None
}

public enum PieceStatType
{
    Attack,
    Defense,
    Health,
    CurrentHealth,
    Mana,
    CurrentMana,
    Range,
    Speed,
    CurrentSpeed,
    None
}


public abstract class Piece : MonoBehaviour
{
    public virtual PieceType pieceType { get; }
    public virtual PlayableCardInfo playableCardInfo { get; protected set; }
    public virtual PieceData pieceData { get; protected set; } = new PieceData();
    public Hex hex { get; protected set; }

    // Piece info
    public bool isBuilding => pieceType == PieceType.Building;
    public bool isUnit => pieceType == PieceType.Unit;
    public Unit unit => isUnit ? (Unit)this : null;

    // Animation
    public virtual PieceAnimator pieceAnimator { get; protected set; }


    // Start action
    public StartAction startAction { get; protected set; }
    public bool hasStartAction => startAction != null;

    // Event manager
    public virtual PieceEventManager eventManager { get; protected set; } = new PieceEventManager();

    
    // Instantiate piece
    public static Piece CreatePiece(string piecePath)
    {
        CardInfo cardInfo = CardInfo.LoadCardInfo(piecePath);
        if (!cardInfo.GetType().IsSubclassOf(typeof(PlayableCardInfo)))
            throw new System.ArgumentException("Cannot create piece from non-playable card info.");

        return CreatePiece((PlayableCardInfo)cardInfo);
    }
    public static Piece CreatePiece(PlayableCardInfo cardInfo)
    {
        switch (cardInfo.cardType)
        {
            case CardType.Building:
                BuildingCardInfo buildingCardInfo = (BuildingCardInfo)cardInfo;
                Building building = Building.CreateBuilding(buildingCardInfo);
                return building;

            case CardType.Unit:
                UnitCardInfo unitCardInfo = (UnitCardInfo)cardInfo;
                Unit unit = Unit.CreateUnit(unitCardInfo);
                return unit;

            default:
                throw new System.ArgumentException("Piece is not of any type.");
        }
    }
    public static T CreatePiece<T>(string piecePath) where T : Piece
    {
        return (T)CreatePiece(piecePath);
    }


    // Initialize
    public virtual void Initialize()
    {
        pieceAnimator.SetPiece(this);
    }


    // Create start action
    protected void CreateStartAction(StartActionInfo startActionInfo)
    {
        if (startActionInfo == null)
            return;

        startAction = StartAction.CreateStartAction(startActionInfo);
    }


    // End turn
    public virtual void EndTurn() 
    {
        pieceData.SetHasActions(true);
    }

    
    // Set hex
    public void SetHex(Hex hex)
    {
        this.hex = hex;
    }
    public void SetHexPosition(Hex hex)
    {
        transform.position = hex.pathNode.worldPosition;
    }
    public void SetHexAndPosition(Hex hex)
    {
        SetHex(hex);
        SetHexPosition(hex);
    }


    // Get whether piece is friendly
    public bool IsFriendly(Piece piece)
    {
        return piece.pieceData.playerId == pieceData.playerId;
    }


    // Perform start action
    public virtual void PerformStartAction(PlayerItemManager itemManager) 
    {
        if (startAction != null)
            startAction.PerformStartAction(this, itemManager);
    }


    // Attack target hex
    public void AttackHex(Hex targetHex)
    {
        if (targetHex == null || !targetHex.hasPiece)
            throw new System.ArgumentNullException("Cannot attack null hex or hex without piece.");

        pieceAnimator.AttackHex(targetHex);
        targetHex.piece.TakeDamage(pieceData.attack);
        pieceData.SetCanAttack(false);
    }


    // Take damage
    public int CalculateDamageTaken(int damage)
    {
        if (damage > pieceData.currentHealth)
            return pieceData.currentHealth;
        else
            return damage;
    }
    public void TakeDamage(int damage)
    {
        pieceData.AddStat(PieceStatType.CurrentHealth, -CalculateDamageTaken(damage));
        pieceAnimator.TakeDamage(pieceData.currentHealth, pieceData.health);
        eventManager.onChangeHealth.OnEvent(pieceData.currentHealth, pieceData.health);
        if (pieceData.currentHealth <= 0)
            Die();
    }


    // Die
    public void Die()
    {
        eventManager.onDeath.OnEvent(this);
        C.DestroyGameObject(gameObject);
    }
}
