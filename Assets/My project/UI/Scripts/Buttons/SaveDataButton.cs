using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveDataButton : MonoBehaviour
{
    public Button button;
    void Start()
    {
        this.button.onClick.AddListener(() => SaveData());
    }

    void SaveData()
    {
        string currentLevel = LevelManager.Instance.currentLevel;
        if (!PlayerData.levelsStats.ContainsKey(currentLevel))
        {
            PlayerData.levelsStats.Add(currentLevel, new LevelStats());
        }
        PlayerData.levelsStats[currentLevel].usedPieces = LevelManager.Instance.instructionsSelected.instructionsSelected.Count;
        PlayerData.levelsStats[currentLevel].collectedPieces = LevelManager.Instance.collectedPieces;
        DataManager.Instance.UpdateData();
        DataManager.Instance.SaveData();
        LevelManager.Instance.levelGenerator.CleanPreviousLevels();
        GameManager.Instance.ChangeState(GameState.LEVEL_MENU);
        UIManager.Instance.SetCurrentPanel("LevelMenu");
    }
}
