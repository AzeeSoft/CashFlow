using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour
{
    public String gameSceneName = "test";

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
