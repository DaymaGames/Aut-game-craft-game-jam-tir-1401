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

    [Space]
    public bool autoAnimation = true;
    public AnimationPlayer animPlayer;

    [HideInInspector] public bool facingRight = true;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (autoAnimation == false || animPlayer == null) return;

        float threshold = 0.2f;
        if (rb.velocity.sqrMagnitude > threshold * threshold && velocity.sqrMagnitude > threshold * threshold)
        {
            animPlayer.PlayAnim(AnimationType.Run);
        }
        else
            animPlayer.PlayAnim(AnimationType.Idle);
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity;
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
