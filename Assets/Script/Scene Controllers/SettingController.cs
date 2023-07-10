using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System.Threading;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Xml.Linq;

public class SettingController : MonoBehaviour
{
    // Variables
    private bool PrevButtonPressed,ConfrimButton;
    private Dictionary<string, int> Setting;

    // Components
    public Toggle FullScreen;
    public TextMeshProUGUI TMP_SubjectNumber;
    public TMP_Dropdown TMP_Resolution, TMP_FPS, TMP_ExpType;
    public TMP_InputField TMP_SubjectNum, TMP_RoundsPerSubject, TMP_IndependentVarNum, TMP_DependentVarNum, TMP_ExperimentName, TMP_GroupNum;
    public GameObject WithinSubject, BetweenSubject;

    // Scripts
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        // Read Settings
        Setting = JsonFile.ReadSetting(ExcelFile.ExperimentName);


        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);

        // Set Init Flag Value
        PrevButtonPressed = false;
        ConfrimButton = false;

        // Set Setting Scene Param Value
        TMP_Resolution.value = FindOptionIndex(TMP_Resolution, Setting["ResolutionX"].ToString() + "x" + Setting["ResolutionY"].ToString());
        TMP_FPS.value = FindOptionIndex(TMP_FPS, Setting["FPS"].ToString());
        TMP_ExpType.value = Setting["ExperimentType"];
        TMP_SubjectNum.text = (Setting["SubjectNum"].ToString());
        TMP_RoundsPerSubject.text = (Setting["RoundsPerSubject"].ToString());
        TMP_IndependentVarNum.text = (Setting["IndependentVarNum"].ToString());
        TMP_DependentVarNum.text = (Setting["DependentVarNum"].ToString());
        TMP_GroupNum.text = (Setting["GroupNum"].ToString());
        FullScreen.isOn = (Setting["IsFullScreen"] == 1);
        SwitchExperimentType();
    }

    // Update is called once per frame
    void Update()
    {
        // Fade to Dark
        if (Script_Fade.CG.alpha > 0.95)
        {
            // Previous Scene
            if (PrevButtonPressed)
            {
                PrevButtonPressed = false;
                SceneController.GoToSceneByIndex(0);
            }
            // Confrim And Start Experiment
            if (ConfrimButton)
            {
                ConfrimButton = false;
                SceneController.GoToSceneByName("Notification");
            }
        }
        
    }
    
    public void SwitchExperimentType()
    {
        // Switch Experinment Type Menu
        if (TMP_ExpType.value == 0)
        {
            WithinSubject.SetActive(true);
            BetweenSubject.SetActive(false);
            TMP_SubjectNumber.SetText("Subject Number:");
        }
        else
        {
            BetweenSubject.SetActive(true);
            WithinSubject.SetActive(false);
            TMP_SubjectNumber.SetText("(Per Group)Subject Number:");
        }
    }

    public void PrevButton()
    {
        PrevButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    
    public void ConfirmButton()
    {
        if(TMP_ExperimentName.text == "")
        {
            // If Empty Experiment Name
            Debug.LogError("ERROR: Please Input Experiment Name");
        }
        else
        {
            // Create Experiment Directory
            Directory.CreateDirectory(Application.dataPath + "/Data/Experiments Data/" + TMP_ExperimentName.text);
            try
            {
                // Write New Setting Param
                JsonFile.Setting ST = new()
                {
                    ResolutionX = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[..4]),
                    ResolutionY = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[5..]),
                    IsFullScreen = Convert.ToInt32(FullScreen.isOn),
                    FPS = Int32.Parse(TMP_FPS.options[TMP_FPS.value].text),
                    ExperimentType = TMP_ExpType.value,
                    SubjectNum = Int32.Parse(TMP_SubjectNum.text),
                    RoundsPerSubject = Int32.Parse(TMP_RoundsPerSubject.text),
                    IndependentVarNum = Int32.Parse(TMP_IndependentVarNum.text),
                    DependentVarNum = Int32.Parse(TMP_DependentVarNum.text),
                    GroupNum = Int32.Parse(TMP_GroupNum.text)
                };

                // Write Setting To Json File
                JsonFile.WriteSetting(TMP_ExperimentName.text, ST);

                // Reset Progress Data
                JsonFile.SetDefaultProgress(TMP_ExperimentName.text);

                // Create Excel File Beased On Experiment Type
                if (ST.ExperimentType == 0)
                {
                    // Within Subject Experiment
                    ExcelFile.CreateWithinExcelFile(TMP_ExperimentName.text);
                }
                else if(ST.ExperimentType == 1)
                {
                    // Between Subject Experiment
                    ExcelFile.CreateBetweenExcelFile(TMP_ExperimentName.text);
                }

                // Set Confrim Button Flag
                ConfrimButton = true;

                // Start Fade Effect
                Script_Fade.CG_Alpha_Target = 1f;
                Debug.Log("Data Updated");
            }
            catch
            {
                Debug.LogError("ERROR: Wrong Input Data Type. Number Must Be Positive Integer");
            }

            // Refresh Setting
            ApplySetting.RefreshDisplaySetting();
        }
        
    }

    // Find Option Index In Dropdown Component With Name
    private int FindOptionIndex(TMP_Dropdown dropdown, string optionName)
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            if (dropdown.options[i].text == optionName)
            {
                return i;
            }
        }
        // Return 0 if not found
        return 0;
    }
    
}
