using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] GameObject bullet;
    void Start()
    {
        //Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        //transform.Rotate(0,0 ,+45 );
        //Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        //transform.Rotate(0, 0, +45);
        //Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        //transform.Rotate(0, 0, +45);
        for(int i = 0; i < 7; i++)
        {
            transform.Rotate(0, 0, +45);
            Instantiate(bullet, bulletSpawnPos.position, bulletSpawnPos.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
