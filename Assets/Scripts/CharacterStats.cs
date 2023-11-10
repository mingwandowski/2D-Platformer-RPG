using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;
    [SerializeField] private int currentHealth;

    private void Start() {
        currentHealth = maxHealth.GetValue();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {

    }
}
