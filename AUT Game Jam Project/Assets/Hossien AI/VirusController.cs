using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIParentClass))]
public class VirusController : MonoBehaviour
{
    public Transform target;
    public List<string> enemyTags;
    
    [Space]
    public VirusStateReferences virusReferences;

    private VirusState state;

    private void Awake()
    {
        state = new MoveToTargetState
        {
            references = virusReferences
        };
    }

    public Transform FindClosestEnemy()
    {
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
        state = state.Tick();
    }

    private void OnDrawGizmos()
    {
        if (state == null)
            return;

        state.OnDrawGizmos();
    }

}