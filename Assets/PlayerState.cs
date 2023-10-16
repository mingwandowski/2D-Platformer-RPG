using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName) {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() {
        player.anim.SetBool(animBoolName, true);
    }

    public virtual void Update() {
        
    }

    public virtual void Exit() {
        player.anim.SetBool(animBoolName, false);
    }
}
