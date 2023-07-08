using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.FullSerializer;

public static class SceneController
{
    // 场景列表数组，这里按顺序填写已经存在的scene 的场景名。
    // Scene List Array, Add SceneName Here In Order
    public static ArrayList sceneNames = new ArrayList() {
        "Intro",
        "Setting",
        "Experiment",
        "Notification"
    };

    // Set Loading Scene Name
    public static string LoadingSceneName = "LoadingScene";
    // Define a call back Action
    public static Action AfterGoToScene;

    public static int sceneIndex = 0;

    public static void GoToNextScene()
    {
        // Update sceneIndex
        SceneController.sceneIndex = SceneController.sceneNames.IndexOf(SceneManager.GetActiveScene().name);
        if (SceneController.sceneIndex < SceneController.sceneNames.Count - 1)
        {
            SceneController.sceneIndex++;
            GoToSceneByIndex(SceneController.sceneIndex);
        }
        else
        {
            Debug.LogError("ERROR: Failed to switch to the next Scene, the next Scene does not exist. Please check if the Scene list in SceneController.cs is filled out correctly.");
        }
    }

    public static void GoToPrevScene()
    {
        // Update sceneIndex
        SceneController.sceneIndex = SceneController.sceneNames.IndexOf(SceneManager.GetActiveScene().name);
        if (SceneController.sceneIndex > 0)
        {
            SceneController.sceneIndex--;
            GoToSceneByIndex(SceneController.sceneIndex);
        }
        else
        {
            Debug.LogError("ERROR: Failed to switch to the previous Scene, the previous Scene does not exist. Please check if the Scene list in SceneController.cs is filled out correctly.");
        }
    }

    /// <summary>
    /// goto the scene if finded name in scenelist,atherwase reopen current scene 
    /// </summary>
    /// <param name="sceneName"></param>
    public static void GoToSceneByName(string sceneName)
    {
        if (SceneController.sceneNames.Contains(sceneName))
        {
            GoToSceneByIndex(SceneController.sceneNames.IndexOf(sceneName));
        }
        else 
        {
            Debug.LogError("ERROR: The input SceneName was not found. Please check if the SceneName value is correct");
            FlowControl.Exit();
        }
    }
    // Switch Scene Based on Scene Index
    public static void GoToSceneByIndex(int sceneIndex)
    {
        // Update sceneIndex
        SceneController.sceneIndex = sceneIndex;

        if ((SceneController.sceneIndex >= 0) & (SceneController.sceneIndex < SceneController.sceneNames.Count))
        {
            // Load Loading Scene
            //Debug.Log("Load Loading Scene" + "  " + Time.time);
            SceneManager.LoadScene(LoadingSceneName);
            //Debug.Log("Finish Load Loading Scene" + "  " + Time.time);
            AfterGoToScene?.Invoke();
        }
        else
        {
            Debug.LogError("ERROR: The input SceneIndex is out of range. Please check if the SceneIndex value is within the valid range in SceneController.cs.");
            FlowControl.Exit();
        }
    }
    public static void UnLoadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    public static void UnLoadSceneById(int sceneID)
    {
        SceneManager.UnloadSceneAsync((string)SceneController.sceneNames[sceneID]);
    }
}
