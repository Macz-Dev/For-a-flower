using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static Dictionary<string, LevelStats> levelsStats;
    public static int parchmentsExpended;
    public static int parchmentsQuantity;

    public static int AvailableParchmentPieces()
    {
        return PlayerData.parchmentsQuantity - PlayerData.parchmentsExpended;
    }
}

public class LevelStats
{
    public int usedPieces;
    public int parchmentPiecesObtained;
}
