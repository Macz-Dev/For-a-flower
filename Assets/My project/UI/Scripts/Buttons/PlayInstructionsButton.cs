using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayInstructionsButton : MonoBehaviour
{
    public Button button;
    public Image imageButton;
    public Sprite playSymbol;
    public Sprite stopSymbol;
    void Start()
    {
        this.button.onClick.AddListener(() => OnPress());
        LevelManager.Instance.StopExecution += Stop;
    }

    void OnPress()
    {
        UIManager.Instance.audioSource.Stop();
        UIManager.Instance.audioSource.Play();
        if (LevelManager.Instance.currentState == LevelState.SELECTING_INSTRUCTIONS)
        {
            Play();
        }
        else if (LevelManager.Instance.currentState == LevelState.EXECUTING_INSTRUCTIONS)
        {
            LevelManager.Instance.ExecuteStopExecution();
            LevelManager.Instance.ExecuteResetLevel();
        }

    }

    void Play()
    {
        imageButton.sprite = stopSymbol;
        LevelManager.Instance.ExecuteStartExecution();
    }

    void Stop(object sender, EventArgs e)
    {
        imageButton.sprite = playSymbol;
    }

    void Reset()
    {
        imageButton.sprite = playSymbol;
    }

}
