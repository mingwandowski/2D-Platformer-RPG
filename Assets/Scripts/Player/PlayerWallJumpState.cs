using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        player.SetVelocity(player.moveSpeed * -player.facingDir, player.jumpForce);
        player.StartCoroutine(WallJumpCoroutine());
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();
    }

    private IEnumerator WallJumpCoroutine() {
        yield return new WaitForSeconds(0.15f);
        stateMachine.ChangeState(player.AirState);
    }
}
