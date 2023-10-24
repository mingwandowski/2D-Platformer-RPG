using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();

    private void AnimationTrigger() {
        enemy.AnimationTrigger();
    }

    private void AttackTrigger() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        foreach (Collider2D hit in hits) {
            if (hit.GetComponent<Player>() != null) {
                hit.GetComponent<Player>().Damage();
            }
        }
    }
}
