using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public GameState previousState;
    public static GameManager Instance;
    public event EventHandler PauseEvent;
    public event EventHandler PlayEvent;
    public event EventHandler GameOverEvent;

    public event EventHandler NextTick;
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
        ChangeState(GameState.PLAYING);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MAIN_MENU:
                break;
            case GameState.PLAYING:
                Time.timeScale = 1f;
                PlayEvent?.Invoke(this, EventArgs.Empty);
                break;
            case GameState.PAUSE:
                Time.timeScale = 0f;
                PauseEvent?.Invoke(this, EventArgs.Empty);
                break;
        }
    }
}

public enum GameState
{
    MAIN_MENU,
    PLAYING,
    PAUSE,
}
