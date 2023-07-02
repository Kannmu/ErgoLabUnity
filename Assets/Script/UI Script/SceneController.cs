using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting.FullSerializer;

public static class SceneController
{
    // �����б����飬���ﰴ˳����д�Ѿ����ڵ�scene �ĳ�������
    public static ArrayList sceneNames = new ArrayList() {
        "IntroAndMenu",
        "Notification",
    };
    // �趨loadingScene����
    public static string LoadingSceneName = "LoadingScene";
    // ����һ���ص��¼�  ������κγ����л�ʱ����
    public static Action AfterGoToScene;

    // �����л����������
    public static int sceneIndex = 0;
    // ���ݳ���˳���л�����һ�������ķ���
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
            Debug.LogError("ERROR: �л���һ��Scene���󣬲�������һ��Scene������SceneController.cs�е�Scene�б��Ƿ���д��ȷ��");
        }
    }
    // ���ݳ���˳���л�����һ������
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
            Debug.LogError("ERROR: �л���һ��Scene���󣬲�������һ��Scene������SceneController.cs�е�Scene�б��Ƿ���д��ȷ��");
        }
    }

    // ���ݳ������ƣ�scene name�� �л���Ŀ�곡�� ��scene index����ű䶯��
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
    //���� �������Index�л���Ŀ�곡��
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
            Debug.LogError("ERROR: �����SceneIndex���ڷ�Χ��");
        }
    }
    // ����Ŀ�곡������ ע��Ŀ�곡�������ֻ��Ϊ��ͳһ������Ϊunity��������˷���
    public static void UnLoadSceneByName(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
    // ����Ŀ�곡����� ע��Ŀ�곡��
    public static void UnLoadSceneById(int sceneID)
    {
        SceneManager.UnloadSceneAsync((string)SceneController.sceneNames[sceneID]);
    }
}
