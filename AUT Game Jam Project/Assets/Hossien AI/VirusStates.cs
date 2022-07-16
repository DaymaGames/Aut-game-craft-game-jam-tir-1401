using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VirusStateReferences
{
    public VirusController controller;
    public AIParentClass ai;
    public Rigidbody2D rb;

    [Header("Moving To Target")]
    public float maxDistanceToTarget = 2;
    public float getBackRange = 1;

    [Header("Attacking")]
    public int damage = 10;
    public float attackRate = 0.5f;
}

public abstract class VirusState
{
    bool startCalled = false;
    public VirusStateReferences references;

    public VirusState Tick()
    {
        if (startCalled == false)
        {
            Start();
            startCalled = true;
        }

        VirusState toR = Update();
        toR.references = references;

        if (toR != this)
        {
            StateExit(toR);
        }

        return toR;
    }

    protected virtual void Start() { }

    protected virtual void StateExit(VirusState nextState) { }

    public virtual void OnDrawGizmos() { }

    protected abstract VirusState Update();

    protected void print(object message)
    {
        Debug.Log(message);
    }
}

public class MoveToTargetState : VirusState
{
    Transform target;
    AIParentClass ai;
    float getBackForce = 200;
    Rigidbody2D rb;

    protected override void Start()
    {
        target = references.controller.target;
        ai = references.ai;
        ai.faceVelocity = false;
        rb = references.rb;
    }

    bool facingRight = true;

    protected override VirusState Update()
    {
        if (!target)
        {
            target = references.controller.FindClosestEnemy();
        }
        if (!target)
        {
            return this;
        }

        Vector2 relative = (Vector2)target.position - references.rb.position;

        if (relative.sqrMagnitude <= references.maxDistanceToTarget * references.maxDistanceToTarget)
        {
            ai.IsStopped = true;

            if (relative.sqrMagnitude <= references.getBackRange * references.getBackRange)
            {
                GetBack(relative);
            }

            HandleAttacking();
        }
        else
        {
            ai.IsStopped = false;
            MoveToTarget();
        }

        HandleFlipping(relative);

        return this;
    }

    float time = 0;
    float nextAttackTime = 0;
    private void HandleAttacking()
    {
        time += Time.deltaTime;
        if (nextAttackTime <= time)
        {
            nextAttackTime = time + 1 / references.attackRate;
            Attack();
        }
    }

    private void Attack()
    {
        target.GetComponent<Health>().TakeDamage(references.damage);
    }

    private void GetBack(Vector2 relative)
    {
        rb.AddForce(-relative * getBackForce * Time.deltaTime);
    }

    private void HandleFlipping(Vector2 relative)
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

    void Flip()
    {
        facingRight = !facingRight;
        references.rb.transform.Rotate(Vector3.up * 180f);
    }

    void MoveToTarget()
    {
        ai.SetDestination(target.position);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rb.position, references.maxDistanceToTarget);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rb.position, references.getBackRange);
    }
}