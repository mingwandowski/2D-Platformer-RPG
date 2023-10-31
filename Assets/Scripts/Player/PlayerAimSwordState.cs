using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAimSwordState : PlayerState
{
    public PlayerAimSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (Input.GetKey(KeyCode.E)) {
            Debug.Log(123);
        } else if (Input.GetKeyUp(KeyCode.E)) {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
