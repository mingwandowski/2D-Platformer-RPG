using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Base info")]
    [SerializeField] private GameObject clonePrefab;
    public float fadeSpeed;

    public void CreateClone(Vector3 position, Quaternion rotation, int facingDir) {
        GameObject newClone = Instantiate(clonePrefab);
        newClone.GetComponent<CloneSkillController>().SetupClone(position, rotation, newClone, facingDir);
    }
}
