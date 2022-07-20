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
    [SerializeField] private string bulletsPishvand;
    [SerializeField] private string bulletsSavedPishvand;

    private void Awake()
    {
        if(bulletsSavedText == null)
        {
            bulletsSavedText = GameObject.FindGameObjectWithTag("T_Bullet").GetComponent<TMPro.TextMeshProUGUI>();
        }
        if(bulletsLeftText == null)
        {
            bulletsLeftText = GameObject.FindGameObjectWithTag("T_Saved").GetComponent<TMPro.TextMeshProUGUI>();
        }
    }

    private void FixedUpdate()
    {
        bulletsLeftText.SetText(bulletsPishvand + bulletsInMag);
        bulletsSavedText.SetText(bulletsSavedPishvand + bulletsSaved);
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
        if(bulletsSaved == 0 || bulletsInMag == magSize)
        {
            return false;
        }
        return true;
    }

    public void Reload()
    {
        int howMuchWeNeed = magSize - bulletsInMag;
        int howMuchWeCanAdd = Mathf.Clamp(bulletsSaved, 0, howMuchWeNeed);

        bulletsInMag += howMuchWeCanAdd;
        bulletsSaved -= howMuchWeCanAdd;

        ClampBulletsInMag();
        ClampBulletsSaved();
    }
    #endregion
}