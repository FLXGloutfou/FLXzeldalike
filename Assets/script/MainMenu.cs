using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public string LevelToLoad;
    public GameObject settingWindow;
    public void startgame()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
    public void settingbutton()
    {
        settingWindow.SetActive(true);
    }

    public void CloseSettingsWindow()
    {
        settingWindow.SetActive(false);
    }

    public void leavegame()
    {
        Application.Quit();
    }
}
