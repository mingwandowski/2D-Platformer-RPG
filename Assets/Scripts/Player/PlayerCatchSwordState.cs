using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchSwordState : PlayerState
{
    public PlayerCatchSwordState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        Transform sword = player.sword.transform;
        if (sword.position.x < player.transform.position.x && player.facingDir > 0 || sword.position.x > player.transform.position.x && player.facingDir < 0) {
            player.Flip();
        }
        rb.velocity = new Vector2(2 * -player.facingDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled) {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
