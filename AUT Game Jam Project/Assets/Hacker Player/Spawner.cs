using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public int maxCount = 2;
    public float timeToAddEach = 3;
    public GameObject prefab;
    public Transform spawnPoint;
    public TextMeshProUGUI countLeft;
    public KeyCode key;
    public Image fillImage;
    private int currentCount = 2;
    private float timer = 0;

    private void Awake()
    {
        if(!spawnPoint)
        {
            spawnPoint = GameObject.Find("Virus Spawn Transform").transform;
        }

        fillImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        currentCount = maxCount;

        countLeft.SetText(currentCount.ToString());

        timer = timeToAddEach;
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            Spawn();
        }

        if (currentCount == maxCount)
        {
            return;
        }

        if (timer <= 0)
        {
            currentCount++;
            countLeft.SetText(currentCount.ToString());
            timer = timeToAddEach;
            fillImage.gameObject.SetActive(false);
        }
        else
        {
            timer -= Time.deltaTime;
            fillImage.gameObject.SetActive(true);
            fillImage.fillAmount = 1 - (timer / timeToAddEach);
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
