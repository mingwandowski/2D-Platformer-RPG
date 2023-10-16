using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; } 
    #endregion

    [Header("Move info")]
    public float moveSpeed = 8;
    public float jumpForce = 12;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    #endregion

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
    }
}
