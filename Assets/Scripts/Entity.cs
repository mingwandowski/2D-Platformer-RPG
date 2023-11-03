using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    public EntityFX fx { get; private set; }
    #endregion

    [Header("Basic info")]
    public int facingDir = 1;
    [SerializeField] protected Vector2 knockbackDir;
    [HideInInspector] public bool isKnocked;
    private bool invincible = false;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 0.5f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackCheckRadius = 1f;

    protected virtual void Awake() {

    }

    protected virtual void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
    }

    protected virtual void Update() {

    }

    public void Damage(int hitDirection) {
        if (invincible) return;
        StartCoroutine(fx.FlashFX(0.3f));
        StartCoroutine(HitKnockback(hitDirection));
        StartCoroutine(Invincible(0.3f));
    }

    protected virtual IEnumerator HitKnockback(int hitDirection) {
        isKnocked = true;
        rb.velocity = new Vector2(hitDirection * knockbackDir.x, knockbackDir.y);
        yield return new WaitForSeconds(0.1f);
        rb.velocity = Vector2.zero;
        isKnocked = false;
    }

    protected virtual IEnumerator Invincible(float invincibleDuration) {
        invincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        invincible = false;
    }

    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity) {
        if (isKnocked) return;
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir, 0), wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(facingDir, 0, 0) * wallCheckDistance);
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip() {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public virtual void FlipController(float x) {
        if (x > 0 && facingDir < 0 || x < 0 && facingDir > 0) Flip();
    }
    #endregion
}
