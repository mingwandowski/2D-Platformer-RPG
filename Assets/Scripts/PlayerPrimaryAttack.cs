using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private readonly float comboWindow = .5f;

    public PlayerPrimaryAttack(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        if (comboCounter > 2 || Time.time - lastTimeAttacked > comboWindow) {
            comboCounter = 0;
        }
        player.anim.SetInteger("comboCounter", comboCounter);
    }

    public override void Exit() {
        base.Exit();
        lastTimeAttacked = Time.time;
        comboCounter++;
    }

    public override void Update() {
        base.Update();

        rb.velocity = Vector2.zero;

        if (triggerCalled) {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
