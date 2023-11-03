using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SwordType {
    Regular,
    Bouncy,
    Pierce,
    Spin
    // todo: setup timeout for sword
}

public class SwordSkill : Skill
{
    [Header("Skill info")]
    public SwordType swordType = SwordType.Regular;
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

    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Spin info")]
    [SerializeField] private float maxRange;
    [SerializeField] private float spinDuration;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.E)) {
            switch (swordType) {
                case SwordType.Bouncy: 
                    swordGravity = bounceGravity; break;
                case SwordType.Pierce:
                    swordGravity = pierceGravity; break;
                case SwordType.Spin:
                    swordGravity = 0f; break;
                default: break;
            };
            GenerateDots();
        }

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

        switch (swordType)
        {
            case SwordType.Regular:
                sword.GetComponent<SwordSkillController>().SetupRegularSword(finalDir, swordGravity, player); break;
            case SwordType.Bouncy:
                sword.GetComponent<SwordSkillController>().SetupBouncySword(finalDir, swordGravity, player, bounceAmount); break;
            case SwordType.Pierce:
                sword.GetComponent<SwordSkillController>().SetupPierceSword(finalDir, pierceGravity, player, pierceAmount); break;
            case SwordType.Spin:
                sword.GetComponent<SwordSkillController>().SetupSpinSword(finalDir, 0f, player, maxRange, spinDuration); break;
            default:
                sword.GetComponent<SwordSkillController>().SetupRegularSword(finalDir, swordGravity, player); break;
        }
        player.AssignNewSword(sword);
    }

    public Vector2 AimDirection() {
        if (swordType == SwordType.Spin) {
            return new Vector2(player.facingDir, 0f);
        }
        Vector2 launchDir = new(player.facingDir * 20, 0f);
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
