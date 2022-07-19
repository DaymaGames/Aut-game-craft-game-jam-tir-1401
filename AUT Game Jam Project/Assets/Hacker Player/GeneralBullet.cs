using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float killTimer = 10;

    public List<string> ignoreTags = new List<string>();

    private void Start()
    {
        StartCoroutine(start());
    }

    private IEnumerator start()
    {
        yield return new WaitForSeconds(killTimer);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Collider2D c = collider;

        foreach (var tag in ignoreTags)
        {
            if (c.CompareTag(tag))
                return;
        }

        if(c.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage, transform);
        }
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
