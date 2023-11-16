using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    private Enemy enemy;
    private RectTransform healthbar;
    private Slider slider;

    private void Start() {
        enemy = GetComponentInParent<Enemy>();
        healthbar = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();


        enemy.OnFlipped += FlipUI;
        enemy.stats.OnHealthChanged += UpdateHealthbar;

        UpdateHealthbar();
    }

    private void OnDisable() {
        enemy.OnFlipped -= FlipUI;
        enemy.stats.OnHealthChanged -= UpdateHealthbar;
    }

    public void FlipUI() => healthbar.Rotate(0, 180, 0);

    public void UpdateHealthbar() {
        slider.maxValue = enemy.stats.maxHealth.GetValue();
        slider.value = enemy.stats.currentHealth;
    }
}
