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
    private bool NewButtonPressed, ExitButtonPressed, ContinueButtonPressed;


    // Components



    // Scripts
    public Fade Script_Fade;


    // Start is called before the first frame update
    void Start()
    {
        ApplySetting.RefreshDisplaySetting();
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        // Set Button Flag
        NewButtonPressed = false;
        ExitButtonPressed = false;
        ContinueButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Switch Scene, Controlled by alpha of the Fade
        if (Script_Fade.CG.alpha > 0.95)
        {
            
            //Debug.Log("Fade Finished" + "  " + Time.time);
            if (NewButtonPressed)
            {
                NewButtonPressed = false;
                ExitButtonPressed = false;
                ContinueButtonPressed = false;
                //Debug.Log("FlowControl NextScene" + "  " + Time.time);
                FlowControl.NextScene();
            }
            if (ContinueButtonPressed)
            {
                NewButtonPressed = false;
                ExitButtonPressed = false;
                ContinueButtonPressed = false;
                SceneController.GoToSceneByName("ExperimentScene");
            }
            if (ExitButtonPressed)
            {
                NewButtonPressed = false;
                ExitButtonPressed = false;
                ContinueButtonPressed = false;
                FlowControl.Exit();
            }
        }
    } 
    void FixedUpdate()
    {
        
    }

    // Start Button
    public void NewButton()
    {
        //Debug.Log("Start Button Pressed" + Time.time);
        NewButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Exit Button
    public void ExitButton()
    {
        ExitButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }

    public void ContinueButton()
    {
        ContinueButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }

}
