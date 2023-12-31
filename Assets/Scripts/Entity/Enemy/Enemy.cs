using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    public float moveSpeed = 2f;
    public float idleTime = 2f;
    public float battleTime = 5f;
    private float defaultMoveSpeed;

    [Header("Detection info")]
    [SerializeField] protected LayerMask whatIsPlayer;
    public float playerDetectionRange = 10;

    [Header("Attack info")]
    public float attackDistance = 1f;
    public float attackCooldown = 0f;
    [HideInInspector] public bool canAttack = true;

    [Header("Stunned info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;
    public Action OnFlipped;

    public EnemyStateMachine stateMachine;

    protected override void Awake() {
        base.Awake();
        stateMachine = new EnemyStateMachine();
        defaultMoveSpeed = moveSpeed;
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();
    }

    public override void Flip() {
        base.Flip();

        OnFlipped.Invoke();
    }

    public virtual void FreezeTime(bool freeze) {
        if (freeze) {
            moveSpeed = 0;
            anim.speed = 0;
        } else {
            moveSpeed = defaultMoveSpeed;
            if (anim != null) anim.speed = 1;
        }
    }

    public virtual IEnumerator DestroyEnemy() {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public virtual void OpenConterAttackWindow() {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow() {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual bool CanBeStunned() {
        if (canBeStunned) {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position, new Vector2(facingDir, 0), playerDetectionRange, whatIsPlayer);

    protected override void OnDrawGizmos() {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(facingDir * playerDetectionRange, 0, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.1f, 0), transform.position + new Vector3(facingDir * attackDistance, 0.1f, 0));
    }
}
