using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private readonly float comboWindow = .5f;
    private float attackDir;

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        attackDir = xInput == 0 ? player.facingDir : xInput;
        if (comboCounter > 2 || Time.time - lastTimeAttacked > comboWindow) {
            comboCounter = 0;
        }
        player.anim.SetInteger("comboCounter", comboCounter);
        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);
        player.StartCoroutine(InertiaTimer());
    }

    public override void Exit() {
        base.Exit();
        lastTimeAttacked = Time.time;
        comboCounter++;

        player.StartCoroutine(player.BusyFor(.1f));
    }

    public override void Update() {
        base.Update();

        if (triggerCalled) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private IEnumerator InertiaTimer() {
        // For inertia
        yield return new WaitForSeconds(.1f);
        rb.velocity = Vector2.zero;
    }
}
