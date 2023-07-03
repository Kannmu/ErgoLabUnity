using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using System.Threading;
using UnityEngine.UI;
using static JsonFile;

public class SettingController : MonoBehaviour
{
    // Variables
    private bool PrevButtonPressed;
    private Dictionary<string, int> ProgramSetting, ExperimentSetting;

    // Components
    public GameObject SettingButtons, ProgramSettingMenu, ExperimentSettingMenu;
    public TMP_Dropdown TMP_Resolution, TMP_FPS;
    public TMP_InputField TMP_SubjectNum, TMP_RoundPerSubject;
    
    // Scripts
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
   
        // Read Settings
        ProgramSetting = JsonFile.ReadProgramSetting();
        ExperimentSetting = JsonFile.ReadExperimentSetting();
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        ProgramSettingMenu.SetActive(false); 
        ExperimentSettingMenu.SetActive(false);
        // Set Init Value
        PrevButtonPressed = false;
        // Set Setting Scene Param Value
        TMP_Resolution.value = FindOptionIndex(TMP_Resolution, ProgramSetting["ResolutionX"].ToString() + "x" + ProgramSetting["ResolutionY"].ToString());
        TMP_FPS.value = FindOptionIndex(TMP_FPS, ProgramSetting["FPS"].ToString());
        TMP_SubjectNum.text = ExperimentSetting["SubjectNum"].ToString();
        TMP_RoundPerSubject.text = ExperimentSetting["RoundPerSubject"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Script_Fade.CG.alpha > 0.9)
        {
            if (PrevButtonPressed)
            {
                SceneController.GoToSceneByIndex(0);
            }
        }
    }
    public void ProgramSettingButton()
    {
        Thread.Sleep(150);
        SettingButtons.SetActive(false);
        ExperimentSettingMenu.SetActive(false);
        ProgramSettingMenu.SetActive(true);

    }
    public void ExperimentSettingButton()
    {
        Thread.Sleep(150);
        SettingButtons.SetActive(false);
        ProgramSettingMenu.SetActive(false);
        ExperimentSettingMenu.SetActive(true);
    }
    public void PrevButton()
    {
        PrevButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    public void ReturnButton()
    {
        ProgramSettingMenu.SetActive(false);
        ExperimentSettingMenu.SetActive(false);
        SettingButtons.SetActive(true);
    }
    public void ApplyProgramSetting()
    {
        JsonFile.ProgramSetting PS = new()
        {
            ResolutionX = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[..4]),
            ResolutionY = Int32.Parse(TMP_Resolution.options[TMP_Resolution.value].text[5..]),
            IsFullScreen = 0,
            FPS = Int32.Parse(TMP_FPS.options[TMP_FPS.value].text)
        };
        JsonFile.WriteProgramSetting(PS);
        ProgramSettingController.RefreshDisplaySetting();
        Debug.Log("Program Setting Updated");
    }
    public void ApplyExperimentSetting()
    {
        JsonFile.ExperimentSetting ES = new()
        {
            SubjectNum = Int32.Parse(TMP_SubjectNum.text),
            RoundPerSubject = Int32.Parse(TMP_RoundPerSubject.text),
        };
        JsonFile.WriteExperimentSetting(ES);
        
        Debug.Log("Experiment Setting Updated");
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
        // 如果找不到匹配的选项，返回一个默认值（例如-1）
        return 0;
    }
}
