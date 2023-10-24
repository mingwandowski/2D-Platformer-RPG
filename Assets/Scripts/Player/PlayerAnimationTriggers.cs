using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger() {
        player.AnimationTrigger();
    }

    private void AttackTrigger() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (Collider2D hit in hits) {
            if (hit.GetComponent<Enemy>() != null) {
                hit.GetComponent<Enemy>().Damage();
            }
        }
    }
}
