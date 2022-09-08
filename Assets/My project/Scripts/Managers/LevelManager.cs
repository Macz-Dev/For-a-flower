using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private InstructionsExecutor instructionsExecutor;

    public event EventHandler NextTick;
    public event EventHandler ResetLevel;
    private bool hasInstructionsToExecute;

    public bool wasExitPointReached;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        this.instructionsExecutor = GetComponent<InstructionsExecutor>();
        this.hasInstructionsToExecute = true;
        this.wasExitPointReached = false;
    }

    public void ExecuteResetLevel()
    {
        ResetLevel?.Invoke(this, EventArgs.Empty);
    }

    public void ExecuteNextTick()
    {
        if (!wasExitPointReached)
        {
            NextTick?.Invoke(this, EventArgs.Empty);
        }
    }
}

public enum LevelState
{
    SELECTING_INSTRUCTIONS,
    EXECUTING_INSTRUCTIONS,
    WAITING_RESET
}
