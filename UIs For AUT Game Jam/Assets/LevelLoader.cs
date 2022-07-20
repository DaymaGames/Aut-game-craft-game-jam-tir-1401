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
    public delegate void OnSceneLoadDel(Scene scene, LoadSceneMode mode);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            LoadLevel("Level-1");
        }
    }

    private void Awake()
    {
        elementsParent.alpha = 0;
        elementsParent.gameObject.SetActive(false);

        Instance = this;
    }
    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (operation.isDone == false)
        {
            yield return null;
        }
        elementsParent.DOFade(0, fadeDuration);
    }
    public static void LoadLevel(string sceneName)
    {
        Instance.elementsParent.gameObject.SetActive(true);
        Instance.elementsParent.DOFade(1, Instance.fadeDuration).
            OnComplete(
                () => Instance.StartCoroutine(Instance.LoadSceneAsync(sceneName))
            );
    }
}
