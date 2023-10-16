using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    #endregion

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    #endregion

    [Header("Move info")]
    public float moveSpeed = 8;
    public float jumpForce = 12;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir = 1;

    private void Awake() {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        JumpState = new PlayerJumpState(this, stateMachine, "jump");
        AirState = new PlayerAirState(this, stateMachine, "jump");
    }

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        stateMachine.Initialize(IdleState);
    }

    private void Update() {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    private void OnDrawGizmos() {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + new Vector3(facingDir, 0, 0) * wallCheckDistance);
    }

    public void Flip() {
        facingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float x) {
        if (x > 0 && facingDir < 0 || x < 0 && facingDir > 0) Flip();
    }
    
}
