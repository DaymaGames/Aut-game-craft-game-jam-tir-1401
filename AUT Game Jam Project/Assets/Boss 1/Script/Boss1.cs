using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    enum BossState { ShootAttack, CicleAttack, Rest, Standby }
    [SerializeField] BossState bossState;
    [SerializeField] Transform player;

    [Header("Required Scirpts")]
    [SerializeField] BulletSpawner bulletSpawnerScript;

    [Header("Delay Between Shooting")]
    [SerializeField] float shootDelay = 1.3f;

    [Header("Cicrle Attack Damage Range")]
    [SerializeField]float damageRange = 5;
    [Header("Delay Before Each Circle Attack")]
    [SerializeField]float delayBetweenCirlceAttacks = 2f;

    [Header("Boss Rest TimeDelay")]
    [SerializeField]float restLenght = 3;
    [Space]
    [SerializeField] AudioSource audioSource;//age gharar shod dad bezane

    [Header("Animation")]
    public Animator animator;
    public string shootState = "Shoot";
    public string attackState = "Shoot";
    public string dieState = "Die";

    [Header("Damage")]
    public int attackDamage = 30;

    [Header("Sounds")]
    public AudioClip shootSound;

    public bool isDead = false; 
    private void Start()
    {
        if (!player)
            player = FindObjectOfType<HackerController>().transform;
    }
    private void Update()
    {
        if (isDead || DialogueManager.ShowingDialogue == true
            || PauseMenu.IsPaused || GameManager.Instance.GameOver ||
            BossAbilityManager.DesigningBoss)
            return;

        switch (bossState)
        {
            case BossState.ShootAttack:
                //first shoot bullet for 10 sec
                //after that switch to circle attack
                StartCoroutine("Shoot");
                bossState = BossState.Standby;
                break;
            case BossState.CicleAttack:
                //two circle attack 
                //switch to rest 
                StartCoroutine("CircleAttack");
                bossState = BossState.Standby;
                break;
            case BossState.Rest:
                //3 second rest
                //after 3 second switch to shoot 
                StartCoroutine("Rest");
                bossState = BossState.Standby;
                break;


        }
    }

    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(shootDelay);
        ShootAttack();

        yield return new WaitForSeconds(shootDelay);
        ShootAttack();
        
        yield return new WaitForSeconds(shootDelay);
        ShootAttack();
        
        yield return new WaitForSeconds(shootDelay);
        ShootAttack();
        
        yield return new WaitForSeconds(shootDelay);
        ShootAttack();
        
        yield return new WaitForSeconds(1);
        bossState = BossState.CicleAttack;
    }
    IEnumerator CircleAttack()
    {
        yield return new WaitForSeconds(delayBetweenCirlceAttacks);
        
        //playAttackAnimation

        animator.Play(attackState);

        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            DamagePlayer(attackDamage);

        }
        yield return new WaitForSeconds(delayBetweenCirlceAttacks);
        
        //playAttackAnimation

        animator.Play(attackState);

        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            DamagePlayer(attackDamage);
        }

        yield return new WaitForSeconds(1);
        bossState = BossState.Rest;
    }
    IEnumerator Rest()
    {

        yield return new WaitForSeconds(restLenght);
        bossState = BossState.ShootAttack;
    }
    void ShootAttack()
    {
        bulletSpawnerScript.Shoot();
        //play shooting animation
        animator.Play(shootState);
        audioSource.PlayOneShot(shootSound);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, damageRange);
    }
    public void AnimationDieEnd()
    {
        Destroy(gameObject);
    }

    void DamagePlayer(int damage)
    {
        player.GetComponent<Health>().TakeDamage(damage, transform);
    }
}
