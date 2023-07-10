using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplySetting
{
    public static void RefreshDisplaySetting()
    {
        Dictionary<string, int> Setting = JsonFile.ReadSetting(ExcelFile.ExperimentName);
        Application.targetFrameRate = Setting["FPS"];
        if (Setting["IsFullScreen"] == 1)
        {
            Screen.fullScreen = true;
            Screen.SetResolution(Setting["ResolutionX"], Setting["ResolutionY"], true);
        }
        else
        {
            Screen.fullScreen = false;
            Screen.SetResolution(Setting["ResolutionX"], Setting["ResolutionY"], false);
        }
    }
}
