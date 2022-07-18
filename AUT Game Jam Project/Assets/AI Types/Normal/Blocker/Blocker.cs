using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIController))]
public class Blocker : MonoBehaviour
{
    public bool attackOnlyIfIsFront = true;

    AIController controller;
    AIParentClass ai;
    Transform target;
    private void Awake()
    {
        controller = GetComponent<AIController>();
        ai = GetComponent<AIParentClass>();
    }
    private void Start()
    {
        target = controller.target;
    }
    private void Update()
    {
        if(attackOnlyIfIsFront == false)
        {
            controller.aIReferences.bypassAttacking = false;
        }

        if (!target)
        {
            target = controller.target;
            return;
        }
        if (ai.facingRight)
        {
            if (target.position.x > transform.position.x)
            {
                controller.aIReferences.bypassAttacking = true;
            }
            else
            {
                controller.aIReferences.bypassAttacking = false;
            }
        }
        else
        {
            if (target.position.x < transform.position.x)
            {
                controller.aIReferences.bypassAttacking = true;
            }
            else
            {
                controller.aIReferences.bypassAttacking = false;
            }
        }
    }
}
