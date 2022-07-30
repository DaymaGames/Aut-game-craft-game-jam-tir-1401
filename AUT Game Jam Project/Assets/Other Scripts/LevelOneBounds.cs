using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBounds : MonoBehaviour
{
    public Health playerHealth;
    public float delay = 0.1f;

    private void Awake()
    {
        if(!playerHealth)
        {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }
    }

    void KillPlayer(Health target)
    {
        target.TakeDamage(999999);
    }

    IEnumerator DelayDie(float delay, Health target)
    {
        yield return new WaitForSeconds(delay);
        KillPlayer(target);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("DieTrigger"))
        {
            StartCoroutine(DelayDie(delay, playerHealth));
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
