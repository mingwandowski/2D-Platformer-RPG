using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton enemy;
    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();
        enemy.canAttack = false;
    }

    public override void Exit() {
        base.Exit();
        enemy.StartCoroutine(CooldownCoroutine());
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;

        if (triggerCalled) {
            stateMachine.ChangeState(enemy.BattleState);
        }
    }

    private IEnumerator CooldownCoroutine() {
        yield return new WaitForSeconds(enemy.attackCooldown);
        enemy.canAttack = true;
    }
}
