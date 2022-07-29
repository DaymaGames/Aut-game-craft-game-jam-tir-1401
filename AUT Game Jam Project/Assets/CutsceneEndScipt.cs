using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using DG.Tweening;

public class CutsceneEndScipt : MonoBehaviour
{
    public float videoDuration = 15.733f;
    public string nexSceneName = "Level-1";
    public VideoPlayer videoPlayer;

    bool pressed = false;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(videoDuration);
        LevelLoader.LoadScene(nexSceneName);
    }

    private void Update()
    {
        if (pressed) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            StopAllCoroutines();
            pressed = true;

            StartCoroutine(FadeVideoSound(LevelLoader.Instance.fadeDuration));
        }
    }

    IEnumerator FadeVideoSound(float duration)
    {
        LevelLoader.LoadScene(nexSceneName);

        while (videoPlayer.GetDirectAudioVolume(0) > 0)
        {
            videoPlayer.SetDirectAudioVolume(0,
                videoPlayer.
                GetDirectAudioVolume(0) - Time.deltaTime / duration);
            yield return null;
        }
    }
}
