using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static Dictionary<string, LevelData> Levels = new Dictionary<string, LevelData>()
    {
        ["1"] = new LevelData(
            6, // Collectible pieces
            7, // Parchment Pieces Required
            new string[] { "MF", "NA", "TL", "TR" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "exit-ewf", "wall", "wall", "wall", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "parchment-8", "iWall", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "iWall", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "gelem", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
        ["2"] = new LevelData(
            20, // Collectible pieces
            11, // Parchment Pieces Required
            new string[] { "MF", "NA", "TL", "TR" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "wall", "exit-ewf", "wall", "wall", "wall" },
            { "wall", "parchment-40", "iWall", "floor", "iWall", "parchment-40", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "iWall", "floor", "hole", "floor", "iWall", "wall" },
            { "wall", "iWall", "floor", "floor", "floor", "iWall", "wall" },
            { "wall", "iWall", "iWall", "gelem", "iWall", "iWall", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
        ["3"] = new LevelData(
            0, // Collectible pieces
            20, // Parchment Pieces Required
            new string[] { "MF", "NA", "TL", "TR", "EM", "DM" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "wall", "wall", "exit-ewf", "wall", "wall" },
            { "wall", "iWall", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "iWall", "floor", "floor", "hole", "iWall", "wall" },
            { "wall", "iWall", "floor", "floor", "fire-dtl-l", "iWall", "wall" },
            { "wall", "iWall", "fire-dtr-u-3", "floor", "hole", "iWall", "wall" },
            { "wall", "iWall", "iWall", "gelem", "iWall", "iWall", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
    };
}

public class LevelData
{
    public int collectiblePieces;
    public int parchmentPiecesRequired;
    public string[,] sceneryTemplate;
    public string[] usableInstructions;
    public LevelData(int collectiblePieces, int parchmentPiecesRequired, string[] usableInstructions, string[,] sceneryTemplate)
    {
        this.collectiblePieces = collectiblePieces;
        this.parchmentPiecesRequired = parchmentPiecesRequired;
        this.usableInstructions = usableInstructions;
        this.sceneryTemplate = sceneryTemplate;
    }
}
