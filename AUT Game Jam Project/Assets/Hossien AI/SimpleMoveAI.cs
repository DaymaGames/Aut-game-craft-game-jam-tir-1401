using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleMoveAI : AIParentClass
{
    public float speed = 200;
    
    Vector2? destination = null;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (destination == null || IsStopped == true)
            return;

        Vector2 relative = destination.Value - rb.position;

        Vector2 force = speed * Time.deltaTime * relative.normalized;

        rb.AddForce(force);

        if (faceVelocity)
        {
            if (relative.x > 0)
            {
                if (!facingRight)
                {
                    Flip();
                }
            }
            else if (relative.x < 0)
            {
                if (facingRight)
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
