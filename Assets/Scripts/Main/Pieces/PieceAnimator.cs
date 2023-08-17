using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PieceAnimator : MonoBehaviour
{
    // Piece info
    public virtual Piece piece { get; protected set; }
    
    // UI elements
    public Fillbar healthbar;
    public float healthbarFillTime = 0.5f;
    
    // Coroutine pausing
    protected int pauseCoroutine = 1;

    // Animation
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    

    // Instantiate
    public static PieceAnimator CreatePieceAnimator()
    {
        PieceAnimator animator = Instantiate(Resources.Load<PieceAnimator>("Prefabs/Pieces/Piece Animator"));
        return animator;
    }
    public static PieceAnimator CreatePieceAnimator<T>() where T : PieceAnimator
    {
        return (T)CreatePieceAnimator();
    }


    // Set piece
    public virtual void SetPiece(Piece piece)
    {
        this.piece = piece;
        piece.pieceData.eventManager.onSetHasActions.Subscribe(HandlePieceHasActions);
    }


    // Pause coroutines
    public void PauseCoroutines(bool pause)
    {
        if (pause)
            pauseCoroutine = 0;
        else
            pauseCoroutine = 1;
    }


    // Show piece enabled
    public void ShowEnabled(bool enabled)
    {
        if (enabled)
            spriteRenderer.color = Color.white;
        else
            spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);
    }


    // Handle changing whether the piece has actions
    protected void HandlePieceHasActions(bool hasActions)
    {
        ShowEnabled(hasActions);
    }


    // Attack target hex
    public void AttackHex(Hex targetHex)
    {
        if (targetHex == null || targetHex.piece == null)
            throw new System.ArgumentNullException("Cannot attack null hex or hex with null piece.");

        StartCoroutine(AnimateAttack(targetHex));
    }


    // Animate attack
    private IEnumerator AnimateAttack(Hex targetHex)
    {
        float t = 0;
        Vector3 startPosition = piece.transform.position;
        Vector3 direction = (targetHex.pathNode.worldPosition - startPosition);
        Vector3 attackPosition = startPosition + direction * 0.5f;
        piece.transform.position = attackPosition;
        while (t < 0.5f)
        {
            piece.transform.position = Vector3.Lerp(attackPosition, startPosition, t * 2);
            t += Time.deltaTime;
            yield return null;
        }
        piece.transform.position = startPosition;
        FinishAttackAnimation();
    }


    // Finish attack animation
    public void FinishAttackAnimation()
    {
        piece.eventManager.onFinishAttackAnimation.OnEvent(piece);
    }


    // Animate take damage
    public void TakeDamage(int currentHealth, int health)
    {
        float fillAmount = (float)currentHealth / health;
        healthbar.gameObject.SetActive(true);
        StartCoroutine(AnimateHealthbarFill(fillAmount));
        StartCoroutine(AnimateTakeDamage());
    }


    // End taking damage
    public void EndTakeDamage()
    {


    }


    // Animate take damage
    private IEnumerator AnimateTakeDamage()
    {
        float t = 0;
        while (t < 1)
        {
            if (t < 0.25f)
                spriteRenderer.color = Color.red;
            else if (0.25f <= t && t < 0.5f)
                spriteRenderer.color = Color.white;
            else if (0.5f <= t && t < 0.75f)
                spriteRenderer.color = Color.red;
            else 
                spriteRenderer.color = Color.white;
            t += Time.deltaTime * pauseCoroutine;
            yield return null;
        }
        EndTakeDamage();
    }


    // Animate healthbar fill
    private IEnumerator AnimateHealthbarFill(float fillAmount)
    {
        float startFill = healthbar.fill;
        float t = 0;

        while (t < 1)
        {
            healthbar.SetFill(Mathf.Lerp(startFill, fillAmount, t / healthbarFillTime));
            t += Time.deltaTime * pauseCoroutine;
            yield return null;
        }
        healthbar.gameObject.SetActive(false);
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
