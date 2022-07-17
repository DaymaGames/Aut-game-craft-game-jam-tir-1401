using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    enum BossState {ShootAttack,CicleAttack,Rest,Standby }
    [SerializeField]BossState bossState;
    [SerializeField] GameObject bullet;

    private void Update()
    {
        switch (bossState)
        {
            case BossState.ShootAttack:
                //first shoot bullet for 10 sec
                //after that switch to circle attack
                break;
            case BossState.CicleAttack:
                //circle attack 
                //switch to rest 
                break;
            case BossState.Rest:
                //3 second rest
                //after 3 second switch to shoot 
                break;
                

        }
    }

    void ShootAttack()
    {

    }
    void CircleAttack()
    {

    }
    
    void SwitchToRest()
    {
        bossState = BossState.Rest;
    }
    void StateSwitcher(BossState givenValue,BossState goalValue)
    {
        givenValue = goalValue;
    }
}
