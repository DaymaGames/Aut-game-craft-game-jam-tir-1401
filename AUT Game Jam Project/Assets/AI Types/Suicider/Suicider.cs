using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicider : MonoBehaviour
{
    public float suicideTime = 3;
    public float attackRadius = 1.25f;
    public LayerMask attackMask;
    public int damage = 20;

    bool attacking = false;
    private void Awake()
    {
        GetComponent<AIController>().OnAttackAction += StartSuicide;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void StartSuicide()
    {
        if (attacking) return;
        attacking = true;
        GetComponent<AIController>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(DelaySuicide());
    }

    IEnumerator DelaySuicide()
    {
        yield return new WaitForSeconds(suicideTime);
        Attack();
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRadius, attackMask);

        foreach (var coll in colliders)
        {
            if (coll.transform == transform)
                break;

            if(coll.TryGetComponent(out Health health))
            {
                health.TakeDamage(damage, transform);
            }
        }
    }
}