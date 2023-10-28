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
    [SerializeField] private SpawnManager spawnManager;

    [Space] [Header("Game Objects")] [Space] [Space]
    public Camera cam;

    [Space] [Header("Game Settings")] [Space] [Space]
    public float timer;
    public float survivalTime;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnded)
            isPaused = !isPaused;
        
        PauseCheck();

        if (!isPaused && !gameEnded)
        {
            survivalTime += Time.deltaTime;
            
            if (timer <= 0)
            {
                UI_Manager.instance.ShowBossHealth();
            }
            else
            {
                timer -= Time.deltaTime;
                UI_Manager.instance.timerText.text = Mathf.Round(timer).ToString();
            }
        }
    }

    public void EndGame(bool win=false)
    {
        if (win) // Win
        {
            UI_Manager.instance.gameplayScreen.SetActive(false);
            UI_Manager.instance.pauseScreen.SetActive(false);
            UI_Manager.instance.deathScreen.SetActive(false);
            
            UI_Manager.instance.winScreen.SetActive(true);
        }
        else // Lose
        {
            UI_Manager.instance.gameplayScreen.SetActive(false);
            UI_Manager.instance.pauseScreen.SetActive(false);
            UI_Manager.instance.winScreen.SetActive(false);
            
            UI_Manager.instance.deathScreen.SetActive(true);   
        }
        
        cam.transform.position = new Vector3(0, 0, 50f);
            
        gameEnded = true;
    }

    private void PauseCheck()
    {
        if (!gameEnded)
        {
            if (isPaused)
            {
                cam.transform.position = new Vector3(0, 0, 50f);
                    
                UI_Manager.instance.gameplayScreen.SetActive(false);
                UI_Manager.instance.pauseScreen.SetActive(true);

                if (Time.timeScale > 0)
                    Time.timeScale = 0;
            }
            else
            {
                cam.transform.position = new Vector3(0, 0, -10f);
                
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
