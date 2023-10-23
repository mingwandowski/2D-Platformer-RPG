using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Move info")]
    public float moveSpeed = 2f;
    public float idleTime = 2f;

    public EnemyStateMachine stateMachine;

    protected override void Awake() {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }

    protected override void Update() {
        base.Update();
        stateMachine.currentState.Update();
    }
}
