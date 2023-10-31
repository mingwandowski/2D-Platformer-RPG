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
            throwHeight += Time.deltaTime * 10;
        } else if (Input.GetKeyUp(KeyCode.E)) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void ResetThrowForce() {
        throwHeight = 0;
    }
}
