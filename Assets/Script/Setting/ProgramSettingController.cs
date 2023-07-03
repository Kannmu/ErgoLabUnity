using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Events;

public static class ProgramSettingController
{
    public static void RefreshDisplaySetting()
    {
        Dictionary<string, int> ProgramSetting = JsonFile.ReadProgramSetting();
        Application.targetFrameRate = ProgramSetting["FPS"];
        if (ProgramSetting["IsFullScreen"] == 1)
        {
            Screen.fullScreen = true;
            Screen.SetResolution(ProgramSetting["ResolutionX"], ProgramSetting["ResolutionY"], true);
        }
        else
        {
            Screen.fullScreen = false;
            Screen.SetResolution(ProgramSetting["ResolutionX"], ProgramSetting["ResolutionY"], false);
        }
    }
}
