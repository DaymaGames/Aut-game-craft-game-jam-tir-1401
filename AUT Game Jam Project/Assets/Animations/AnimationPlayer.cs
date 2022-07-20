using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    public string idleAnim = "Idle";
    public string runAnim = "Run";
    public string attackAnim = "Attack";
    public string dieAnim = "Die";
    public string suicideAnim = "Suicide";

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAnim(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.Idle:
                PlayState(idleAnim);
                break;
            case AnimationType.Run:
                PlayState(runAnim);
                break;
            case AnimationType.Attack:
                PlayState(attackAnim);
                break;
            case AnimationType.Die:
                PlayState(dieAnim);
                break;
            case AnimationType.Suicide:
                PlayState(suicideAnim);
                break;
        }
    }
    void PlayState(string state)
    {
        int id = Animator.StringToHash(state);
        
        if (animator == null)
            return;

        if (animator.HasState(0, id) == false)
        {
            return;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(state)) 
        {
            return;
        }

        animator.Play(id);
    }
    public void DestroyObject()
    {
        Destroy(transform.root.gameObject);
    }
}
public enum AnimationType { Idle,Run,Attack,Die,Suicide }