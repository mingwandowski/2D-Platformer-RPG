using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlackHoleState : PlayerState
{
    private float baseGravityScale;
    public PlayerBlackHoleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter() {
        base.Enter();
        baseGravityScale = rb.gravityScale;
        player.StartCoroutine(FlyToTheAir());
    }

    public override void Exit() {
        base.Exit();
        rb.gravityScale = baseGravityScale;
        player.anim.speed = 1;
    }

    public override void Update() {
        base.Update();
    }

    private IEnumerator FlyToTheAir() {
        rb.gravityScale = 0;
        rb.velocity = Vector2.up * 15f;
        
        yield return new WaitForSeconds(.5f);

        OpenBlackhole();
    }

    private void OpenBlackhole() {
        player.anim.SetBool("jump", false);
        player.anim.SetBool("idle", true);
        player.anim.speed = 0;
        rb.velocity = Vector2.down * 0.1f;
        SkillManager.instance.blackhole.UseSkill();
    }
}
