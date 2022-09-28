using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsSelected : MonoBehaviour
{
    public GameObject container;
    public GameObject instructionSelectedPrefab;
    public List<InstructionSelectedButton> instructionsSelected;
    // Start is called before the first frame update
    void Awake()
    {
        this.instructionsSelected = new List<InstructionSelectedButton> { };
    }

    public void AddInstructionSelected(string id, Sprite instructionSymbol)
    {
        GameObject newInstructionSelected = Instantiate(this.instructionSelectedPrefab, this.container.transform);
        InstructionSelectedButton newInstructionSelectedButton = newInstructionSelected.GetComponent<InstructionSelectedButton>();
        newInstructionSelectedButton.id = id;
        newInstructionSelectedButton.instructionSymbolSprite = instructionSymbol;
        newInstructionSelectedButton.Initialize();
        instructionsSelected.Add(newInstructionSelectedButton);
    }

    public void NoHighlighted()
    {
        foreach (var instructionSelected in this.instructionsSelected)
        {
            instructionSelected.NoExecutingInstruction();
        }
    }
}
