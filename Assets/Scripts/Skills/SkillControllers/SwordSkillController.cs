using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    [SerializeField] private float returnSpeed = 15f;
    private bool canRotate = true;
    private bool swordReturning = false;
    private bool swordBouncing = false;
    [SerializeField] private int bouncyCnt = 4;
    public List<Transform> enemyTarget = new();
    private int targetIdx = 0;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        if (canRotate) {
            transform.right = rb.velocity;
        }

        if (swordReturning) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1) {
                player.CatchSword();
            }
            return;
        }

        if (swordBouncing && bouncyCnt > 0) {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIdx].position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIdx].position) < cd.radius ) {
                targetIdx = (targetIdx + 1) % enemyTarget.Count;
                bouncyCnt--;
            }
            if (bouncyCnt == 0) {
                ReturnSword();
            }
        }
    }

    public void SetupSword(Vector2 dir, float swordGravity, Player player) {
        rb.velocity = dir;
        rb.gravityScale = swordGravity;
        this.player = player;
        anim.SetBool("rotation", true);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (swordReturning || swordBouncing) return;

        if (bouncyCnt == 0 && other.CompareTag("Enemy")) {
            ReturnSword();
        } else if (bouncyCnt >= 0 && other.CompareTag("Enemy")) {
            SetBouncyEnemyTarget(other);
        } else {
            StuckInto(other);
        }
    }

    private void SetBouncyEnemyTarget(Collider2D other) {
        if (bouncyCnt > 0 && enemyTarget.Count == 0) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
            foreach (Collider2D collider in colliders) {
                if (collider.CompareTag("Enemy")) {
                    enemyTarget.Add(collider.transform);
                }
            }
            if (enemyTarget.Count <= 1) {
                ReturnSword();
            } else {
                int currIndex = enemyTarget.IndexOf(other.transform);
                if (currIndex == 0) {
                    enemyTarget.RemoveAt(0);
                    enemyTarget.Add(other.transform);
                }
                BouncySword();
            }
        }
    }

    private void StuckInto(Collider2D collider) {
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.transform.SetParent(collider.transform);
        anim.SetBool("rotation", false);
    }

    public void ReturnSword() {
        rb.isKinematic = false;
        transform.parent = null;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        swordReturning = true;
        swordBouncing = false;
        anim.SetBool("rotation", true);
    }

    public void BouncySword() {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        swordBouncing = true;
    }
}
