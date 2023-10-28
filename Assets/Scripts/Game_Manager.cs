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

    [Space] [Header("Game Settings")][Space] [Space]
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
                // Spawn boss
                //UI_Manager.instance.ShowBossHealth();
                //UI_Manager.instance.ChangeBossHealth(25); // boss get damage
            }
            else
            {
                timer -= Time.deltaTime;
                UI_Manager.instance.timerText.text = Mathf.Round(timer).ToString();
            }
            
            // Diğer objeler ile bağlanacak
            //UI_Manager.instance.ChangeHealth(25); // get damage
            //
            //UI_Manager.instance.ChangeStamina(50); // dash
            //
            //// if boss is on scene
            //
            
            //
            //// enemy death
            //
            //UI_Manager.instance.ChangeScore(UnityEngine.Random.Range(50,150));
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
            
        gameEnded = true;
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