using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private int dashDir;

    public override void Enter() {
        base.Enter();
        dashDir = player.facingDir;
        Dash();
    }

    public override void Exit() {
        base.Exit();
        rb.velocity = Vector2.zero;
    }

    public override void Update() {
        base.Update();
        rb.velocity = new(dashDir * player.dashSpeed, 0);
    }

    private void Dash() {
        player.canDash = false;
        player.StartCoroutine(DashDuration());
        player.StartCoroutine(DashCooldown());
    }

    private IEnumerator DashDuration() {
        yield return new WaitForSeconds(player.dashDuration);
        stateMachine.ChangeState(player.IdleState);
    }

    private IEnumerator DashCooldown() {
        yield return new WaitForSeconds(player.dashCooldown);
        player.canDash = true;
    }
}
