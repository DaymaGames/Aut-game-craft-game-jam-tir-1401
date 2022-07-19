using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup elementsParent;
    public float duration = 0.3f;
    public bool IsPaused { get; private set; } = false;

    private void Awake()
    {
        IsPaused = true;
        elementsParent.alpha = 0;
        elementsParent.gameObject.SetActive(false);
    }

    public void Open()
    {
        elementsParent.gameObject.SetActive(true);
        elementsParent.DOComplete();
        elementsParent.DOFade(1, duration);
        IsPaused = true;
    }

    public void Close()
    {
        elementsParent.DOComplete();
        Tweener tweener = elementsParent.DOFade(0, duration);
        tweener.OnComplete(
            () => { 
                IsPaused = false;
                elementsParent.gameObject.SetActive(false);
            });
    }

    public void ButtonOpenMenu()
    {
        Debug.Log("Opening Menu");
    }

    public void ButtonExitGame()
    {
        Debug.Log("Quiting");
        Application.Quit();
    }
}
