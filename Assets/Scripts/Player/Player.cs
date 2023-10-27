using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    public PlayerCounterAttackState CounterAttackState { get; private set; }
    #endregion

    #region Status
    public bool IsBusy { get; private set; }
    #endregion

    [Header("Move info")]
    public float moveSpeed = 8;
    public float jumpForce = 12;

    [Header("Attack info")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    [Header("Dash info")]
    public float dashDuration = 0.2f;
    public float dashSpeed = 20;

    [Header("Wall slide info")]
    public float wallSlideSpeed = 3;

    protected override void Awake() {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, stateMachine, "idle");
        MoveState = new PlayerMoveState(this, stateMachine, "move");
        JumpState = new PlayerJumpState(this, stateMachine, "jump");
        AirState = new PlayerAirState(this, stateMachine, "jump");
        DashState = new PlayerDashState(this, stateMachine, "dash");
        WallSlideState = new PlayerWallSlideState(this, stateMachine, "wallSlide");
        WallJumpState = new PlayerWallJumpState(this, stateMachine, "jump");

        PrimaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "attack");
        CounterAttackState = new PlayerCounterAttackState(this, stateMachine, "counterAttack");
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(IdleState);
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput() {
        if (IsWallDetected()) return;
        if (Input.GetKeyDown(KeyCode.L) && SkillManager.instance.dash.CanUseSkill()) {
            stateMachine.ChangeState(DashState);
        }
    }

    public IEnumerator BusyFor(float seconds) {
        IsBusy = true;
        yield return new WaitForSeconds(seconds);
        IsBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
