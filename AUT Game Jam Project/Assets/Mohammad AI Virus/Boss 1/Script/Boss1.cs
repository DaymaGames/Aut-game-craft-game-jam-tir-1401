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
    private void Start()
    {
        if (!player)
            player = FindObjectOfType<HackerController>().transform;
    }
    private void Update()
    {

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
        SetFacing();

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
        print("CircleAttack1");
        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            print("Damaged");

        }
        yield return new WaitForSeconds(delayBetweenCirlceAttacks);
        //playAttackAnimation
        print("CircleAttack2");
        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            print("Damaged");
        }
        yield return new WaitForSeconds(1);
        bossState = BossState.Rest;
    }
    IEnumerator Rest()
    {
        print("BossIsResting");
        yield return new WaitForSeconds(restLenght);
        bossState = BossState.ShootAttack;
    }
    void ShootAttack()
    {
        bulletSpawnerScript.Shoot();
    }
    void SetFacing()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (transform.position.x - player.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, damageRange);
    }
   
}
