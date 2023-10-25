using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonStunnedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();
        enemy.StartCoroutine(StunCoroutine());
        // enemy.fx.InvokeRepeating("RedColorBlink", 0, .2f);
        enemy.fx.InvokeRepeating(nameof(EntityFX.RedColorBlink), 0, .2f);
    }

    public override void Exit() {
        base.Exit();
        enemy.fx.Invoke(nameof(EntityFX.CancelRedBlink), 0);
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator StunCoroutine() {
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunDirection.x, enemy.stunDirection.y);
        yield return new WaitForSeconds(enemy.stunDuration);
        stateMachine.ChangeState(enemy.IdleState);
    }
}
