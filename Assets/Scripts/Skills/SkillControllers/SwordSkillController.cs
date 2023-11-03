using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    private Player player;

    private SwordType swordType;

    [SerializeField] private float returnSpeed = 15f;
    private bool canRotate = true;
    private bool swordReturning = false;
    private bool swordBouncing = false;
    private int bounceAmount = -1;
    private List<Transform> enemyTarget = new();
    private int targetIdx = 0;
    private int pierceAmount = 0;
    private float maxRange;
    private float spinDuration;

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

        if (swordType == SwordType.Bouncy && swordBouncing && bounceAmount > 0) {
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget[targetIdx].position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, enemyTarget[targetIdx].position) < cd.radius ) {
                targetIdx = (targetIdx + 1) % enemyTarget.Count;
                bounceAmount--;
            }
            if (bounceAmount == 0) {
                ReturnSword();
            }
        }

        if (swordType == SwordType.Spin && Vector2.Distance(transform.position, player.transform.position) > maxRange) {
            StartCoroutine(SpinReturnCoroutine());
        }
    }

    private IEnumerator SpinReturnCoroutine() {
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(spinDuration);
        ReturnSword();
    }

    private void SetupBaseSword(Vector2 dir, float swordGravity, Player player) {
        rb.velocity = dir;
        rb.gravityScale = swordGravity;
        this.player = player;
        anim.SetBool("rotation", true);
    }

    public void SetupRegularSword(Vector2 dir, float swordGravity, Player player) {
        swordType = SwordType.Regular;
        SetupBaseSword(dir, swordGravity, player);
        
    }

    public void SetupBouncySword(Vector2 dir, float swordGravity, Player player, int bounceAmount) {
        swordType = SwordType.Bouncy;
        SetupBaseSword(dir, swordGravity, player);
        this.bounceAmount = bounceAmount;
    }

    public void SetupPierceSword(Vector2 dir, float swordGravity, Player player, int pierceAmount) {
        swordType = SwordType.Pierce;
        SetupBaseSword(dir, swordGravity, player);
        this.pierceAmount = pierceAmount;
    }

    public void SetupSpinSword(Vector2 dir, float swordGravity, Player player, float maxRange, float spinDuration) {
        swordType = SwordType.Spin;
        SetupBaseSword(dir, swordGravity, player);
        this.maxRange = maxRange;
        this.spinDuration = spinDuration;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (swordType != SwordType.Spin) return;
        other.GetComponent<Enemy>().Damage(other.transform.position.x > transform.position.x ? 1 : -1);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (swordType == SwordType.Spin) return;
        other.GetComponent<Enemy>()?.Damage(other.transform.position.x > transform.position.x ? 1 : -1);

        if (swordReturning || swordBouncing) return;

        if (bounceAmount == 0 && other.CompareTag("Enemy")) {
            ReturnSword();
        } else if (bounceAmount >= 0 && other.CompareTag("Enemy")) {
            SetBounceEnemyTarget(other);
        } else {
            StuckInto(other);
        }
    }

    private void SetBounceEnemyTarget(Collider2D other) {
        if (bounceAmount > 0 && enemyTarget.Count == 0) {
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
                BounceSword();
            }
        }
    }

    private void StuckInto(Collider2D collider) {
        // Pierce sword pass the enemy
        if (swordType == SwordType.Pierce && pierceAmount > 0 && collider.CompareTag("Enemy")) {
            pierceAmount--;
            return;
        }

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

    public void BounceSword() {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        swordBouncing = true;
    }
}
