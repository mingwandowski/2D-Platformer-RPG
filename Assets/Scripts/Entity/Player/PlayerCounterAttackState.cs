using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private Coroutine counterAttackCoroutine;
    public PlayerCounterAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        counterAttackCoroutine = player.StartCoroutine(CounterAttack());
        player.anim.SetBool("successfulCounterAttack", false);
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update() {
        base.Update();

        rb.velocity = Vector2.zero;

        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (Collider2D hit in hits) {
            if (hit.GetComponent<Enemy>() != null) {
                if (hit.GetComponent<Enemy>().CanBeStunned()) {
                    player.StopCoroutine(counterAttackCoroutine);
                    player.anim.SetBool("successfulCounterAttack", true);
                }
            }
        }

        if (triggerCalled) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    private IEnumerator CounterAttack() {
        yield return new WaitForSeconds(player.counterAttackDuration);
        stateMachine.ChangeState(player.IdleState);
    }
}
