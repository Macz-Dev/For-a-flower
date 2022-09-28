using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class InstructionSelectedButton : MonoBehaviour
{
    public string id;
    public Image instructionSymbol;
    public Sprite instructionSymbolSprite;
    public Button button;
    public Image imageButton;
    public int instructionReferenceIndex;

    void Start()
    {
        LevelManager.Instance.ResetLevel += Reset;
        LevelManager.Instance.StartExecution += DisableButton;
        LevelManager.Instance.StopExecution += EnableButton;
        GameManager.Instance.GoLevelMenu += RemoveInstruction;
    }
    public void Initialize()
    {
        this.button.onClick.AddListener(() => RemoveInstruction());
        this.instructionSymbol.sprite = this.instructionSymbolSprite;
    }

    public void RemoveInstruction()
    {
        LevelManager.Instance.instructionsSelected.instructionsSelected.Remove(this);
        Destroy(this.gameObject);
    }

    public void RemoveInstruction(object sender, EventArgs e)
    {
        LevelManager.Instance.instructionsSelected.instructionsSelected.Remove(this);
        Destroy(this.gameObject);
    }

    public void NoExecutingInstruction()
    {
        this.imageButton.color = new Color(1f, 1f, 1f);
    }

    public void ExecutingInstruction()
    {
        this.imageButton.color = new Color(1f, 1f, 0f);
    }

    public void Reset(object sender, EventArgs e)
    {
        NoExecutingInstruction();
    }

    void DisableButton(object sender, EventArgs e)
    {
        this.button.interactable = false;
    }

    void EnableButton(object sender, EventArgs e)
    {
        this.button.interactable = true;
    }

    void OnDestroy()
    {
        LevelManager.Instance.ResetLevel -= Reset;
        LevelManager.Instance.StartExecution -= DisableButton;
        LevelManager.Instance.StopExecution -= EnableButton;
        GameManager.Instance.GoLevelMenu -= RemoveInstruction;
    }
}
