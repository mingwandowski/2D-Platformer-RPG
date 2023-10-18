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
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    #endregion

    [Header("Move info")]
    public float moveSpeed = 8;
    public float jumpForce = 12;
    public int facingDir = 1;

    [Header("Dash info")]
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float dashSpeed = 20;
    public bool canDash = true;

    [Header("Wall slide info")]
    public float wallSlideSpeed = 3;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.5f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.5f;
    [SerializeField] private LayerMask whatIsGround;


    private void Awake() {
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        JumpState = new PlayerJumpState(this, stateMachine, "jump");
        AirState = new PlayerAirState(this, stateMachine, "jump");
        DashState = new PlayerDashState(this, stateMachine, "dash");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, "jump");
    }

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        stateMachine.Initialize(IdleState);
    }

    private void Update() {
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput() {
        if (IsWallDetected()) return;
        if (canDash && Input.GetKeyDown(KeyCode.L)) {
            stateMachine.ChangeState(DashState);
        }
    }

    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, new Vector2(facingDir, 0), wallCheckDistance, whatIsGround);

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
