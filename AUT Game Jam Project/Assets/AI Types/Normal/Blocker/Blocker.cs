using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIShooter))]
public class Blocker : MonoBehaviour
{
    public float turnDuration = 1;
    public bool faceTarget = true;

    private AIShooter shooter;
    private AIParentClass mover;
    private float faceT = 0;
    private bool facingRight = true;

    private void Awake()
    {
        shooter = GetComponent<AIShooter>();
        shooter.autoAttack = false;
        mover = shooter.aIReferences.ai;
        mover.faceVelocity = false;
        facingRight = mover.facingRight;
    }

    Vector2 GetTargetPosition()
    {
        return shooter.target.position;
    }

    private IEnumerator Start()
    {
        while (true)
        {
            while (!shooter.target || faceTarget == false)
                yield return null;

            faceT = turnDuration;

            while (facingRight == true && GetTargetPosition().x < transform.position.x)
            {
                faceT -= Time.deltaTime;

                if (faceT <= 0)
                {
                    Flip();
                    break;
                }

                yield return null;
            }

            while (facingRight == false && GetTargetPosition().x > transform.position.x)
            {
                faceT -= Time.deltaTime;

                if (faceT <= 0)
                {
                    Flip();
                    break;
                }

                yield return null;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if(FacingPlayer() == true)
        {
            shooter.aIReferences.bypassAttacking = false;
        }
        else
        {
            shooter.aIReferences.bypassAttacking = true;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Flip2D();
    }

    bool FacingPlayer()
    {
        if(facingRight && GetTargetPosition().x > transform.position.x)
        {
            return true;
        }
        else if(!facingRight && GetTargetPosition().x < transform.position.x)
        {
            return true;
        }

        return false;
    }
}