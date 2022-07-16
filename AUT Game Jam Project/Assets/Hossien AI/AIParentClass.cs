using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIParentClass : MonoBehaviour
{
    public abstract void SetDestination(Vector2 target);
    public bool faceVelocity = true;
    public bool IsStopped { get; set; } = false;
    public bool facingRight = true;
    protected void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up * 180f);
    }
}
