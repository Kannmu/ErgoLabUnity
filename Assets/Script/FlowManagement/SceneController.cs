using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.FullSerializer;

public static class SceneController
{
    // 场景列表数组，这里按顺序填写已经存在的scene 的场景名。
    public static ArrayList sceneNames = new ArrayList() {
        "IntroAndMenu",
        "Notification",
    };
    // 设定loadingScene名。
    public static string LoadingSceneName = "LoadingScene";
    // 定义一个回调事件  当完成任何场景切换时触发
    public static Action AfterGoToScene;

    // 即将切换场景的序号
    public static int sceneIndex = 0;
    // 根据场景顺序，切换到下一个场景的方法
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
            Debug.LogError("ERROR: 切换下一个Scene错误，不存在下一个Scene。请检查SceneController.cs中的Scene列表是否填写正确。");
        }
    }
    // 根据场景顺序，切换到上一个场景
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
            Debug.LogError("ERROR: 切换上一个Scene错误，不存在上一个Scene。请检查SceneController.cs中的Scene列表是否填写正确。");
        }
    }

    // 根据场景名称（scene name） 切换到目标场景 （scene index会跟着变动）
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
    }
    //根据 场景序号Index切换到目标场景
    public static void GoToSceneByIndex(int sceneIndex)
    {
        // Update sceneIndex
        SceneController.sceneIndex = sceneIndex;
        if ((SceneController.sceneIndex >= 0) & (SceneController.sceneIndex < SceneController.sceneNames.Count))
        {
            // Load Loading Scene
            SceneManager.LoadScene(LoadingSceneName);
            AfterGoToScene?.Invoke();
        }
        else
        {
            Debug.LogError("ERROR: 输入的SceneIndex不在范围内");
        }
    }
    // 根据目标场景名称 注销目标场景，这个只是为了统一管理，因为unity早就内置了方法
    public static void UnLoadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    // 根据目标场景序号 注销目标场景
    public static void UnLoadSceneById(int sceneID)
    {
        SceneManager.UnloadSceneAsync((string)SceneController.sceneNames[sceneID]);
    }
}
