using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Stat damage;
    public Stat maxHealth;
    [SerializeField] private int currentHealth;

    protected virtual void Start() {
        currentHealth = maxHealth.GetValue();
    }

    public virtual void TakeDamage(int damage) {
        currentHealth -= damage;

        if (currentHealth <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        Debug.Log(gameObject.name +  " died.");
    }
}
