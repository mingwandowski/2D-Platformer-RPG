using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (Input.GetKeyDown(KeyCode.J)) {
            stateMachine.ChangeState(player.PrimaryAttackState);
        }

        if (Input.GetButtonDown("Jump") && player.IsGroundDetected()) {
            stateMachine.ChangeState(player.JumpState);
        }

        if (!player.IsGroundDetected()) {
            stateMachine.ChangeState(player.AirState);
        }
    }
}
