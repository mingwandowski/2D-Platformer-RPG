using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    private string animBoolName;

    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    protected bool triggerCalled;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
        xInput = Input.GetAxisRaw("Horizontal");
    }

    public virtual void Update() {
        if (UIManager.instance.menuActive) return;
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit() {
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger() {
        triggerCalled = true;
    }
}
