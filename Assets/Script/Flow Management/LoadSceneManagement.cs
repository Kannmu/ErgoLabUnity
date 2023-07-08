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
        // Start Coroutine
        //UnityEngine.Debug.Log("StartCoroutine" + "  " + Time.time);
        StartCoroutine(this.AsyncLoading());
        //Application.backgroundLoadingPriority = ThreadPriority.Low;
    }

    IEnumerator AsyncLoading()
    {
        this.asyncOperation = SceneManager.LoadSceneAsync((string)SceneController.sceneNames[SceneController.sceneIndex]);
        //Stop Auto Active New Scene
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
        // Change UI
        //UnityEngine.Debug.Log("Loading:" + (100f * progress).ToString() + "%" + "  " + Time.time);
        Percentage.SetText("Loading: " + (progress * 100f).ToString() + "%");

    }
}

