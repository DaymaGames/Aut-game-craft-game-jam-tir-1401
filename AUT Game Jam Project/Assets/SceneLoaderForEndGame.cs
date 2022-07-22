using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderForEndGame : MonoBehaviour
{
    public void LoadMenu()
    {
        LevelLoader.LoadScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0).name);
    }
}
