using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIStateReferences
{
    public AIController controller;
    public AIParentClass ai;
    public Rigidbody2D rb;
    public AnimationPlayer animPlayer;

    [Header("Moving To Target")]
    public float maxDistanceToTarget = 2;
    public float getBackRange = 1;
    public float getBackForce = 160;

    [Header("Attacking")]
    public int damage = 10;
    public float attackRate = 0.5f;
    public bool bypassAttacking = false;
}

public abstract class AIState
{
    bool startCalled = false;
    public AIStateReferences references;

    public AIState Tick()
    {
        if (startCalled == false)
        {
            Start();
            startCalled = true;
        }

        AIState toR = Update();
        toR.references = references;
        toR.OnAttack = OnAttack;
        if (toR != this)
        {
            StateExit(toR);
        }

        return toR;
    }

    protected virtual void Start() { }

    protected virtual void StateExit(AIState nextState) { }

    public virtual void OnDrawGizmos() { }

    protected abstract AIState Update();

    protected void print(object message)
    {
        Debug.Log(message);
    }

    public delegate void OnAttackDel(Transform target);
    public OnAttackDel OnAttack;
}

public class MoveToTargetState : AIState
{
    Transform target;
    AIParentClass ai;
    Rigidbody2D rb;

    protected override void Start()
    {
        target = references.controller.target;
        ai = references.ai;
        ai.faceVelocity = false;
        rb = references.rb;
    }

    bool facingRight = true;

    protected override AIState Update()
    {
        if(DialogueManager.ShowingDialogue == true)
        {
            ai.IsStopped = true;
            return this;
        }
        else
        {
            ai.IsStopped = false;
        }

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
            else
            {
                HandleAttacking();
            }
        }
        else
        {
            ai.IsStopped = false;
            MoveToTarget();

            float speedThreshold = 0.3f;
            if (rb.velocity.sqrMagnitude > speedThreshold * speedThreshold)
                references.animPlayer.PlayAnim(AnimationType.Run);
            else
                references.animPlayer.PlayAnim(AnimationType.Idle);
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
        if (references.bypassAttacking)
            return;
        OnAttack?.Invoke(target);

        references.animPlayer.PlayAnim(AnimationType.Attack);

        //target.GetComponent<Health>().TakeDamage(references.damage);
    }

    private void GetBack(Vector2 relative)
    {
        rb.AddForce(references.getBackForce * Time.deltaTime * -relative);
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
}