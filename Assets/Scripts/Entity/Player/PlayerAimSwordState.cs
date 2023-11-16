using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public float throwHeight = 0;

    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.skill.sword.SetDotsActive(true);
    }

    public override void Exit() {
        base.Exit();
        player.skill.sword.SetDotsActive(false);
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKey(KeyCode.E)) {
            rb.velocity = Vector2.zero;
            throwHeight += Time.deltaTime * 10;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            player.anim.SetBool("aimSword", false);
        }
    }

    public void ThrowSwordFinish() {
        throwHeight = 0;
        stateMachine.ChangeState(player.IdleState);
    }
}
