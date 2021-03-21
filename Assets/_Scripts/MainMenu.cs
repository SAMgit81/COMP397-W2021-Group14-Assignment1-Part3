using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnMenuButtonPressed()
    {
        LoadMenu();
    }
    public void QuitGame ()
    {
        Application.Quit();
    }
}
