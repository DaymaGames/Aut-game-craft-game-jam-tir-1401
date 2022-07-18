using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float eachRingDelay = 0.3f;
    
    [SerializeField] float rotateEachThreeRing=25;


    public void Shoot()
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
        transform.Rotate(0, 0, rotateEachThreeRing);
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
