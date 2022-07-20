using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelLoader : MonoBehaviour
{
    public CanvasGroup elementsParent;
    public float fadeDuration = 0.3f;

    public static LevelLoader Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
        elementsParent.gameObject.SetActive(false);
        elementsParent.alpha = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            LoadScene("Menu");
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while(operation.isDone == false)
        {
            yield return null;
        }
        elementsParent.DOFade(0, fadeDuration).
            OnComplete(() => elementsParent.gameObject.SetActive(false));
    }

    public static void LoadScene(string sceneName)
    {
        Instance.elementsParent.gameObject.SetActive(true);
        Instance.elementsParent.DOFade(1,Instance.fadeDuration).
            OnComplete(() => 
            Instance.StartCoroutine(Instance.LoadSceneAsync(sceneName)));
    }
}
