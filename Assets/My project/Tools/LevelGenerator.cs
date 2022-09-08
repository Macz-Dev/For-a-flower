using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelTransform;
    [SerializeField] private string[,] levelTemplate;
    [SerializeField] private int matrixLevelSize;
    [SerializeField] private float blockDistanceOffset;
    [SerializeField] private float wallHeight;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private GameObject exitPointPrefab;
    [SerializeField] private GameObject holePrefab;
    private string segmentType;
    private Vector3 segmentPosition;

    // Segment groups
    string[] segmentsWithFloor = { "floor", "exit", "exit-ewt", "exit-ewb", "exit-ewr", "exit-ewl" };

    void Start()
    {
        levelTemplate = new string[,]{
            { "wall", "wall", "wall", "exit-ewt", "wall", "wall", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "exit", "floor", "floor", "wall" },
            { "wall", "floor", "floor", "floor", "floor", "floor", "wall" },
            { "wall", "wall", "wall", "wall", "wall", "wall", "wall" }};
        Generate();
    }
    void Generate()
    {
        for (int col = 0; col < matrixLevelSize; col++)
        {
            for (int row = 0; row < matrixLevelSize; row++)
            {
                this.segmentType = this.levelTemplate[col, row];
                this.segmentPosition.x = row;
                this.segmentPosition.y = 0f;
                this.segmentPosition.z = -col;
                GenerateSegment();
            }
        }
    }

    void GenerateSegment()
    {
        if (SegmentHasFloor())
        {
            GenerateSegmentWithFloor();
        }
        else
        {
            GenerateSegmentWithoutFloor();
        }
    }
    void GenerateSegmentWithFloor()
    {
        GameObject floor = GenerateBlock(levelTransform);

        if (this.segmentType == "floor")
        {
            floor.name = "Floor";
            return;
        }

        if (this.segmentType.Contains("exit"))
        {
            floor.name = "Exit";
            GenerateExitPoint(floor.transform);
        }

    }
    void GenerateSegmentWithoutFloor()
    {
        if (this.segmentType == "wall")
        {
            this.segmentPosition.y = 1f;
            GameObject baseWallBlock = GenerateBlock(levelTransform);
            baseWallBlock.name = "Wall";
            for (int yPosition = 2; yPosition < wallHeight; yPosition++)
            {
                this.segmentPosition.y = yPosition;
                GameObject wallBlock = GenerateBlock(baseWallBlock.transform);
                wallBlock.name = "WallBlock";
            }
        }
        else if (this.segmentType == "hole")
        {
            GenerateHole();
        }
    }

    // Function to generate blocks
    GameObject GenerateBlock(Transform parent)
    {
        // Adding offset
        Vector3 onPosition = this.segmentPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Instantiating the block
        GameObject newBlock = Instantiate(this.blockPrefabs[UnityEngine.Random.Range(0, this.blockPrefabs.Length)], onPosition, Quaternion.Euler(-90f, 0f, 0f), parent);
        return newBlock;
    }

    // Function to generate exitPoint
    void GenerateExitPoint(Transform parent)
    {
        this.segmentPosition.y = 1;
        // Adding offset
        Vector3 onPosition = this.segmentPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Instantiating the exitPoint
        GameObject exitPoint = Instantiate(this.exitPointPrefab, onPosition, Quaternion.identity, parent);
        // Add extra wall if is no just exit segment
        if (this.segmentType != "exit")
        {
            AddExtraWall(parent);
        }
    }

    void GenerateHole()
    {
        // Adding offset
        Vector3 onPosition = this.segmentPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Instantiating the hole
        GameObject newBlock = Instantiate(this.holePrefab, onPosition, Quaternion.Euler(-90f, 0f, 0f), this.levelTransform);
    }

    void AddExtraWall(Transform parent)
    {
        DefineWallPosition();
        for (int yPosition = 1; yPosition < wallHeight; yPosition++)
        {
            this.segmentPosition.y = yPosition;
            GameObject wallBlock = GenerateBlock(parent);
            wallBlock.name = "WallBlock";
        }
    }
    void DefineWallPosition()
    {
        if (this.segmentType.Contains("ewt"))
        {
            this.segmentPosition.z += 1;
        }
        else if (this.segmentType.Contains("ewb"))
        {
            this.segmentPosition.z -= 1;
        }
        else if (this.segmentType.Contains("ewr"))
        {
            this.segmentPosition.x += 1;
        }
        else if (this.segmentType.Contains("ewl"))
        {
            this.segmentPosition.x += 1;
        }
    }

    bool SegmentHasFloor()
    {
        return Array.Exists(this.segmentsWithFloor, element => element == this.segmentType);
    }
}