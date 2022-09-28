using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public LevelGenerator levelGenerator;
    public InstructionsSelector instructionsSelector;
    public InstructionsSelected instructionsSelected;
    public string currentLevel;
    public LevelState currentState;
    public InstructionsExecutor instructionsExecutor;

    public event EventHandler StartExecution;
    public event EventHandler NextTick;
    public event EventHandler ResetLevel;
    public event EventHandler StopExecution;

    public event EventHandler LevelCompleted;

    public int collectedPieces;

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
    }

    public void ExecuteResetLevel()
    {
        this.collectedPieces = 0;
        ResetLevel?.Invoke(this, EventArgs.Empty);
    }

    public void ExecuteNextTick()
    {
        if (this.currentState == LevelState.EXECUTING_INSTRUCTIONS)
        {
            NextTick?.Invoke(this, EventArgs.Empty);
        }
        if (this.currentState == LevelState.COMPLETED)
        {
            ExecuteLevelCompleted();
        }
    }

    public void ExecuteStartExecution()
    {
        StartExecution?.Invoke(this, EventArgs.Empty);
        ExecuteResetLevel();
        this.currentState = LevelState.EXECUTING_INSTRUCTIONS;
        ExecuteNextTick();
    }
    public void ExecuteStopExecution()
    {
        this.currentState = LevelState.SELECTING_INSTRUCTIONS;
        StopExecution?.Invoke(this, EventArgs.Empty);
    }

    public void ExecuteLevelCompleted()
    {
        this.instructionsSelected.NoHighlighted();
        StopExecution?.Invoke(this, EventArgs.Empty);
        LevelCompleted?.Invoke(this, EventArgs.Empty);
    }
    public void LoadLevel()
    {
        this.levelGenerator.GenerateLevel(this.currentLevel);
        this.instructionsSelector.SetLevelConfiguration();
        InitializeLevel();
        UIManager.Instance.SetCurrentPanel("Game");
    }

    public void InitializeLevel()
    {
        this.currentState = LevelState.SELECTING_INSTRUCTIONS;
    }
}

public enum LevelState
{
    SELECTING_INSTRUCTIONS,
    EXECUTING_INSTRUCTIONS,
    COMPLETED
}
