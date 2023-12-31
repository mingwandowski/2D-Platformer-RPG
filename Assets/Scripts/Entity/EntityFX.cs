using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material hitMat;
    private Material originalMat;

    private void Start() {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }

    public IEnumerator FlashFX(float flashDuration) {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    }

    public void RedColorBlink() {
        sr.color = sr.color != Color.white ? Color.white : Color.red;
    }

    public void CancelRedBlink() {
        CancelInvoke();
        sr.color = Color.white;
    }
}
