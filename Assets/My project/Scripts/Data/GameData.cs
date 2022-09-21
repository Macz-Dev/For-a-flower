using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static Dictionary<string, LevelData> Levels = new Dictionary<string, LevelData>()
    {
        ["1"] = new LevelData(
            6, // Optimums Pieces
            10, // Parchment Pieces Required
            new string[] { "MF", "TL", "TR" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "exit-ewf", "wall", "wall", "wall", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "gelem", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
        ["2"] = new LevelData(
            11, // Optimums Pieces
            15, // Parchment Pieces Required
            new string[] { "MF", "TL", "TR" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "wall", "exit-ewf", "wall", "wall", "wall" },
            { "wall", "iWall", "iWall", "floor", "iWall", "iWall", "wall" },
            { "wall", "iWall", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "iWall", "floor", "hole", "floor", "iWall", "wall" },
            { "wall", "iWall", "floor", "floor", "floor", "iWall", "wall" },
            { "wall", "iWall", "iWall", "gelem", "iWall", "iWall", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
        ["3"] = new LevelData(
            11, // Optimums Pieces
            30, // Parchment Pieces Required
            new string[] { "MF", "TL", "TR", "NA", "EM", "DM" }, // Usable Instructions
            new string[,]{ // Scenery Template
            { "wall", "wall", "wall", "wall", "exit-ewf", "wall", "wall" },
            { "wall", "iWall", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "iWall", "floor", "floor", "hole", "iWall", "wall" },
            { "wall", "iWall", "fire-l", "floor", "fire-dtl-l", "iWall", "wall" },
            { "wall", "iWall", "fire-dtr-u-3", "floor", "hole", "iWall", "wall" },
            { "wall", "iWall", "iWall", "gelem", "iWall", "iWall", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }}),
    };
}

public class LevelData
{
    public int optimumsPieces;
    public int parchmentPiecesRequired;
    public string[,] sceneryTemplate;
    public string[] usableInstructions;
    public LevelData(int optimumsPieces, int parchmentPiecesRequired, string[] usableInstructions, string[,] sceneryTemplate)
    {
        this.optimumsPieces = optimumsPieces;
        this.parchmentPiecesRequired = parchmentPiecesRequired;
        this.usableInstructions = usableInstructions;
        this.sceneryTemplate = sceneryTemplate;
    }
}
