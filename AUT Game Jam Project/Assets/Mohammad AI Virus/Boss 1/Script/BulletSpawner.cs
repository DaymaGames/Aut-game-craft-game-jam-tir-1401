using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] GameObject bullet;
    [SerializeField]float eachRingDelay=0.3f;


    

   public void BossShoot()
    {
        StartCoroutine("MakeBulletRings");
    }
   IEnumerator MakeBulletRings()
    {
        yield return new WaitForSeconds(eachRingDelay);
        MakeABulletRing();
        yield return new WaitForSeconds(eachRingDelay);
        MakeABulletRing();
        yield return new WaitForSeconds(eachRingDelay);
        MakeABulletRing();
    }

   void MakeABulletRing()
    {
        for (int i = 0; i < 8; i++)
        {
            transform.Rotate(0, 0, +45);
            Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        }
    }

}
