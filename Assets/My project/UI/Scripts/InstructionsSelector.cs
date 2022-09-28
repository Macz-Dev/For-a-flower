using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
public class InstructionsSelector : MonoBehaviour
{
    public Button previousInstructionViewButton;
    public InstructionButton[] instructionButtons;
    public RectTransform instructionsView;
    public Button nextInstructionViewButton;
    public string[] usableInstructions;
    private float viewPositionOffset = 175f;
    private int instructionsViewScroll;
    // Start is called before the first frame update
    void Awake()
    {
        this.instructionButtons = this.GetComponentsInChildren<InstructionButton>(true);
    }
    void Start()
    {
        previousInstructionViewButton.onClick.AddListener(() => ShowPreviousInstructionView());
        nextInstructionViewButton.onClick.AddListener(() => ShowNextInstructionView());
    }

    public void SetLevelConfiguration()
    {
        this.instructionsViewScroll = 0;
        SetUsableInstructionButtons();
        SetInstructionViewButtons();
    }

    void SetInstructionViewButtons()
    {
        this.previousInstructionViewButton.interactable = false;
        this.nextInstructionViewButton.interactable = this.usableInstructions.Length > 5 ? true : false;
    }

    void SetUsableInstructionButtons()
    {
        this.usableInstructions = GameData.Levels[LevelManager.Instance.currentLevel].usableInstructions;
        foreach (var instructionButton in instructionButtons)
        {
            Debug.Log(instructionButton.id);
            if (Array.Exists(this.usableInstructions, x => x == instructionButton.id))
            {
                instructionButton.gameObject.SetActive(true);
            }
            else
            {
                instructionButton.gameObject.SetActive(false);
            }
        }
    }

    void ShowNextInstructionView()
    {
        this.instructionsViewScroll += 1;
        float newXPosition = -(this.instructionsViewScroll * this.viewPositionOffset);
        this.instructionsView.anchoredPosition = new Vector2(newXPosition, this.instructionsView.anchoredPosition.y);
        RefreshInstructionViewButtonsState();
    }

    void ShowPreviousInstructionView()
    {
        this.instructionsViewScroll -= 1;
        float newXPosition = (this.instructionsViewScroll * this.viewPositionOffset);
        this.instructionsView.anchoredPosition = new Vector2(newXPosition, this.instructionsView.anchoredPosition.y);
        RefreshInstructionViewButtonsState();
    }

    void RefreshInstructionViewButtonsState()
    {
        // For previous button
        this.previousInstructionViewButton.interactable = this.instructionsViewScroll > 0 ? true : false;
        this.nextInstructionViewButton.interactable = this.usableInstructions.Length > (this.instructionsViewScroll + 5) ? true : false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
