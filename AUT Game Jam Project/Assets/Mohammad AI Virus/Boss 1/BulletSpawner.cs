using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPos;
    [SerializeField] Bullet bullet;
    void Start()
    {
        bulletSpawnPos.rotation = new Quaternion(1, 1, 1,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
