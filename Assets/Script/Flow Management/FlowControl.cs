using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlowControl
{
    public static void PreviseScene()
    {
        //Debug.Log("Going Previse Scene" + Time.time);
        SceneController.GoToPrevScene();
    }
    public static void NextScene()
    {
        //Debug.Log("Going Next Scene" + Time.time);
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
