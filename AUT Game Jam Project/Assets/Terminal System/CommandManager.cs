using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public string level1Name = "Level-1";
    public void Exit() 
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LevelLoader.LoadScene(level1Name);
    }

    public void Credit()
    {
    }

    public void Retry()
    {
        LevelLoader.ReloadScene();
    }

    public void LoadMenu()
    {
        LevelLoader.LoadScene(menuSceneName);
    }

    public void Resume()
    {
        FindObjectOfType<PauseMenu>().Resume();
    }
}
