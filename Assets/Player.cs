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
    public float moveSpeed = 12;

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    #endregion

    private void Awake() {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "idle");
        moveState = new PlayerMoveState(this, stateMachine, "move");
    }

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        stateMachine.Initialize(idleState);
    }

    private void Update() {
        stateMachine.currentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity) {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
}
