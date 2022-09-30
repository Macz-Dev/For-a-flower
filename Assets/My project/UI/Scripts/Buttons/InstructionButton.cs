using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class InstructionButton : MonoBehaviour
{
    public string id;
    public Image instructionSymbol;
    public Sprite instructionSymbolSprite;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => SelectInstruction());
        this.instructionSymbol.sprite = this.instructionSymbolSprite;
        LevelManager.Instance.StartExecution += DisableButton;
        LevelManager.Instance.StopExecution += EnableButton;
        LevelManager.Instance.NoAvailablePieces += DisableButton;
        LevelManager.Instance.AvailablePieces += EnableButton;
    }

    void SelectInstruction()
    {
        UIManager.Instance.audioSource.Stop();
        UIManager.Instance.audioSource.Play();
        LevelManager.Instance.instructionsSelected.AddInstructionSelected(this.id, this.instructionSymbolSprite);
        LevelManager.Instance.UpdateUsedPiecesTMP();
    }

    void DisableButton(object sender, EventArgs e)
    {
        this.button.interactable = false;
    }

    void EnableButton(object sender, EventArgs e)
    {
        this.button.interactable = true;
    }

}
