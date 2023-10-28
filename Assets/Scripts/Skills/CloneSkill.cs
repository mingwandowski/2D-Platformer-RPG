using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Base info")]
    [SerializeField] private GameObject clonePrefab;
    public float fadeSpeed;

    public void CreateClone(Transform transform) {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetupClone(transform, newClone);
    }
}
