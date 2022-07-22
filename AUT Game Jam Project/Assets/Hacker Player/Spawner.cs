using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public int maxCount = 2;
    public float timeToAddEach = 3;
    public GameObject prefab;
    public Transform spawnPoint;
    public TextMeshProUGUI countLeft;

    private int currentCount = 2;
    private float timer = 0;

    private void Awake()
    {
        if(!spawnPoint)
        {
            spawnPoint = GameObject.Find("Virus Spawn Transform").transform;
        }
    }

    private void Start()
    {
        currentCount = maxCount;

        countLeft.SetText(currentCount.ToString());

        timer = timeToAddEach;
    }

    private void Update()
    {
        if (currentCount == maxCount)
        {
            return;
        }

        if (timer <= 0)
        {
            currentCount++;
            countLeft.SetText(currentCount.ToString());
            timer = timeToAddEach;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    public void Spawn()
    {
        if (currentCount <= 0)
        {
            return;
        }

        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        
        currentCount--;

        countLeft.SetText(currentCount.ToString());
    }
}
