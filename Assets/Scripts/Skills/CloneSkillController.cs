using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    private Player player;
    private float cloneTimer;
    private int facingDir;
    private GameObject attackCheck;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = PlayerManager.instance.player;
    }

    private void Start() {
        anim.SetInteger("attackNumber", Random.Range(1, 4));
        cloneTimer = 3f;
    }

    private void Update() {
        if (cloneTimer > 0) {
            cloneTimer -= Time.deltaTime;
        } else {
            sr.color = Color.Lerp(sr.color, Color.clear, SkillManager.instance.clone.fadeSpeed * Time.deltaTime);
            if (sr.color.a <= 0.1f) {
                Destroy(gameObject);
            }
        }
    }

    public void SetupClone(Transform transform, GameObject parentObj) {
        attackCheck = new("AttackCheck");
        attackCheck.transform.parent = parentObj.transform;
        attackCheck.transform.localPosition = player.attackCheck.localPosition;
        this.transform.SetPositionAndRotation(transform.position, transform.rotation);
        facingDir = player.facingDir;
    }

    private void AnimationTrigger() {
        cloneTimer = -.1f;
    }

    private void AttackTrigger() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackCheck.transform.position, player.attackCheckRadius);
        foreach (Collider2D hit in hits) {
            if (hit.GetComponent<Enemy>() != null) {
                int hitDirection = transform.position.x > hit.transform.position.x ? -1 : 1;
                hit.GetComponent<Enemy>().Damage(hitDirection);
            }
        }
    }

    // private void OnDrawGizmos() {
    //     Gizmos.DrawWireSphere(attackCheck.transform.position, player.attackCheckRadius);
    // }
}
