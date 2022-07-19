using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooter : AIController
{
    public float bulletForce = 10;
    public GeneralBullet bulletPrefab;
    public Transform bulletSpawnParent;
    public Transform bulletSpawnPoint;
    public bool rotateAndLookToTarget = true;
    private void Start()
    {
        FindClosestEnemy();
    }
    private void LateUpdate()
    {
        if (!target || !rotateAndLookToTarget)
            return;
        
        Look();
    }

    void Look()
    {
        Vector2 direction = target.position - bulletSpawnParent.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bulletSpawnParent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void OnAttack(Transform target)
    {
        SpawnBullet();
    }
    void SpawnBullet()
    {
        GeneralBullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        bullet.ignoreTags.Add(tag);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletForce);
    }
}
public static class ExtenstionForTransform
{
    public static void Look2D()
    {

    }
}