using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CharacterDieTrigger : MonoBehaviour
{
    public string dieTag = "OutSide";
    public Health health;
    public float dieDelay = 0.1f;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    IEnumerator DelayDie(float delay)
    {
        yield return new WaitForSeconds(delay);

        Kill();
    }

    void Kill()
    {
        health.TakeDamage(9999999);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(dieTag))
        {
            StartCoroutine(DelayDie(dieDelay));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(dieTag))
        {
            StopAllCoroutines();
        }
    }
}