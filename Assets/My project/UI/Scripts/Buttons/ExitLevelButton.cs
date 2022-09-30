using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ExitLevelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;


    void Start()
    {
        this.button.onClick.AddListener(() => ExitLevel());
        LevelManager.Instance.StartExecution += DisableButton;
        LevelManager.Instance.StopExecution += EnableButton;
    }

    void ExitLevel()
    {
        LevelManager.Instance.levelGenerator.CleanPreviousLevels();
        GameManager.Instance.ChangeState(GameState.LEVEL_MENU);
        UIManager.Instance.SetCurrentPanel("LevelMenu");
    }

    void DisableButton(object sender, EventArgs e)
    {
        this.button.interactable = false;
    }

    void EnableButton(object sender, EventArgs e)
    {
        this.button.interactable = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.audioSource.Stop();
        UIManager.Instance.audioSource.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.audioSource.Stop();
    }
}
