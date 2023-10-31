using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;

    public void CreateSword(float throwForce) {
        GameObject sword = Instantiate(swordPrefab, player.transform.position + player.facingDir * new Vector3(1, 0, 0), player.transform.rotation);
        sword.GetComponent<SwordSkillController>().SetupSword(launchDir + new Vector2(throwForce, 0), swordGravity);
    }
}
