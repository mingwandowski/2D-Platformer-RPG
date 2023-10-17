using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Update() {
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (xInput == 0) {
            stateMachine.ChangeState(player.IdleState);
        }

        base.Update();
    }

    public override void Exit() {
        base.Exit();
    }
}
