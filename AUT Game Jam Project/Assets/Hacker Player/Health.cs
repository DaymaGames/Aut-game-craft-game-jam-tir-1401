using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public bool fullHealthOnAwake = true;
    public bool isDead = false;
    [Space]
    public UnityEvent OnDieEvent;
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
        OnDieEvent.Invoke();
        isDead = true;
    }

    public void FullHealth()
    {
        currentHealth = maxHealth;
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
