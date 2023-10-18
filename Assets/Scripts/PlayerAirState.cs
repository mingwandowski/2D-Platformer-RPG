using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (player.IsWallDetected()) {
            stateMachine.ChangeState(player.WallSlideState);
        }

        if (player.IsGroundDetected()) {
            rb.velocity = Vector2.zero;
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
