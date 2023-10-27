using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance { get; private set; }
    
    public GameObject gameplayScreen;
    public GameObject pauseScreen;
    public GameObject deathScreen;
    
    public Slider healthbarSlider;
    public Slider healthbarEaseSlider;
    
    public Slider staminaSlider;
    public Slider staminaEaseSlider;
    
    public Slider bossHealthbarSlider;
    public Slider bossHealthbarEaseSlider;
    
    private float healthEaseWaitTime;
    private float staminaEaseWaitTime;
    private float bossHealthEaseWaitTime;
    
    public TMP_Text scoreText;
    public TMP_Text endScoreText;
    private float currentScore;
    private float tempScore;

    private void Awake()
    {
        if (instance == null) instance = this;
        
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);
        gameplayScreen.SetActive(true);
        
        bossHealthbarSlider.transform.parent.gameObject.SetActive(false);
        
        scoreText.text = "0";
    }

    private void Update()
    {
        if (!Game_Manager.instance.isPaused && !Game_Manager.instance.gameEnded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeHealth(25);
                
                ShowBossHealth();
                ChangeBossHealth(25);
                
                ChangeStamina(50);
                ChangeScore(UnityEngine.Random.Range(50,150));
            }

            if (healthEaseWaitTime > 0)
                healthEaseWaitTime -= Time.deltaTime;
            
            if (staminaEaseWaitTime > 0)
                staminaEaseWaitTime -= Time.deltaTime;
            
            if (bossHealthEaseWaitTime > 0)
                bossHealthEaseWaitTime -= Time.deltaTime;

            if (healthbarEaseSlider.value > healthbarSlider.value && healthEaseWaitTime <= 0)
                healthbarEaseSlider.value -= 100 * Time.deltaTime;
            
            if (staminaEaseSlider.value > staminaSlider.value && staminaEaseWaitTime <= 0)
                staminaEaseSlider.value -= 100 * Time.deltaTime;
            
            if (bossHealthbarEaseSlider.value > bossHealthbarSlider.value && bossHealthEaseWaitTime <= 0)
                bossHealthbarEaseSlider.value -= 100 * Time.deltaTime;

            if (tempScore > 0)
            {
                tempScore -= 1;
                currentScore += 1;

                scoreText.text = currentScore.ToString();
            }
            else
                tempScore = 0;
        }
        else if (Game_Manager.instance.gameEnded)
        {
            endScoreText.text = "Your Score: " + currentScore;
        }
    }
    
    public void ChangeScore(float score)
    {
        tempScore = score;
    }

    public void ChangeHealth(float damage)
    {
        healthbarSlider.value -= damage;

        healthEaseWaitTime = .25f;
    }
    
    public void ChangeStamina(float stamina)
    {
        staminaSlider.value -= stamina;

        staminaEaseWaitTime = .25f;
    }
    public void ChangeBossHealth(float damage)
    {
        bossHealthbarSlider.value -= damage;

        bossHealthEaseWaitTime = .25f;
    }

    public void ShowBossHealth()
    {
        bossHealthbarSlider.transform.parent.gameObject.SetActive(true);
    }
}
