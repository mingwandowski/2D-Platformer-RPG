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

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update() {
        xInput = Input.GetAxisRaw("Horizontal");
    }

    public virtual void Exit() {
        player.anim.SetBool(animBoolName, false);
    }
}
