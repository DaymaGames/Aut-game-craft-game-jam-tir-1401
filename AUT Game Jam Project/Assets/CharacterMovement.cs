using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Facing Direction")]
    [SerializeField] private Transform targetFlip;


    [HideInInspector] public bool facingRight = true;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }
    private void FixedUpdate()
    {
        if (!rb)
        {
            Debug.LogError("attach a 2d rigidbody component", gameObject);
            return;
        }
        rb.velocity = velocity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity.normalized * moveSpeed;
    }
    public void HandleFacingDirection()
    {
        Vector2 velocity = this.velocity;
        
        if(velocity.x > 0.1f)
        {
            if(facingRight == false)
            {
                Flip();
            }
        }
        else if(velocity.x < -0.1f)
        {
            if(facingRight == true)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        targetFlip.Rotate(Vector3.up * 180f);
        facingRight = !facingRight;
    }
}
