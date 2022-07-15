using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool fullHealthOnAwake = true;
    public bool isDead = false;

    private void Awake()
    {
        if (fullHealthOnAwake)
            FullHealth();
    }

    public event System.Action OnDie;

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;
        ClampHealth();
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void ClampHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    private void Die()
    {
        OnDie?.Invoke();
        isDead = true;
    }

    public void FullHealth()
    {
        currentHealth = maxHealth;
    }
}
