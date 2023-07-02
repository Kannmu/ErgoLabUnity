// Intro And Menu Script for Unity Experiment Template
// Author: Kannmu
// Date: 2023.7.1
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroAndMenu : MonoBehaviour
{
    // Variables
    private bool StartButtonPressed, ExitButtonPressed;

    // Components
    


    // Scripts
    public Fade Script_Fade;

    private void Awake()
    {
        //Limit the FPS to 60
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        // Set Button Flag
        StartButtonPressed = false;
        ExitButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(Script_Fade.CG.alpha);
        // Switch Scene, Controlled by alpha of the Fade
        if (Script_Fade.CG.alpha > 0.88)
        {
            if (StartButtonPressed)
            {
                NextScene();
            }
            if(ExitButtonPressed)
            {
                Exit();
            }
        }
    } 
    void FixedUpdate()
    {

    }

    // Start Button
    public void StartButton()
    {
        StartButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Exit Button
    public void ExitButton()
    {
        ExitButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Enter next scene
    private void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("Notification");
    }
    // Exit the game
    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    
}
