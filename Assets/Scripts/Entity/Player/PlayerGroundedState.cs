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

        if (Input.GetKeyDown(KeyCode.Q)) {
            stateMachine.ChangeState(player.CounterAttackState);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            if (!player.sword) {
                stateMachine.ChangeState(player.AimSwordState);
            } else {
                player.sword.GetComponent<SwordSkillController>().ReturnSword();
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            stateMachine.ChangeState(player.BlackHoleState);
        }

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
