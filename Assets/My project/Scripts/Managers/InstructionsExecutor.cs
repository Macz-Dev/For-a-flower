using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InstructionsExecutor : MonoBehaviour
{
    public string[] instructions;
    private int indexOfInstructionToExecute;
    public Gelem gelem;

    void Start()
    {
        this.instructions = new string[] { "DM", "MF", "NA", "EM", "MF", "TL", "MF", "TR", "MF", "DM", "EM" };
        LevelManager.Instance.NextTick += Execute;
        LevelManager.Instance.ResetLevel += ResetInitialValues;

    }
    void ResetInitialValues(object sender, EventArgs e)
    {
        this.indexOfInstructionToExecute = 0;
    }

    public void Execute(object sender, EventArgs e)
    {
        if (HasInstructionsToExecute())
        {
            string instructionToExecute = this.instructions[this.indexOfInstructionToExecute];
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
        return this.instructions.Length > this.indexOfInstructionToExecute;
    }
}
