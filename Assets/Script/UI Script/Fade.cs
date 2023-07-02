// Fade In Effect Script for Unity Experiment Template
// Author: Kannmu
// Date: 2023.7.1
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    // Variables
    public float CG_Alpha_Target;
    // Components
    public CanvasGroup CG;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            // Set Initial Alpha to zero
            CG.alpha = 1.0f;
            // Set Initial Alpha Target to one
            CG_Alpha_Target = 0.0f;
        }
        catch
        {
            Debug.LogError("ERROR: δ��FadeMask���壬����Hierarchy����н�Fade/FadeMask������ק������Fade.cs�ű�CG���ڡ�");
        }
        
    }
    void FixedUpdate()
    {
        CG.alpha += Approach(CG.alpha, CG_Alpha_Target, 0.06f);
    }
    // Calculate approach delta
    private float Approach(float Value, float Target, float Speed)
    {
        float Temp = Speed * (float)Math.Pow(Target - Value, 1.0f);
        return Temp;
    }
}
