using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int count = 2;
    public GameObject prefab;
    public Transform spawnPoint;
    public TextMeshProUGUI countLeft;

    private void Start()
    {
        countLeft.SetText(count.ToString());
    }

    public void Spawn()
    {
        if (count <= 0)
            return;

        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        
        count--;

        countLeft.SetText(count.ToString());
    }
}
