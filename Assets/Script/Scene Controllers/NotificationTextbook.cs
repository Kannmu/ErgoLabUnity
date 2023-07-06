// Notification Textbook Script for Unity Experiment Template
// Author: Kannmu
// Date: 2023.7.1
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationTextbook : MonoBehaviour
{
    // Variables
    private int Page_Index;
    private string[] TextInLine;
    private bool StartButtonPressed, PrevButtonPressed;

    // Components
    public TextMeshProUGUI Content;
    public TextMeshProUGUI Footnote;
    public Button NextButtonComponent, BackButtonComponent,StartButtonComponent, PrevButtonComponent;

    // Scripts
    public Fade Script_Fade;

    // Data
    public TextAsset NotificationText;

    // Start is called before the first frame update
    void Start()
    {
        StartButtonPressed = false;
        PrevButtonPressed = false;
        //Debug.Log("Notification Start" + "  " + Time.time);
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
            Debug.LogError("ERROR: Experiment Notification file error, this may be due to a missing file or content not written according to specifications. Please check if the Experiment Notification.txt file exists in the Assert/Data/ directory, or if it is filled out according to the specifications.");
            FlowControl.Exit();
        }
        //Debug.Log("Text Loaded" + "  " + Time.time);
        // Initialize Page Index
        Page_Index = 0;
        // Initial Button Toggle
        ToggleButton();
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(Time.time);
        // Refresh the content and footnote
        Content.SetText(TextInLine[Page_Index]);
        Footnote.SetText((Page_Index + 1).ToString());
        // Switch Scene, Controlled by alpha of the Fade
        if (StartButtonPressed)
        {
            if ((Script_Fade.CG.alpha > 0.95)) 
            {
                StartButtonPressed = false;
                PrevButtonPressed = false;
                SceneController.GoToSceneByName("ExperimentScene");
            }
        }
        if (PrevButtonPressed)
        {
            if ((Script_Fade.CG.alpha > 0.95))
            {
                StartButtonPressed = false;
                PrevButtonPressed = false; 
                SceneController.GoToSceneByName("SettingScene");
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
            PrevButtonComponent.gameObject.SetActive(true);
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
            PrevButtonComponent.gameObject.SetActive(false);
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
        PrevButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
}
