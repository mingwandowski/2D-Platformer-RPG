using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        rb.velocity = Vector2.zero;
    }

    public override void Update() {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected()) return;

        if (xInput != 0 && !player.IsBusy) {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void Exit() {
        base.Exit();
    }
}
