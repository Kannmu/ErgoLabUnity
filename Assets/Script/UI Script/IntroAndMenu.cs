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
    private bool StartButtonPressed, ExitButtonPressed, SettingButtonPressed;


    // Components



    // Scripts
    public Fade Script_Fade;


    // Start is called before the first frame update
    void Start()
    {
        ProgramSettingController.RefreshDisplaySetting();
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        // Set Button Flag
        StartButtonPressed = false;
        ExitButtonPressed = false;
        SettingButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch Scene, Controlled by alpha of the Fade
        if (Script_Fade.CG.alpha > 0.95)
        {
            //Debug.Log("Fade Finished" + "  " + Time.time);
            if (StartButtonPressed)
            {
                //Debug.Log("FlowControl NextScene" + "  " + Time.time);
                FlowControl.NextScene();
            }
            if (SettingButtonPressed)
            {
                SceneController.GoToSceneByName("SettingScene");
            }
            if (ExitButtonPressed)
            {
                FlowControl.Exit();
            }
        }
    } 
    void FixedUpdate()
    {
        
    }

    // Start Button
    public void StartButton()
    {
        //Debug.Log("Start Button Pressed" + Time.time);
        StartButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Exit Button
    public void ExitButton()
    {
        ExitButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }

    public void SettingButton()
    {
        SettingButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }

}
