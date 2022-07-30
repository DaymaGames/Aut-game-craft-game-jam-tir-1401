using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBullet : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float killTimer = 10;

    public List<string> ignoreTags = new List<string>();

    [HideInInspector] public Transform shooter;

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

        if(IsBlocker(c.transform) == true && c.transform.root != shooter)
        {
            if (CanDamageBlocker(c.transform.root))
            {
                c.transform.root.GetComponent<Health>().TakeDamage(damage, transform);
                Explode();
            }
            else
            {
                HitBlockerDefense();
                Explode();
                return;
            }
        }

        if (ignoreTags.Contains(c.tag) || c.CompareTag("OutSide"))
        {
            return;
        }
        
        if(c.transform.root.TryGetComponent(out Health health))
        {
            health.TakeDamage(damage, transform);
        }

        Explode();
    }

    void Explode()
    {
        StopAllCoroutines();
        this.SelfDestroy();
    }

    void HitBlockerDefense()
    {
        //hit particle
    }

    bool CanDamageBlocker(Transform blocker)
    {
        Vector2 blockerLeftDirection = -blocker.right;
        Vector2 blockerToBulletDirecton = (transform.position - blocker.position).normalized;
        float dot = Vector2.Dot(blockerLeftDirection, blockerToBulletDirecton);

        if(dot > 0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool IsBlocker(Transform target)
    {
        return target.root.TryGetComponent(out Blocker _);
    }
}
