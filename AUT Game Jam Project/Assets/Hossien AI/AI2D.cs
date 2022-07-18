using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D),typeof(Seeker))]
public class AI2D : AIParentClass
{
    public float nextWaypointDistance = 3f;
    public bool reachedEndOfPath = false;

    [Header("Update Path")]
    public float updateInterval = 0.3f;


    private Vector2? destination = null;

    Seeker seeker;
    Path path;
    int currentWaypoint = 0;
    Rigidbody2D rb;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(UpdatePath), 0, updateInterval);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone() == false || destination.HasValue == false)
            return;

        seeker.StartPath(rb.position, destination.Value, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (path.error)
            return;

        this.path = path;
        currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        if (path == null || IsStopped == true)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 relative = (Vector2)path.vectorPath[currentWaypoint] - rb.position;
        Vector2 force = moveSpeed * Time.fixedDeltaTime * relative.normalized;

        rb.AddForce(force);

        if (relative.sqrMagnitude < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (faceVelocity)
        {
            Vector2 rel = destination.Value - rb.position;
            if (rel.x > 0)
            {
                if (facingRight == false)
                {
                    Flip();
                }
            }
            else if (rel.x < 0)
            {
                if(facingRight == true)
                {
                    Flip();
                }
            }
        }
    }

    public override void SetDestination(Vector2 target)
    {
        destination = target;
    }
}