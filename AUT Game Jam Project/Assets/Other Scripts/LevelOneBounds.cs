using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBounds : MonoBehaviour
{
    public HackerController player;
    public float delay = 0.1f;

    bool isDead = false;
    void Die()
    {
        if (isDead)
            return;

        isDead = true;
        player.Die();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<Health>().TakeDamage(999999);
    }

    IEnumerator DelayDie(float delay)
    {
        yield return new WaitForSeconds(delay);
        Die();
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("DieTrigger"))
        {
            StartCoroutine(DelayDie(delay));
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("DieTrigger"))
        {
            StopAllCoroutines();
        }
    }
}
