using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    private void Start() {
        playerStats.OnHealthChanged += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void UpdateHealthBar() {
        slider.maxValue = playerStats.maxHealth.GetValue();
        slider.value = playerStats.currentHealth;
    }
}
