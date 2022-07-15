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
}