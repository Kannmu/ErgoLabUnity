using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlowControl
{
    public static void PreviseScene()
    {
        SceneController.GoToPrevScene();
    }
    public static void NextScene()
    {
        SceneController.GoToNextScene();
    }
    public static void Exit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
