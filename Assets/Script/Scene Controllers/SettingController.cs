using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System.Threading;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    // Variables
    private bool PrevButtonPressed,ConfrimButton;
    private Dictionary<string, int> Setting;

    // Components
    public TMP_Dropdown TMP_Resolution, TMP_FPS, TMP_ExpType;
    public TMP_InputField TMP_SubjectNum, TMP_RoundsPerSubject, TMP_IndependentVarNum, TMP_DependentVarNum, TMP_ExperimentName;
    
    // Scripts
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        
        // Read Settings
        Setting = JsonFile.ReadSetting();
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        // Set Init Value
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
    }

    // Update is called once per frame
    void Update()
    {
        // Fade to Dark
        if (Script_Fade.CG.alpha > 0.95)
        {
            if (PrevButtonPressed)
            {
                PrevButtonPressed = false;
                SceneController.GoToSceneByIndex(0);
            }
            if (ConfrimButton)
            {
                ConfrimButton = false;
                SceneController.GoToSceneByName("Notification");
            }
        }
        
    }
    
    public void PrevButton()
    {
        PrevButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    
    public void ConfirmButton()
    {
        ExcelFile.CreateExcelFile(TMP_ExperimentName.text);
        try
        {
            // Write New Setting Param
            JsonFile.Setting ST = new()
            {
                ResolutionX = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[..4]),
                ResolutionY = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[5..]),
                IsFullScreen = 0,
                FPS = Int32.Parse(TMP_FPS.options[TMP_FPS.value].text),
                ExperimentType = TMP_ExpType.value,
                SubjectNum = Int32.Parse(TMP_SubjectNum.text),
                RoundsPerSubject = Int32.Parse(TMP_RoundsPerSubject.text),
                IndependentVarNum = Int32.Parse(TMP_IndependentVarNum.text),
                DependentVarNum = Int32.Parse(TMP_DependentVarNum.text)
            };
            JsonFile.WriteSetting(ST);
            ConfrimButton = true;
            Script_Fade.CG_Alpha_Target = 1f;
            Debug.Log("Setting Updated");
        }
        catch
        {
            Debug.LogError("ERROR: Wrong Input Data Type. Number Must Be Positive Integer");
        }
        ApplySetting.RefreshDisplaySetting();
        JsonFile.SetDefaultProgress();
    }

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
