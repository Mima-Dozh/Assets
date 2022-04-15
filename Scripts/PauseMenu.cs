using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;
    public static bool IsSettings = false;
    public GameObject pauseMenuUI;
    public GameObject resourses;
    public GameObject settings;

    private void Start()
    {
        resourses = GameObject.Find("Resourse");
        settings = GameObject.Find("Setting").transform.GetChild(0).gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsSettings)
                DisableSettings();
            else
            {
                if (GameIsPause)
                    Resume();
                else
                    Pause();
            }
        }
           
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        resourses.SetActive(false);
        Time.timeScale = 0f;
        GameIsPause = true;  

    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        resourses.SetActive(true);
        Time.timeScale = 1f;
        GameIsPause = false;
    }

    public void ActiveSettings()
    {
        pauseMenuUI.SetActive(false);
        IsSettings = true;
        settings.SetActive(IsSettings);
    }

    public void DisableSettings()
    {
        pauseMenuUI.SetActive(true);
        IsSettings = false;
        settings.SetActive(IsSettings);
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
