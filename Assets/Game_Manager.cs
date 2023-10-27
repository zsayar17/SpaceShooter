using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance { get; private set; }
    [HideInInspector] public bool isPaused = false;
    [HideInInspector] public bool gameEnded = false;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
            isPaused = !isPaused;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            UI_Manager.instance.gameplayScreen.SetActive(false);
            UI_Manager.instance.pauseScreen.SetActive(false);
            
            UI_Manager.instance.deathScreen.SetActive(true);
            
            gameEnded = true;
        }
        
        PauseCheck();
    }

    private void PauseCheck()
    {
        if (!gameEnded)
        {
            if (isPaused)
            {
                UI_Manager.instance.gameplayScreen.SetActive(false);
                UI_Manager.instance.pauseScreen.SetActive(true);

                if (Time.timeScale > 0)
                    Time.timeScale = 0;
            }
            else
            {
                UI_Manager.instance.gameplayScreen.SetActive(true);
                UI_Manager.instance.pauseScreen.SetActive(false);
                
                if (Time.timeScale == 0)
                    Time.timeScale = 1;
            }
        }
    }
    
    public void RestartScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
    
    public void ResumeScene()
    {
        isPaused = false;
    }
}
