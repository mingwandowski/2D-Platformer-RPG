using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    #endregion

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance = 0.5f;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance = 0.5f;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir = 1;

    protected virtual void Awake() {

    }

    protected virtual void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
    }

    protected virtual void Update() {

    }

    #region Velocity
    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir, 0), wallCheckDistance, whatIsGround);

    protected virtual void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(facingDir, 0, 0) * wallCheckDistance);
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
