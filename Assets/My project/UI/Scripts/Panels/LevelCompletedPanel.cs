using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
public class LevelCompletedPanel : MonoBehaviour
{
    public TextMeshProUGUI savedUsedPiecesTMP;
    public TextMeshProUGUI savedCollectedPiecesTMP;
    public TextMeshProUGUI newUsedPiecesTMP;
    public TextMeshProUGUI newCollectedPiecesTMP;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.LevelCompleted += LevelCompleted;
    }

    void LevelCompleted(object sender, EventArgs e)
    {
        SetDataOnUI();
        this.gameObject.SetActive(true);
    }

    void SetDataOnUI()
    {
        // Get information required
        string currentLevel = LevelManager.Instance.currentLevel;
        bool hasSavedDataFromThisLevel = PlayerData.levelsStats.ContainsKey(currentLevel);
        // Setting Texts
        // Saved Data
        this.savedUsedPiecesTMP.text = hasSavedDataFromThisLevel ? PlayerData.levelsStats[currentLevel].usedPieces.ToString() : "0";
        this.savedCollectedPiecesTMP.text = hasSavedDataFromThisLevel ? PlayerData.levelsStats[currentLevel].collectedPieces.ToString() : "0";
        // New Data
        this.newUsedPiecesTMP.text = LevelManager.Instance.instructionsSelected.instructionsSelected.Count.ToString();
        this.newCollectedPiecesTMP.text = LevelManager.Instance.collectedPieces.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
