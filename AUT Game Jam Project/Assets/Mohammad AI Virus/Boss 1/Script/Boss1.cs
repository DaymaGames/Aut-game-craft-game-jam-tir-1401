using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    enum BossState {ShootAttack,CicleAttack,Rest,Standby }
    [SerializeField]BossState bossState;
    [SerializeField]Transform player;

    [Header("Scirpts")]
    [SerializeField]BulletSpawner bulletSpawnerScript;

    [Header("Delay Between Shooting")]
    [SerializeField]float shootDelay=1.5f;

    [Header("Cicrle Attack Damage Range")]
    [SerializeField] float damageRange = 3;

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
                break;
            case BossState.Rest:
                //3 second rest
                //after 3 second switch to shoot 
                break;
                

        }
        SetFacing();
    }

    void ShootAttack()
    {
        bulletSpawnerScript.Shoot();
    }
    void CircleAttack()
    {
        print((player.transform.position - transform.position).magnitude);
        if ((player.transform.position - transform.position).magnitude <= damageRange)
        {
            //Damage Player
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
    void SetFacing()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.localScale=new Vector3(1, 1, 1);
        }
        if(transform.position.x - player.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
