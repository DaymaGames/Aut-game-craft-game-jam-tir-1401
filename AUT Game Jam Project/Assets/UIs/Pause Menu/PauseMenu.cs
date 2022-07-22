using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup pauseParent;
    public float fadeDuration = 0.3f;

    public static bool IsPaused { get; private set; } = false;

    private void Awake()
    {
        pauseParent.gameObject.SetActive(false);
        pauseParent.alpha = 0;
        IsPaused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseParent.gameObject.SetActive(true);
        pauseParent.DOComplete();
        pauseParent.DOFade(1, fadeDuration);
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
        pauseParent.DOComplete();
        pauseParent.DOFade(0, fadeDuration).OnComplete(
            () =>
            {
                pauseParent.gameObject.SetActive(false);
            });
    }
}
