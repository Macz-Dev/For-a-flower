using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InstructionsExecutor : MonoBehaviour
{
    public string[] instructions;
    private int indexOfInstructionToExecute;
    [SerializeField] Gelem gelem;

    void Start()
    {
        this.instructions = new string[] { "MF", "TL", "MF", "MF", "MF", "TL", "TL", "MF", "TR", "MF" };
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
            Debug.Log(instructionToExecute);
            switch (instructionToExecute)
            {
                case "MF": // Move forward
                    this.gelem.MoveForward();
                    break;
                case "TL": // Turn left
                    this.gelem.turningDirection = TurningDirection.LEFT;
                    this.gelem.Turn();
                    break;
                case "TR": // Turn right
                    this.gelem.turningDirection = TurningDirection.RIGHT;
                    this.gelem.Turn();
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
