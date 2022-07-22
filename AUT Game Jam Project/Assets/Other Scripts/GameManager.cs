using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Game Over")]
    public CanvasGroup gameOverParent;
    [BoxGroup("Game Over")]
    public float gameOverFadeDurartion = 0.3f;

    public static GameManager Instance { get; private set; }
    public bool GameOver { get; private set; } = true;

    private void Awake()
    {
        Instance = this;
        gameOverParent.gameObject.SetActive(false);
        gameOverParent.alpha = 0;
    }
    public void OpenGameOver()
    {
        GameOver = true;
        gameOverParent.gameObject.SetActive(true);
        gameOverParent.DOFade(1, gameOverFadeDurartion);
    }
    public void CloseGameOver()
    {
        GameOver = false;
        gameOverParent.gameObject.SetActive(false);
        gameOverParent.alpha = 0;
    }
}