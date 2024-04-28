using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class setting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public PlayerControl playerControl; 
    public GameObject settingWindow;
    private bool menuActivated;
    public string LevelToLoad;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetControllerMode(bool isController)
    {
        playerControl.useController = isController; 
    }

    public void CloseSettingsWindow()
    {
        settingWindow.SetActive(false);
    }

    public void leavegame()
    {
        Application.Quit();
    }

    public void menu()
    {
        SceneManager.LoadScene(LevelToLoad);
    }

}
