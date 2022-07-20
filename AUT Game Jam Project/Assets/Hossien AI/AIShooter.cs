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

        if (target)
            bullet.transform.Look2D(target, Vector3.right);
        
        bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right * bulletForce);
    }
}
public static class ExtenstionForTransform
{
    public static void Look2D(this Transform transform, Transform target, Vector3 forward)
    {
        Vector2 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.Rotate(Vector3.forward *
            Quaternion.FromToRotation(Vector3.right, forward).eulerAngles.z);
    }
    public static void Look2D(this Transform transform, Vector3 target, Vector3 forward)
    {
        Vector2 direction = target - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.Rotate(Vector3.forward *
            Quaternion.FromToRotation(Vector3.right, forward).eulerAngles.z);
    }
}