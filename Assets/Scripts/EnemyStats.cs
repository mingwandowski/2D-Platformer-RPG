using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;

    protected override void Start() {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    protected override void Die()
    {
        base.Die();
        enemy.stateMachine.currentState.Exit();
        enemy.anim.SetTrigger("die");
        enemy.moveSpeed = 0;
        enemy.isDead = true;
        enemy.StartCoroutine(enemy.DestroyEnemy());
    }
}
