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
    private bool isReturn = false;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        if (canRotate) {
            transform.right = rb.velocity;
        }

        if (isReturn) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, returnSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 1) {
                player.CatchSword();
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
        canRotate = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        rb.transform.SetParent(other.transform);
        anim.SetBool("rotation", false);
    }

    public void ReturnSword() {
        rb.isKinematic = false;
        transform.parent = null;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        isReturn = true;
        anim.SetBool("rotation", true);
    }
}
