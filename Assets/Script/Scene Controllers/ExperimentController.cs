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
    public TextMeshProUGUI TMP_SubjectIndex_Within, TMP_SubjectIndex_Bewteen, TMP_RoundIndex, TMP_FinishButton,TMP_GroupIndex;
    public Slider S_ProgressBar;
    public GameObject WithinSubject, BetweenSubject;

    // Script
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        // Activate Fade
        Script_Fade.CG.gameObject.SetActive(true);

        // Read Setting and Progress
        Setting = JsonFile.ReadSetting(ExcelFile.ExperimentName);
        Progress = JsonFile.ReadProgress(ExcelFile.ExperimentName);

        if (Setting["ExperimentType"] == 0)
        {
            WithinSubject.SetActive(true);
            BetweenSubject.SetActive(false);
            // Set Within Subject Experiment UI Value
            TMP_SubjectIndex_Within.SetText("Subject: " + (Progress["CurrentSubjectIndex"] + 1).ToString());
            TMP_RoundIndex.SetText("Round: " + (Progress["CurrentRoundIndex"] + 1).ToString());
        }
        else
        {
            BetweenSubject.SetActive(true);
            WithinSubject.SetActive(false);
            // Set Between Subject Experiment UI Value
            TMP_SubjectIndex_Bewteen.SetText("Subject: " + (Progress["CurrentSubjectIndex"] + 1).ToString());
            TMP_GroupIndex.SetText("Group: " + (Progress["CurrentGroupIndex"] + 1).ToString());
        }


        // Init Bool
        FinishButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For Subject And Round Switch
        if((Setting["ExperimentType"] == 0))
        {
            if ((Progress["CurrentRoundIndex"] == Setting["RoundsPerSubject"] - 1))
            {
                // In The Last Round Of Each Subject
                TMP_FinishButton.SetText("Finish");
            }
        }
        else
        {
            if ((Progress["CurrentSubjectIndex"] == (Setting["SubjectNum"]) - 1))
            {
                // In The Last Subject Of Each Group
                TMP_FinishButton.SetText("Next Group");
            }
        }
        
        if (Script_Fade.CG.alpha > 0.95)
        {
            UpdateProgress();
            if (FinishButtonPressed)
            {
                FinishButtonPressed = false;
                if (Setting["ExperimentType"] == 0)
                {
                    Progress = JsonFile.WriteProgress(ExcelFile.ExperimentName, Progress["TotalPresentedRounds"] + 1, Progress["CurrentSubjectIndex"], Progress["CurrentRoundIndex"] + 1, 0);
                    if (Progress["CurrentRoundIndex"] == Setting["RoundsPerSubject"])
                    {
                        // Update Progress of Subject and Reset CurrentRoundIndex
                        Progress = JsonFile.WriteProgress(ExcelFile.ExperimentName, Progress["TotalPresentedRounds"], Progress["CurrentSubjectIndex"] + 1, 0, 0);
                        if (Progress["CurrentSubjectIndex"] == Setting["SubjectNum"])
                        {
                            // After All Expriments
                            SceneController.GoToSceneByName("Intro");
                        }
                        else
                        {
                            // After Each Subject
                            SceneController.GoToSceneByName("Notification");
                        }
                    }
                    else
                    {
                        // Switch Round
                        SceneController.GoToSceneByName("Experiment");
                    }
                }
                else
                {
                    Progress = JsonFile.WriteProgress(ExcelFile.ExperimentName, Progress["TotalPresentedRounds"] + 1, Progress["CurrentSubjectIndex"]+1, 0, Progress["CurrentGroupIndex"]);
                    if (Progress["CurrentSubjectIndex"] == (Setting["SubjectNum"]))
                    {
                        Progress = JsonFile.WriteProgress(ExcelFile.ExperimentName, Progress["TotalPresentedRounds"], Progress["CurrentSubjectIndex"], 0, Progress["CurrentGroupIndex"]+1);
                        if (Progress["CurrentGroupIndex"] == Setting["GroupNum"])
                        {
                            // After All Expriments
                            SceneController.GoToSceneByName("Intro");
                        }
                    }
                    // After Each Subject
                    SceneController.GoToSceneByName("Notification");
                }
            }
        }
    }
    void UpdateProgress()
    {
        Progress = JsonFile.ReadProgress(ExcelFile.ExperimentName);
        if (Setting["ExperimentType"] == 0)
        {
            S_ProgressBar.value = (float)((float)Progress["CurrentRoundIndex"] / (float)Setting["RoundsPerSubject"]) + 0.1f;
        }
        else
        {
            S_ProgressBar.value = (float)((float)Progress["CurrentSubjectIndex"] / (float)(Setting["SubjectNum"])) + 0.1f;
        }
    }
    public void FinishButton()
    {
        FinishButtonPressed = true;
        // Update Progress of Round
        
        Script_Fade.CG_Alpha_Target = 1f;
    }
}
