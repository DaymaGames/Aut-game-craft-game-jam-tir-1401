using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Loot : MonoBehaviour
{
    public string playerTag = "Player";
    public int amountOfBullets = 5;
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            if (other.transform.root
                .GetComponent<HackerInventory>()
                .CollectBullets(amountOfBullets))
            {
                Destroy(gameObject);
            }
        }
    }
}
