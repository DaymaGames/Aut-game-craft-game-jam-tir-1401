using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HackerController))]
public class HackerInventory : MonoBehaviour
{
    [Header("Gun Numbers")]
    [SerializeField] private int magSize = 10;
    public int bulletsInMag = 0;

    [Header("Back Pack Bullet Numbers")]
    [SerializeField] private int bulletSaveSize = 30;
    public int bulletsSaved = 0;

    [Header("UI")]
    [SerializeField] private TMPro.TextMeshProUGUI bulletsLeftText;
    [SerializeField] private TMPro.TextMeshProUGUI bulletsSavedText;

    private void Update()
    {
        bulletsLeftText.SetText("Bullets : " + bulletsInMag);
        bulletsSavedText.SetText("Saved Bullets : " + bulletsSaved);
    }

    private int Clamp(int num, int min, int max)
    {
        return Mathf.Clamp(num, min, max);
    }

    #region Shooting Logic
    public bool CanShoot()
    {
        ClampBulletsInMag();

        if(bulletsInMag == 0)
        {
            return false;
        }

        return true;
    }

    public void DecreaseBulletsInMag()
    {
        bulletsInMag--;
        ClampBulletsInMag();
    }

    public void FullBulletsInMag()
    {
        bulletsInMag = magSize;
        ClampBulletsInMag();
    }
    
    private void ClampBulletsInMag()
    {
        bulletsInMag = Clamp(bulletsInMag, 0, magSize);
    }
    #endregion

    #region Collecting Bullets Logic
    public bool CollectBullets(int amount)
    {
        ClampBulletsSaved();
        int targetBullets = bulletsSaved + amount;
        if(bulletsSaved == bulletSaveSize || targetBullets > bulletSaveSize)
        {
            return false;
        }

        bulletsSaved = targetBullets;
        return true;
    }

    private void ClampBulletsSaved()
    {
        bulletsSaved = Clamp(bulletsSaved, 0, bulletSaveSize);
    }
    #endregion

    #region Reloading
    public bool CanReload()
    {
        ClampBulletsSaved();
        if(bulletsSaved == 0)
        {
            return false;
        }
        return true;
    }

    public void DecreaseSavedBullets(int amount)
    {
        bulletsSaved -= amount;
        ClampBulletsSaved();
    }

    public void Reload()
    {
        int remainingBullets = magSize - bulletsInMag;
        DecreaseSavedBullets(remainingBullets);

        FullBulletsInMag();
    }
    #endregion
}