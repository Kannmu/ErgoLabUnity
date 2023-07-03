using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class LoadSceneManagement : MonoBehaviour
{
    // Variables
    private AsyncOperation asyncOperation;

    // Components
    public TextMeshProUGUI Percentage;

    void Start()
    {
        // 协程启动异步加载
        //UnityEngine.Debug.Log("StartCoroutine" + "  " + Time.time);
        StartCoroutine(this.AsyncLoading());
        Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    IEnumerator AsyncLoading()
    {
        this.asyncOperation = SceneManager.LoadSceneAsync((string)SceneController.sceneNames[SceneController.sceneIndex]);
        //终止自动切换场景
        this.asyncOperation.allowSceneActivation = false;
        this.asyncOperation.priority = 0;
        yield return asyncOperation;
    }
    void Update()
    {
        // Change Loading UI
        this.SettingLoadingUI(this.asyncOperation.progress);
        if (this.asyncOperation.progress >= 0.9f)
        {
            // Switch Scene
            //UnityEngine.Debug.Log("Start Activate Next Scene" + "  " + Time.time);
            this.asyncOperation.allowSceneActivation = true;
        }
    }
    public void SettingLoadingUI(float progress)
    {
        // 百分比的方式显示进度
        //UnityEngine.Debug.Log("Loading:" + (100f * progress).ToString() + "%" + "  " + Time.time);
        Percentage.SetText("Loading: " + (progress * 100f).ToString() + "%");

    }
}

