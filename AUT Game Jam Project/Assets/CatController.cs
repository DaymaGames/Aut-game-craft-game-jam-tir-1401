using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SimpleMoveAI))]
public class CatController : MonoBehaviour
{
    public Transform playerFollow;
    public Vector2 followOffset = Vector2.zero;
    AIParentClass mover;
    private void Awake()
    {
        mover = GetComponent<AIParentClass>();
        mover.faceVelocity = false;
        if(!playerFollow)
            playerFollow = FindObjectOfType<HackerController>().transform;
    }
    private void Update()
    {
        if (!playerFollow)
            return;

        Vector3 target = (Vector2)playerFollow.position + followOffset;

        float threshold = 1;

        if ((target - transform.position).sqrMagnitude > threshold * threshold)
        {
            mover.IsStopped = false;
            mover.SetDestination(target);
        }
        else
        {
            mover.IsStopped = true;
        }
    }
    bool facingRight = true;
    private void LateUpdate()
    {
        if(transform.position.x > playerFollow.position.x)
        {
            if (facingRight)
            {
                Flip();
            }
        }
        else if(transform.position.x < playerFollow.position.x)
        {
            if (!facingRight)
            {
                Flip();
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180f);
    }
    private void OnDrawGizmosSelected()
    {
        if (playerFollow)
            Gizmos.DrawWireSphere((Vector2)playerFollow.position + followOffset, 0.3f);
    }
}
