using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneBounds : MonoBehaviour
{
    public float timeToLose = 5;
    float timer = 0;

    public RectTransform warningTransform;
    public TMPro.TextMeshProUGUI timeText;
    public HackerController player;
    private void Awake()
    {
        warningTransform.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (timer <= 0)
            return;

        timer -= Time.deltaTime;
        timeText.SetText(timer.ToString("0"));

        if (timer <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        player.Die();
        player.GetComponent<Health>().TakeDamage(999999);
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            warningTransform.gameObject.SetActive(true);
            timer = timeToLose;
        }
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            warningTransform.gameObject.SetActive(false);
            timer = 0;
        }
    }
}
