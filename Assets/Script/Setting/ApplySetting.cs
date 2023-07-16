using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplySetting
{
    public static void RefreshDisplaySetting()
    {
        Application.targetFrameRate = JsonFile.Setting["FPS"];
        if (JsonFile.Setting["IsFullScreen"] == 1)
        {
            Screen.fullScreen = true;
            Screen.SetResolution(JsonFile.Setting["ResolutionX"], JsonFile.Setting["ResolutionY"], true);
        }
        else
        {
            Screen.fullScreen = false;
            Screen.SetResolution(JsonFile.Setting["ResolutionX"], JsonFile.Setting["ResolutionY"], false);
        }
    }
}
