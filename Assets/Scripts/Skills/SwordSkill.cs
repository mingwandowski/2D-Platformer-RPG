using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkill : Skill
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform launchDir;
    [SerializeField] private float swordGravity;
}
