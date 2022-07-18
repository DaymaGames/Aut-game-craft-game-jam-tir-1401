using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerBoss : MonoBehaviour
{
    public List<string> enemyTags = new List<string>();

    [Header("Tracking")]
    public float followRange = 3;
    public float getBackRange = 2;
    public float getBackForce = 200;

    [Header("Tracking Far")]
    public float Far_FollowRange = 5;
    public float Far_GetBackRange = 3.5f;

    [Header("Tracking Close")]
    public float Close_FollowRange = 3;
    public float Close_GetBackRange = 2f;

    [Header("Different Sizes")]
    public Transform toScale;
    public CapsuleCollider2D capsuleColliderToScale;
    public float bigSize = 4;
    public float mediumSize = 3;
    public float smallSize = 0.75f;

    [Header("Speeds")]
    public float fastSpeed = 300;
    public float getBackFastSpeed = 250;
    [Space]
    public float mediumSpeed = 250;
    public float getBackMediumSpeed = 250;
    [Space]
    public float slowSpeed = 150;
    public float getBackSlowSpeed = 250;

    [Header("Attacking")]
    public int damage = 20;
    public float attackRate = 1;

    [Space]
    public bool previewMode = false;

    [SerializeField] Transform target;

    Abilities.AttackMode attackMode;
    Abilities.SizeMode sizeMode;
    Abilities.MoveMode moveMode;
    Abilities.SpeedMode speedMode;
    Rigidbody2D rb;
    private AIParentClass mover;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        FindClosestEnemy();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, followRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, getBackRange);
    }

    private void Update()
    {
        if (previewMode)
            return;

        if (!target)
        {
            FindClosestEnemy();
            return;
        }

        if(!mover)
        {
            return;
        }

        Vector2 relative = target.position - transform.position;
        if (relative.sqrMagnitude <= followRange * followRange)
        {
            mover.IsStopped = true;
            HandleAttacking();

            if (relative.sqrMagnitude <= getBackRange * getBackRange)
            {
                GetBack(relative);
            }
        }
        else
        {
            mover.IsStopped = false;
            mover.SetDestination(target.position);
        }
    }

    void GetBack(Vector2 relative)
    {
        Vector2 force = getBackForce * Time.deltaTime * -relative.normalized;
        rb.AddForce(force);
    }

    float time = 0;
    float currentTime = 0;
    void HandleAttacking()
    {
        time += Time.deltaTime;
        if (time >= currentTime)
        {
            currentTime = time + 1 / attackRate;
            Attack();
        }
    }

    void Attack()
    {
        target.GetComponent<Health>().TakeDamage(damage, transform);
    }

    public void SetAbilities(Abilities abilities)
    {
        attackMode = abilities.attackMode;
        sizeMode = abilities.sizeMode;
        moveMode = abilities.moveMode;
        speedMode = abilities.speedMode;

        switch (attackMode)
        {
            case Abilities.AttackMode.Far:

                followRange = Far_FollowRange;
                getBackRange = Far_GetBackRange;

                break;

            case Abilities.AttackMode.Close:

                followRange = Close_FollowRange;
                getBackRange = Close_GetBackRange;

                break;
        }

        switch (sizeMode)
        {
            case Abilities.SizeMode.Big:
                
                toScale.localScale = Vector3.one * bigSize;

                Vector2 bCollSize = capsuleColliderToScale.size;
                bCollSize.x = bigSize;
                bCollSize.y = 2 * bigSize;

                break;
            case Abilities.SizeMode.Medium:
                
                toScale.localScale = Vector3.one * mediumSize;

                Vector2 mCollSize = capsuleColliderToScale.size;
                mCollSize.x = mediumSize;
                mCollSize.y = 2 * mediumSize;

                break;
            case Abilities.SizeMode.Small:
                
                toScale.localScale = Vector3.one * smallSize;

                Vector2 sCollSize = capsuleColliderToScale.size;
                sCollSize.x = smallSize;
                sCollSize.y = 2 * smallSize;

                break;
        }

        switch (moveMode)
        {
            case Abilities.MoveMode.Air:

                if (!previewMode)
                    mover = gameObject.AddComponent<SimpleMoveAI>();

                break;
            case Abilities.MoveMode.Ground:

                if (!previewMode)
                    mover = gameObject.AddComponent<AI2D>();

                break;
        }

        switch (speedMode)
        {
            case Abilities.SpeedMode.Fast:

                if (!previewMode)
                {
                    mover.moveSpeed = fastSpeed;
                    getBackForce = getBackFastSpeed;
                }

                break;
            case Abilities.SpeedMode.Medium:

                if (!previewMode)
                {
                    mover.moveSpeed = mediumSize;
                    getBackForce = getBackMediumSpeed;
                }

                break;
            case Abilities.SpeedMode.Slow:

                if (!previewMode)
                {
                    mover.moveSpeed = slowSpeed;
                    getBackForce = getBackSlowSpeed;
                }

                break;
        }
    }

    public Transform FindClosestEnemy()
    {
        List<GameObject> hasTags = new List<GameObject>();

        foreach (var tag in enemyTags)
        {
            foreach (var g in GameObject.FindGameObjectsWithTag(tag))
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
}
