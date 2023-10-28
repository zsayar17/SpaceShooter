using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main_Menu_Manager : MonoBehaviour
{
    [Space] [Header("Object References")] [Space] [Space]
    public Camera cam;
    public GameObject fadeScreen;
    private Animation fadeAnimation;
    private Image fadeImage;
    
    [Space] [Header("Variables")] [Space] [Space]
    private bool fading;

    private void Awake()
    {
        fadeAnimation = fadeScreen.GetComponent<Animation>();
        fadeImage = fadeScreen.GetComponent<Image>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && fadeImage.color.a <= 0f)
        {
            fadeAnimation.Play();
            fading = true;
        }

        if (fadeImage.color.a >= 1f)
            SceneManager.LoadScene("SampleScene");

        if (fading)
            cam.transform.Translate(Vector3.forward * (35 * Time.deltaTime));
    }
}
