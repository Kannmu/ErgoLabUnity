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
        //Setting = JsonFile.ReadSetting(ExcelFile.ExperimentName);
        //Progress = JsonFile.ReadProgress(ExcelFile.ExperimentName);

        if (JsonFile.Setting["ExperimentType"] == 0)
        {
            WithinSubject.SetActive(true);
            BetweenSubject.SetActive(false);
            // Set Within Subject Experiment UI Value
            TMP_SubjectIndex_Within.SetText("Subject: " + (JsonFile.Progress["CurrentSubjectIndex"] + 1).ToString());
            TMP_RoundIndex.SetText("Round: " + (JsonFile.Progress["CurrentRoundIndex"] + 1).ToString());
        }
        else
        {
            BetweenSubject.SetActive(true);
            WithinSubject.SetActive(false);
            // Set Between Subject Experiment UI Value
            TMP_SubjectIndex_Bewteen.SetText("Subject: " + (JsonFile.Progress["CurrentSubjectIndex"] + 1).ToString());
            TMP_GroupIndex.SetText("Group: " + (JsonFile.Progress["CurrentGroupIndex"] + 1).ToString());
        }


        // Init Bool
        FinishButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        // For Subject And Round Switch
        if((JsonFile.Setting["ExperimentType"] == 0))
        {
            if ((JsonFile.Progress["CurrentRoundIndex"] == JsonFile.Setting["RoundsPerSubject"] - 1))
            {
                // In The Last Round Of Each Subject
                TMP_FinishButton.SetText("Finish");
            }
        }
        else
        {
            if ((JsonFile.Progress["CurrentSubjectIndex"] == (JsonFile.Setting["SubjectNum"]) - 1))
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
                if (JsonFile.Setting["ExperimentType"] == 0)
                {
                    JsonFile.WriteProgress(ExcelFile.ExperimentName, JsonFile.Progress["TotalPresentedRounds"] + 1, JsonFile.Progress["CurrentSubjectIndex"], JsonFile.Progress["CurrentRoundIndex"] + 1, 0);
                    if (JsonFile.Progress["CurrentRoundIndex"] == JsonFile.Setting["RoundsPerSubject"])
                    {
                        // Update Progress of Subject and Reset CurrentRoundIndex
                         JsonFile.WriteProgress(ExcelFile.ExperimentName, JsonFile.Progress["TotalPresentedRounds"], JsonFile.Progress["CurrentSubjectIndex"] + 1, 0, 0);
                        if (JsonFile.Progress["CurrentSubjectIndex"] == JsonFile.Setting["SubjectNum"])
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
                    JsonFile.WriteProgress(ExcelFile.ExperimentName, JsonFile.Progress["TotalPresentedRounds"] + 1, JsonFile.Progress["CurrentSubjectIndex"] + 1, 0, JsonFile.Progress["CurrentGroupIndex"]);
                    if (JsonFile.Progress["CurrentSubjectIndex"] == (JsonFile.Setting["SubjectNum"]))
                    {
                        JsonFile.WriteProgress(ExcelFile.ExperimentName, JsonFile.Progress["TotalPresentedRounds"], JsonFile.Progress["CurrentSubjectIndex"], 0, JsonFile.Progress["CurrentGroupIndex"] + 1);
                        if (JsonFile.Progress["CurrentGroupIndex"] == JsonFile.Setting["GroupNum"])
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
        if (JsonFile.Setting["ExperimentType"] == 0)
        {
            S_ProgressBar.value = (float)((float)JsonFile.Progress["CurrentRoundIndex"] / (float)JsonFile.Setting["RoundsPerSubject"]) + 0.1f;
        }
        else
        {
            S_ProgressBar.value = (float)((float)JsonFile.Progress["CurrentSubjectIndex"] / (float)(JsonFile.Setting["SubjectNum"])) + 0.1f;
        }
    }
    public void FinishButton()
    {
        FinishButtonPressed = true;
        // Update Progress of Round
        
        Script_Fade.CG_Alpha_Target = 1f;
    }
}
