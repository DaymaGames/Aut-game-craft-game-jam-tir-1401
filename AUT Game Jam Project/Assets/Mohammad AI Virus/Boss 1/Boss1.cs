using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    enum BossState {ShootAttack,CicleAttack,Rest }
    [SerializeField]BossState bossState;
}
