using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    [PropertyRange(0,"maxHealth")]
    public int currentHealth = 100;
    public bool fullHealthOnAwake = true;
    [Space]
    public bool isVulnerable = true;
    public bool isDead = false;
    [Space]
    public float damageForce = 50f;
    [Space]
    public SpriteRenderer targetGraphics;
    public Color damagedColor = Color.red;
    public Color normalColor = Color.white;
    [Space]
    public UnityEvent OnDieEvent;
    public AnimationPlayer animPlayer;

    public AudioSource dieSound;
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

        if (targetGraphics != null)
        {
            targetGraphics.color = damagedColor;

            Invoke(nameof(SetColorToWhite), 0.25f);
        }

        currentHealth -= damage;
        ClampHealth();
        if (currentHealth == 0)
        {
            Die();
        }
        return true;
    }

    void SetColorToWhite()
    {
        targetGraphics.color = normalColor;
    }

    public void TakeDamage(int damage, Transform damager)
    {
        if (!isVulnerable)
            return;

        if (TakeDamage(damage) == false)
            return;

        Rigidbody2D r = GetComponent<Rigidbody2D>();

        bool hasRigidbody;

        if (r == null)
            hasRigidbody = false;
        else
            hasRigidbody = true;

        if (hasRigidbody)
        {
            Vector2 relative = transform.position - damager.position;
            r.AddForce(damageForce * relative.normalized);
        }
    }

    [ContextMenu("Take 100 Damage")]
    public void Take100Damage()
    {
        TakeDamage(100);
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

        if (TryGetComponent(out AIController controller))
        {
            controller.dontTick = true;
        }
        else if (TryGetComponent(out CharacterMovement movement))
        {
            movement.autoAnimation = false;
            movement.SetVelocity(Vector2.zero);
            GetComponent<HackerController>().Die();
        }
        else if (TryGetComponent(out Boss1 boss))
        {
            boss.isDead = true;
            boss.StopAllCoroutines();
            boss.animator.Play(boss.dieState);
        }
        else if (TryGetComponent(out HackerBoss hboss))
        {
            hboss.animator.Play(hboss.dieState);
            hboss.isDead = true;
        }

        if (animPlayer)
            animPlayer.PlayAnim(AnimationType.Die);

        if (dieSound)
            dieSound.Play();
    }

    [ContextMenu("Full Health")]
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
