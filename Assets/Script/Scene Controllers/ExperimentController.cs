using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExperimentController : MonoBehaviour
{
    // Variables
    private Dictionary<string, int> Setting, Progress;
    private bool FinishButtonPressed;

    // Components
    public TextMeshProUGUI TMP_SubjectIndex, TMP_RoundIndex, TMP_FinishButton;
    public Slider S_ProgressBar;

    // Script
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        Script_Fade.CG.gameObject.SetActive(true);
        Setting = JsonFile.ReadSetting();
        Progress = JsonFile.ReadProgress();
        TMP_SubjectIndex.SetText("Subject: "+ (Progress["CurrentSubjectIndex"]+1).ToString());
        TMP_RoundIndex.SetText("Round: " + (Progress["CurrentRoundIndex"]+1).ToString());
        FinishButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For Subject And Round Switch
        if(Progress["CurrentRoundIndex"] == Setting["RoundsPerSubject"]-1)
        {
            TMP_FinishButton.SetText("Finish");
        }
        if (Script_Fade.CG.alpha > 0.95)
        {
            UpdateUI();
            if (FinishButtonPressed)
            {
                FinishButtonPressed = false;
                if (Progress["CurrentRoundIndex"] == Setting["RoundsPerSubject"])
                {
                    Progress = JsonFile.WriteProgress(Progress["TotalPresentedRounds"], Progress["CurrentSubjectIndex"] + 1, 0);
                    if (Progress["CurrentSubjectIndex"] == Setting["SubjectNum"])
                    {
                        SceneController.GoToSceneByName("IntroAndMenu");
                    }
                    else 
                    {
                        SceneController.GoToSceneByName("Notification");
                    }
                }
                else
                {
                    SceneController.GoToSceneByName("ExperimentScene");
                }
            }
        }
        
    }
    void UpdateUI()
    {
        Progress = JsonFile.ReadProgress();
        S_ProgressBar.value = (float)((float)Progress["CurrentRoundIndex"] / (float)Setting["RoundsPerSubject"]) + 0.1f;
    }
    public void FinishButton()
    {
        FinishButtonPressed = true;
        Progress = JsonFile.WriteProgress(Progress["TotalPresentedRounds"] + 1, Progress["CurrentSubjectIndex"], Progress["CurrentRoundIndex"] + 1);
        Script_Fade.CG_Alpha_Target = 1f;
    }
}
