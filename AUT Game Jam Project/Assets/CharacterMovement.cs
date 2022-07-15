using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 2f;


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
    }
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity.normalized * moveSpeed;
    }
}
