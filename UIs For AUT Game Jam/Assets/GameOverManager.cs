using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    public CanvasGroup elementsParent;
    public float fadeDuration = 0.3f;
    [Space]
    public UnityEvent OnGameOver;
    private void Awake()
    {
        GameOver = false;
        elementsParent.alpha = 0;
        elementsParent.gameObject.SetActive(false);
    }

    private bool gameOver = false;
    public bool GameOver 
    {
        get
        {
            return gameOver;
        }
        set
        {
            if (value == true && gameOver == false)
            {
                DoGameOver();
            }
            else if(value == false && gameOver == true)
            {
                CloseGameOver();
            }


            gameOver = value;
        }
    }
    public void ButtonRetry()
    {
        Debug.Log("Retry");
    }
    public void ButtonMenu()
    {
        Debug.Log("Opening Menu");
    }
    public void ButtonExit()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
    private void DoGameOver()
    {
        OnGameOver.Invoke();
        elementsParent.gameObject.SetActive(true);
        elementsParent.DOFade(1, fadeDuration);
    }
    private void CloseGameOver()
    {
        elementsParent.DOFade(0, fadeDuration).
            OnComplete(()=>elementsParent.gameObject.SetActive(false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            LoseGame();
    }

    void LoseGame()
    {
        GameOver = true;
    }
}
