using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;

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
        if(bossTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        rigidBody.AddForce(bulletSpeed * Time.deltaTime * (transform.position - bossTransform.position).normalized);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().TakeDamage(damage, transform);
            Destroy(gameObject);
        }
    }

}
