using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectDifficulty : MonoBehaviour
{
    private readonly string difficultyKey;

    private void Start()
    {
        PlayerPrefs.GetFloat(difficultyKey);
    }

    //For easy button
    public void SetToEasy()
    {
        
        OnDifficultyChanged();
    }

    //For normal button
    public void SetToNormal()
    {
        OnDifficultyChanged();
    }

    //For hard button
    public void SetToHard()
    {
        
        OnDifficultyChanged();
    }

    //For nightmare button
    public void SetToNightmare()
    {
        
        OnDifficultyChanged();
    }

    private void OnDifficultyChanged()
    {
        
        PlayerPrefs.Save();
    }
}
