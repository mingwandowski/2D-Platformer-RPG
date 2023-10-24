using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundedState
{
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName, enemy)
    {
    }

    public override void Enter() {
        base.Enter();
        enemy.rb.velocity = Vector2.zero;
        enemy.StartCoroutine(IdleCoroutine());
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator IdleCoroutine() {
        yield return new WaitForSeconds(enemy.idleTime);
        // turn around
        enemy.Flip();
        stateMachine.ChangeState(enemy.MoveState);
    }
}
