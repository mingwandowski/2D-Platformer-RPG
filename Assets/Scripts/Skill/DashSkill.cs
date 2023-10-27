using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSkill : Skill
{
    protected override void Start() {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void UseSkill() {
        base.UseSkill();
        Debug.Log("Dash");
    }
}
