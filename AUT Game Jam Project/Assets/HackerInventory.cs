using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HackerController))]
public class HackerInventory : MonoBehaviour
{
    [Header("Bullet Numbers")]
    [SerializeField] private int maxSavedBullets = 30;
    [SerializeField] private int maxGunMagSize = 10;
    [SerializeField] private int currentBulletsInGun;

    [Header("Reload")]
    [SerializeField] private float reloadTime = 1;
    public bool reloading = false;

    public bool Reload()
    {
        if(currentBulletsInGun == maxGunMagSize)
        {
            Debug.Log("mag is <color=green>full!</color>");
            return false;
        }
        else if(reloading == true)
        {
            Debug.Log("<color=blue>currently reloading</color>");
            return false;
        }
        StartCoroutine(RelaodWithDelay(reloadTime));
        return true;
    }

    IEnumerator RelaodWithDelay(float delay)
    {
        float t = 0;
        reloading = true;

        while (t < 1)
        {
            t += Time.deltaTime / delay;
            yield return null;
        }

        FullMag();
        reloading = false;
    }

    private void FullMag()
    {
        currentBulletsInGun = maxGunMagSize;
    }

    public void DoShootLogic()
    {
        currentBulletsInGun--;
        currentBulletsInGun = Mathf.Clamp(currentBulletsInGun, 0, maxGunMagSize);
    }

    public bool CanShoot()
    {
        return (currentBulletsInGun > 0 && !reloading);
    }

    public bool CollectBullet(int amount)
    {

    }
}