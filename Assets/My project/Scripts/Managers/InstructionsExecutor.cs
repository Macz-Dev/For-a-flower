using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InstructionsExecutor : MonoBehaviour
{
    private int indexOfInstructionToExecute;
    public Gelem gelem;

    void Start()
    {
        LevelManager.Instance.ResetLevel += ResetInitialValues;
        LevelManager.Instance.NextTick += Execute;
    }
    public void ResetInitialValues()
    {
        this.indexOfInstructionToExecute = 0;
    }

    public void ResetInitialValues(object sender, EventArgs e)
    {
        this.indexOfInstructionToExecute = 0;
    }

    public void Execute(object sender, EventArgs e)
    {
        LevelManager.Instance.instructionsSelected.NoHighlighted();

        if (!HasInstructionsToExecute())
        {
            LevelManager.Instance.ExecuteStopExecution();
        }
        else if (LevelManager.Instance.currentState == LevelState.EXECUTING_INSTRUCTIONS)
        {
            string instructionToExecute = LevelManager.Instance.instructionsSelected.instructionsSelected[this.indexOfInstructionToExecute].id;
            LevelManager.Instance.instructionsSelected.instructionsSelected[this.indexOfInstructionToExecute].ExecutingInstruction();
            switch (instructionToExecute)
            {
                case "MF": // Move forward
                    this.gelem.MoveForward();
                    break;
                case "EM": // Move forward
                    this.gelem.ElevateMikeas();
                    break;
                case "DM": // Move forward
                    this.gelem.DownMikeas();
                    break;
                case "TL": // Turn left
                    this.gelem.turningDirection = TurningDirection.LEFT;
                    this.gelem.Turn();
                    break;
                case "TR": // Turn right
                    this.gelem.turningDirection = TurningDirection.RIGHT;
                    this.gelem.Turn();
                    break;
                case "NA": // Do Nothing
                    this.gelem.NoAction();
                    break;
                default:

                    break;
            }
            // Preparing next instruction
            this.indexOfInstructionToExecute++;
        }
    }

    public bool HasInstructionsToExecute()
    {
        return LevelManager.Instance.instructionsSelected.instructionsSelected.Count > this.indexOfInstructionToExecute;
    }
}
