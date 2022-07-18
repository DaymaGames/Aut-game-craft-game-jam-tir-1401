using System.Collections;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    enum BossState { ShootAttack, CicleAttack, Rest, Standby }
    [SerializeField] BossState bossState;
    [SerializeField] Transform player;

    [Header("Scirpts")]
    [SerializeField] BulletSpawner bulletSpawnerScript;

    [Header("Delay Between Shooting")]
    [SerializeField] float shootDelay = 1.3f;

    [Header("Cicrle Attack Damage Range")]
    [SerializeField] float damageRange = 5;

    float delayBetweenCirlceAttacks=1.5f;
    private void Start()
    {

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

                bossState = BossState.Standby;
                break;


        }
        SetFacing();
        
    }

    void ShootAttack()
    {
        bulletSpawnerScript.Shoot();
    }
    
    IEnumerator CircleAttack()
    {
        //playAttackAnimation
        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            print("CircleAttackOne");
          
        }
        yield return new WaitForSeconds(delayBetweenCirlceAttacks);
        //playAttackAnimation
        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
            print("CircleAttacktwo");
        }
        bossState = BossState.Rest;
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
}
