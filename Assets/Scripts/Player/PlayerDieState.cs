using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        // player.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        Time.timeScale = 0.7f;
        player.isDead = true;
        player.StartCoroutine(AfterDeath());
    }

    public override void Update() {
        base.Update();
    }

    public override void Exit() {
        base.Exit();
        Time.timeScale = 1;
        player.isDead = false;
    }

    private IEnumerator AfterDeath() {
        yield return new WaitForSeconds(2);
        player.stateMachine.ChangeState(player.IdleState);
    }
}
