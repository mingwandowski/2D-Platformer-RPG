using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;
    public int currentHealth;

    public Action OnHealthChanged;

    protected virtual void Start() {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;
        OnHealthChanged?.Invoke();

        if (currentHealth <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Debug.Log(gameObject.name +  " died.");
    }
}
