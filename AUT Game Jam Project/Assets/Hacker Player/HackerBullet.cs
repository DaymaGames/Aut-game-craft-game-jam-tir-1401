using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerBullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float killTimer = 10;

    public string ignoreTag;

    private void Start()
    {
        StartCoroutine(start());
    }

    private IEnumerator start()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / killTimer;
            yield return null;
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Collider2D c = collider;

        if (c.CompareTag(ignoreTag))
            return;

        if(c.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage);
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
