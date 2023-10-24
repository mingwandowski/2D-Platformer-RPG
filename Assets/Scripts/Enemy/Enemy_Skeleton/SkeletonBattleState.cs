using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance) {
            // Attack
            stateMachine.ChangeState(enemy.AttackState);
        } else {
            moveDir = player.position.x < enemy.transform.position.x ? -1 : 1;
            enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);
        }
    }
}
