using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance { get; private set; }
    [HideInInspector] public bool isPaused = false;
    [HideInInspector] public bool gameEnded = false;
    [SerializeField] private SpawnManager spawnManager;

    [Space] [Header("Game Objects")] [Space] [Space]
    public Transform playerTransform;
    public Camera cam;
    public GameObject[] enemyShips;

    [Space] [Header("Game Settings")] [Space] [Space]
    public float timer;
    public float survivalTime;

    private float generateTimer;

    private void Awake()
    {
        if (instance == null) instance = this;
        
        generateTimer = Random.Range(1f, 10f);
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

                if (generateTimer <= 0f)
                {
                    GenerateEnemyShips(Random.Range(0, enemyShips.Length));
                }
                else
                {
                    generateTimer -= Time.deltaTime;
                }
            }
        }
    }

    private void GenerateEnemyShips(int rnd)
    {
        Vector3 pos = new Vector3(Random.Range(0, 30), Random.Range(-20, 20), 0f);
        GameObject enemy = Instantiate(enemyShips[rnd], pos, Quaternion.identity);
        if (enemy.GetComponent<BasicShip>())
            enemy.GetComponent<BasicShip>().playertransform = playerTransform; 
        else if (enemy.GetComponent<KamikazeShip>())
            enemy.GetComponent<KamikazeShip>().playertransform = playerTransform;

        generateTimer = Random.Range(2f, 5f);
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
