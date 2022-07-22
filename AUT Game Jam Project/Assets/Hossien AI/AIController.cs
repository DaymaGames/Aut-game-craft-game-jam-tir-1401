using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIParentClass),typeof(Rigidbody2D))]
public class AIController : MonoBehaviour
{
    public Transform target;
    public List<string> enemyTags;
    public bool autoAttack = true;
    public bool spawnAnimFinished = true;
    
    [Space]

    public AIStateReferences aIReferences;

    private AIState state;

    public System.Action OnAttackAction;

    public bool dontTick = false;
    private void Awake()
    {
        state = new MoveToTargetState
        {
            references = aIReferences
        };

        state.OnAttack += OnAttack;
    }

    public Transform FindClosestEnemy()
    {
        if (enemyTags.Count == 0)
            return null;

        List<GameObject> hasTags = new List<GameObject>();

        foreach (var tag in enemyTags)
        {
            foreach(var g in GameObject.FindGameObjectsWithTag(tag))
            {
                hasTags.Add(g);
            }
        }

        GameObject[] objects = hasTags.ToArray();

        if (objects.Length == 0)
            return null;

        target = objects[objects.Length - 1].transform;

        foreach (var obj in objects)
        {
            float olddistance = (target.position - transform.position).sqrMagnitude;
            float newDistance = (obj.transform.position - transform.position).sqrMagnitude;
            if (newDistance < olddistance)
            {
                target = obj.transform;
            }
        }
        return target;
    }

    private void Update()
    {
        if (dontTick || spawnAnimFinished == false)
            return;

        state = state.Tick();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aIReferences.maxDistanceToTarget);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, aIReferences.getBackRange);
    }

    public virtual void OnAttack(Transform target)
    {
        OnAttackAction?.Invoke();
        if (autoAttack)
        {
            if (target.root.TryGetComponent(out Health health))
            {
                health.TakeDamage(aIReferences.damage, transform);
            }
        }
    }
}