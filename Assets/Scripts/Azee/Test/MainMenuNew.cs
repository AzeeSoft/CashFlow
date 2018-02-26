using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour
{
    public GameObject creditsScreen;

    public String gameSceneName = "test";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (creditsScreen.activeSelf)
            {
                hideCreditsScreen();
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void showCreditsScreen()
    {
        creditsScreen.SetActive(true);
    }

    public void hideCreditsScreen()
    {
        creditsScreen.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
