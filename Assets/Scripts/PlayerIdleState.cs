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
    }

    public override void Update() {
        if (xInput != 0) {
            stateMachine.ChangeState(player.MoveState);
        }
        base.Update();
    }

    public override void Exit() {
        base.Exit();
    }
}
