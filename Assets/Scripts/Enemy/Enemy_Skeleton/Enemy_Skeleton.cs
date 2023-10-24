using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States
    public SkeletonIdleState IdleState { get; private set; }
    public SkeletonMoveState MoveState { get; private set; }
    public SkeletonBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    #endregion
    protected override void Awake() {
        base.Awake();
        
        IdleState = new SkeletonIdleState(this, stateMachine, "idle", this);
        MoveState = new SkeletonMoveState(this, stateMachine, "move", this);
        BattleState = new SkeletonBattleState(this, stateMachine, "move", this);
        AttackState = new SkeletonAttackState(this, stateMachine, "attack", this);
    }

    protected override void Start() {
        base.Start();
        stateMachine.Initialize(IdleState);
    }

    protected override void Update() {
        base.Update();
    }
}
