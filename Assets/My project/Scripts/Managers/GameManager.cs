using UnityEngine;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameState currentState;
    public GameState previousState;
    public static GameManager Instance;
    public event EventHandler PauseEvent;
    public event EventHandler PlayEvent;
    public event EventHandler GoLevelMenu;

    public TextMeshProUGUI parchmentInfo;
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
        UpdateParchmentData();
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MAIN_MENU:
                break;
            case GameState.LEVEL_MENU:
                GoLevelMenu?.Invoke(this, EventArgs.Empty);
                UpdateParchmentData();
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

    void UpdateParchmentData()
    {
        this.parchmentInfo.text = PlayerData.AvailableParchmentPieces() + "/" + PlayerData.parchmentsQuantity;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

public enum GameState
{
    MAIN_MENU,
    LEVEL_MENU,
    PLAYING,
    PAUSE,
}
