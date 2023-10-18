using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();
        
        if (yInput < 0) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(rb.velocity.x, -player.wallSlideSpeed);
        }

        if (xInput * player.facingDir < 0) {
            stateMachine.ChangeState(player.AirState);
        }

        if (player.IsGroundDetected()) {
            stateMachine.ChangeState(player.IdleState);
        }

        if (Input.GetButtonDown("Jump")) {
            stateMachine.ChangeState(player.WallJumpState);
        }
    }
}
