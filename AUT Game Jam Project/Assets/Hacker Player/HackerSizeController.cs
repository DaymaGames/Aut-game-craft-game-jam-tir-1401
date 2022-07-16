using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HackerSizeController : MonoBehaviour
{
    public float normalSize = 1;
    public float otherSize = 0.5f;
    [Space]
    public float timeToBeSmall = 3;
    public Image timerImage;

    float timer = 0;

    private void Awake()
    {
        timerImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;

            timerImage.fillAmount = timer / timeToBeSmall;

            if (timer <= 0)
            {
                EndOfTimer();
            }
            else
            {
                return;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ButtonDown();
        }
    }
 
    void ButtonDown()
    {
        transform.localScale = Vector3.one * otherSize;
        
        timer = timeToBeSmall;
        
        timerImage.gameObject.SetActive(true);
    }
    
    void EndOfTimer()
    {
        transform.localScale = Vector3.one * normalSize;
        timerImage.gameObject.SetActive(false);
    }

    void EffectSmallSize()
    {

    }

    void RemoveSmallEffect()
    {

    }
}