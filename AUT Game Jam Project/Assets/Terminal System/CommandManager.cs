using UnityEngine;

public class CommandManager : MonoBehaviour
{
    public string menuSceneName = "Menu";
    public string nextLevelName = "Cutscene";

    [Space]

    [TextArea]
    public string creditsText;
    public TMPro.TextMeshProUGUI creditsTextMesh;
    public void Exit() 
    {
        Application.Quit();
    }

    public void StartGame()
    {
        LevelLoader.LoadScene(nextLevelName);
    }

    public void ShowCredit()
    {
        creditsTextMesh.gameObject.SetActive(false);
        creditsTextMesh.SetText(creditsText);
        creditsTextMesh.gameObject.SetActive(true);
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
