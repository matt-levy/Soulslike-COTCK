using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void ContinueGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NewGame()
    {
        // TODO
        // Check If save files exist
        // If so, delete them

        SceneManager.LoadScene("GameScene");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
