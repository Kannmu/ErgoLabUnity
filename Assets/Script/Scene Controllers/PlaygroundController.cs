using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundController : MonoBehaviour
{

    // Variables
    private bool StartButtonPressed;

    // Script
    public Fade Script_Fade;

    // Start is called before the first frame update
    void Start()
    {
        // Activate Fade
        Script_Fade.CG.gameObject.SetActive(true);

        StartButtonPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Script_Fade.CG.alpha > 0.95)
        {
            if(StartButtonPressed)
            {
                StartButtonPressed = false;
                SceneController.GoToSceneByName("Experiment");
            }
        }
    }


    public void StartButton()
    {
        // Start Real Experiment
        StartButtonPressed = true;
        Script_Fade.CG_Alpha_Target = 1f;
    }
}
