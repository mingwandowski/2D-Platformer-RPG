using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
    }

    public override void Update() {
        base.Update();
        if (Input.GetAxisRaw("Horizontal") != 0) {
            player.stateMachine.ChangeState(player.moveState);
        }
    }

    public override void Exit() {
        base.Exit();
    }
}
