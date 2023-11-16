using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;
    private float battleTime;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton enemy) : base(enemyBase, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter() {
        base.Enter();
        battleTime = enemy.battleTime;
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit() {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected() && enemy.IsPlayerDetected().distance < enemy.attackDistance) {
            // Attack
            if (enemy.canAttack) {
                stateMachine.ChangeState(enemy.AttackState);
            }
        } else {
            moveDir = player.position.x < enemy.transform.position.x ? -1 : 1;
            enemy.SetVelocity(moveDir * enemy.moveSpeed, rb.velocity.y);
        }

        if (!enemy.IsPlayerDetected()) {
            battleTime -= Time.deltaTime;
            if (battleTime <= 0) {
                stateMachine.ChangeState(enemy.IdleState);
            }
        } else {
            battleTime = enemy.battleTime;
        }
    }
}
