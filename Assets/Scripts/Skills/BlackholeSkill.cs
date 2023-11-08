using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkill : Skill
{
    [Header("Blackhole info")]
    [SerializeField] private GameObject blackHolePrefab;
    public bool blackholeOpened = false;
    private GameObject blackhole;

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();

        if (blackholeOpened && blackhole == null) {
            player.stateMachine.ChangeState(player.AirState);
            blackholeOpened = false;
        }
    }

    public override void UseSkill() {
        base.UseSkill();
        CreateBlackhole();
    }

    private void CreateBlackhole() {
        blackholeOpened = true;
        blackhole = Instantiate(blackHolePrefab, player.transform.position + new Vector3(0, -2, 0), Quaternion.identity);
    }
}
