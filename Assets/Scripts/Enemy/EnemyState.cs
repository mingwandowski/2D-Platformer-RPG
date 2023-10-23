using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected bool triggerCalled;
    private string animBoolName;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        triggerCalled = false;
        Debug.Log(enemyBase);
        Debug.Log(enemyBase.anim);
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void Update() {

    }
}
