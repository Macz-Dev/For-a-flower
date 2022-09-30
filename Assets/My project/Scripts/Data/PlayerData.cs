using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static Dictionary<string, LevelStats> levelsStats;
    public static int usedParchments;
    public static int parchmentsQuantity;

    public static int AvailableParchmentPieces()
    {
        return PlayerData.parchmentsQuantity - PlayerData.usedParchments;
    }

    public static int AvailableParchmentPieces(string level)
    {
        int globalQuantity = parchmentsQuantity - CollectedParchmentPreviously(level);
        return globalQuantity - usedParchments + PlayerData.UsedParchmentPreviously(level);
    }

    public static int UsedParchmentPreviously(string level)
    {
        return HasCompleteTheLevel(level) ? levelsStats[level].usedPieces : 0;
    }

    public static int CollectedParchmentPreviously(string level)
    {
        return HasCompleteTheLevel(level) ? levelsStats[level].collectedPieces : 0;
    }
    public static bool HasCompleteTheLevel(string level)
    {
        return levelsStats.ContainsKey(level);
    }
}

public class LevelStats
{
    public int usedPieces;
    public int collectedPieces;
}
