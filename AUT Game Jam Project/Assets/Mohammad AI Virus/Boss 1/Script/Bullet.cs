using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    Rigidbody2D rigidBody;
    Transform bossTransform;

    float destroyTime=2;
    float bulletSpeed = 500;
    private void Awake()
    {
        bossTransform = FindObjectOfType<Boss1>().transform;
        rigidBody = GetComponent<Rigidbody2D>();

        Destroy(gameObject,destroyTime);

    }
    
    private void FixedUpdate()
    {
        rigidBody.AddForce((transform.position - bossTransform.position).normalized * bulletSpeed*Time.deltaTime);
    }

  
}
