// Notification Textbook Script for Unity Experiment Template
// Author: Kannmu
// Date: 2023.7.1
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationTextbook : MonoBehaviour
{
    // Variables
    private int Page_Index;
    private string[] TextInLine;
    private bool StartButtonPressed, MenuButtonPressed;

    // Components
    public TextMeshProUGUI Content;
    public TextMeshProUGUI Footnote;
    public Button NextButtonComponent, BackButtonComponent,StartButtonComponent, MenuButtonComponent;

    // Scripts
    public Fade Script_Fade;

    // Data
    public TextAsset NotificationText;


    private void Awake()
    {
        //Limit the FPS to 60
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Active the Fade
        Script_Fade.CG.gameObject.SetActive(true);
        // Read and process Experiment Notification
        try
        {
            // Spilt Text into lines
            TextInLine = NotificationText.text.Split('\n');
            // Clear Null, "\n" and "\r" Value
            TextInLine = TextInLine.Where(s => (!string.IsNullOrEmpty(s)) & s != "\n" & s != "\r").ToArray();
        }
        catch
        {
            Debug.LogError("ERRPR: Experiment Notification文件出错，这可能是由于未按照规范书写内容。请检查Assert/Setting/TextContent/目录下 Experiment Notification.txt 文件是否存在，或是否按规范填写。");
        }
        
        // Initialize Page Index
        Page_Index = 0;
        // Initial Button Toggle
        ToggleButton();
        StartButtonPressed = false;
        MenuButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        print(Script_Fade.CG.alpha);
        // Refresh the content and footnote
        Content.SetText(TextInLine[Page_Index]);
        Footnote.SetText((Page_Index + 1).ToString());
        // Switch Scene, Controlled by alpha of the Fade
        if (Script_Fade.CG.alpha > 0.88 )
        {
            if (StartButtonPressed)
            {
                NextScene();
            }
            if (MenuButtonPressed)
            {
                LoadMenu();
            }
        }
    }
    void FixedUpdate()
    {

    }

    public void NextButton()
    {
        Page_Index += 1;
        if (Page_Index > TextInLine.Length)
        {
            Page_Index = TextInLine.Length - 1;
        }
        if (Page_Index < 0)
        {
            Page_Index = 0;
        }
        ToggleButton();


    }
    public void BackButton()
    {
        Page_Index -= 1;
        if (Page_Index > TextInLine.Length)
        {
            Page_Index = TextInLine.Length - 1;
        }
        if (Page_Index < 0)
        {
            Page_Index = 0;
        }
        ToggleButton();
    }
    private void ToggleButton()
    {
        // Disable the button in the first and last page
        if (Page_Index == 0)
        {
            BackButtonComponent.gameObject.SetActive(false);
            MenuButtonComponent.gameObject.SetActive(true);
        }
        else if (Page_Index == TextInLine.Length - 1)
        {
            NextButtonComponent.gameObject.SetActive(false);
            StartButtonComponent.gameObject.SetActive(true);
        }
        else
        {
            NextButtonComponent.gameObject.SetActive(true);
            BackButtonComponent.gameObject.SetActive(true);
            StartButtonComponent.gameObject.SetActive(false);
            MenuButtonComponent.gameObject.SetActive(false);
        }

    }
    // Start Experiment Button
    public void StartButton()
    {
        StartButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    public void MenuButton()
    {
        MenuButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Exit Button
    public void ExitButton()
    {
        Invoke("Exit", 0.5f);
        Script_Fade.CG_Alpha_Target = 1f;
    }
    // Exit the game
    void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void LoadMenu()
    {
        SceneManager.LoadScene("IntroAndMenu");
    }
}
