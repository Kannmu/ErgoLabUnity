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
    public Fade Script_Fade;
    public TextMeshProUGUI Percentage;

    void Start()
    {
        // Э�������첽����
        StartCoroutine(this.AsyncLoading());
        Script_Fade.CG.gameObject.SetActive(true);
        Script_Fade.CG_Alpha_Target = 1;
    }

    IEnumerator AsyncLoading()
    {
        this.asyncOperation = SceneManager.LoadSceneAsync((string)SceneController.sceneNames[SceneController.sceneIndex]);
        //��ֹ�Զ��л�����
        this.asyncOperation.allowSceneActivation = false;
        yield return asyncOperation;
    }
    void Update()
    {
        // Change Loading UI
        this.SettingLoadingUI(this.asyncOperation.progress);
        // 
        if (this.asyncOperation.progress >= 0.9f)
        {
            // Switch Scene
            this.asyncOperation.allowSceneActivation = true;
        }
    }
    public void SettingLoadingUI(float progress)
    {
        // �ٷֱȵķ�ʽ��ʾ����
        UnityEngine.Debug.Log("Loading:" + progress.ToString());
        Percentage.SetText("Loading: " + (progress * 100f).ToString() + "%");
        //this.progressText.text = (progress * 100f).ToString() + "%";
        //// ͨ���ı�image�Ŀ����ʵ�ֽ�����
        //this.progressBar.fillAmount = progress;
    }
}

