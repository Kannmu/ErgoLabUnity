using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestController : MonoBehaviour
{

    // Variables
    public float RestTime = 30, TimerStart;
    public bool TimerEndFlag, NextButtonPressed;

    // Components
    public TextMeshProUGUI ProgressText;
    public Image ProgressCircle;
    public GameObject CG_NextButton;

    // Script
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        // Activate Fade
        Script_Fade.CG.gameObject.SetActive(true);

        // Init Bool
        TimerEndFlag = false;
        NextButtonPressed = false;

        CG_NextButton.SetActive(false);

        // Get Start Time
        TimerStart = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float Progress = (Time.time - TimerStart) / RestTime;
        if (Progress >= 1)
        {
            TimerEndFlag = true;
            CG_NextButton.SetActive(true);
        }
        else
        {
            UpdateUI(Progress);
        }
        if (Script_Fade.CG.alpha > 0.95)
        {
            if (NextButtonPressed)
            { 
                TimerEndFlag = false;
                NextButtonPressed = false;
                SceneController.GoToSceneByName("Experiment");
            }
        }
    }

    void UpdateUI(float Progress)
    {
        ProgressText.SetText((Time.time - TimerStart).ToString("0.00") + "/" + RestTime.ToString("0") + "s");
        ProgressCircle.fillAmount = Progress;
    }
    public void NextButton()
    {
        NextButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1;
    }
}
