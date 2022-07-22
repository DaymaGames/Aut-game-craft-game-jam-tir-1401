using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEndScipt : MonoBehaviour
{
    public float videoDuration = 15.733f;
    public string nexSceneName = "Level-1";
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(videoDuration);
        LevelLoader.LoadScene(nexSceneName);
    }
}
