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
    public float damageForce = 50f;
    [Space]
    public UnityEvent OnDieEvent;
    private void Awake()
    {
        if (fullHealthOnAwake)
            FullHealth();
    }

    public event System.Action OnDie;

    public bool TakeDamage(int damage)
    {
        if (isDead)
            return false;

        currentHealth -= damage;
        ClampHealth();
        if (currentHealth == 0)
        {
            Die();
        }
        return true;
    }

    public void TakeDamage(int damage, Transform damager)
    {
        if (TakeDamage(damage) == false)
            return;

        if (TryGetComponent(out Rigidbody2D rb))
        {
            Vector2 relative = transform.position - damager.position;
            rb.AddForce(damageForce * relative.normalized);
        }
        else
        {
            Debug.LogWarning("No Rigidbody Detected");
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

    public void Print(string message)
    {
        Debug.Log(message);
    }
}
