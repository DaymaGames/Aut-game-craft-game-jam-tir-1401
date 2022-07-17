using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed=500;
    Rigidbody2D rigidBody;
    [SerializeField]Transform bossTransform;
    [SerializeField]Transform bulletSpawnPos;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rigidBody.AddForce((transform.position - bossTransform.position).normalized * bulletSpeed*Time.deltaTime);
    }

    
}
