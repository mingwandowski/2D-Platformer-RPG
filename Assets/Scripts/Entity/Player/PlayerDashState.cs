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
    Coroutine dashCoroutineInstance;

    public override void Enter() {
        base.Enter();
        dashDir = xInput == 0 ? player.facingDir : (int)xInput;
        Dash();

        player.skill.clone.CreateClone(player.transform.position, player.transform.rotation);
    }

    public override void Exit() {
        base.Exit();
        rb.velocity = Vector2.zero;
    }

    public override void Update() {
        base.Update();
        rb.velocity = new(dashDir * player.dashSpeed, 0);

        if (player.IsWallDetected()) {
            player.StopCoroutine(dashCoroutineInstance);
            stateMachine.ChangeState(player.WallSlideState);
        }
    }

    private void Dash() {
        dashCoroutineInstance = player.StartCoroutine(DashDuration());
    }

    private IEnumerator DashDuration() {
        yield return new WaitForSeconds(player.dashDuration);
        stateMachine.ChangeState(player.IdleState);
    }
}
