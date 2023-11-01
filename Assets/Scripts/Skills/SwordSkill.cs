using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordSkill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private float swordGravity = 4;
    [SerializeField] private float throwForce = 15;
    private float throwHeight;
    private Vector2 finalDir;

    [Header("Aim dots")]
    [SerializeField] private int numOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject aimDotPrefab;
    [SerializeField] private Transform dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();
    }

    protected override void Update() {
        base.Update();
        if (Input.GetKey(KeyCode.E)) {
            throwHeight = player.AimSwordState.throwHeight;
            for (int i = 0; i < dots.Length; i++) {
                dots[i].transform.position = GetDotPosition(i * spaceBetweenDots);
            }
        }
    }

    public void CreateSword() {
        finalDir = GetThrowVector();
        GameObject sword = Instantiate(swordPrefab, player.transform.position, player.transform.rotation);
        sword.GetComponent<SwordSkillController>().SetupSword(finalDir, swordGravity, player);
        player.AssignNewSword(sword);
    }

    public Vector2 AimDirection() {
        Vector2 launchDir = new(player.facingDir * 10, 1f);
        launchDir.y += throwHeight * 2;
        return launchDir.normalized;
    }

    public Vector2 GetThrowVector() {
        return AimDirection()  * throwForce;
    }

    public void SetDotsActive(bool active) {
        foreach (GameObject dot in dots) {
            dot.SetActive(active);
        }
    }

    private void GenerateDots() {
        dots = new GameObject[numOfDots];
        for (int i = 0; i < numOfDots; i++) {
            dots[i] = Instantiate(aimDotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 GetDotPosition(float t) {
        return  (Vector2)player.transform.position + GetThrowVector() * t + .5f * (t * t) * (Physics2D.gravity * swordGravity);
    }
}
