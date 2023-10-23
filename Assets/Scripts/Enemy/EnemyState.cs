using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Enemy enemy;
    protected EnemyStateMachine stateMachine;
    protected bool triggerCalled;
    private string animBoolName;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        triggerCalled = false;
        enemy.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() {
        enemy.anim.SetBool(animBoolName, false);
    }

    public virtual void Update() {

    }
}
