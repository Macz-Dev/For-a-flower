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
    [SerializeField] private float internalWallHeight;
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private Material floorMaterial;
    [SerializeField] private GameObject gelemPrefab;
    [SerializeField] private GameObject fireTrapPrefab;
    [SerializeField] private GameObject exitPointPrefab;
    [SerializeField] private GameObject holePrefab;
    private string elementType;
    private string rawElementType;
    private string elementModifier;
    private Vector3 elementPosition;
    private Quaternion elementDirection;

    // Element groups
    string[] elementsWithFloor = { "floor", "wall", "iWall", "gelem", "fire", "exit" };
    string[] directionModifiers = { "dtf", "dtb", "dtr", "dtl" };
    string[] extraWallsModifiers = { "ewf", "ewb", "ewr", "ewl" };

    void Start()
    {
        levelTemplate = GameData.Levels["3"].sceneryTemplate;
        Generate();
    }

    // Function that read the scenery template and call the generateElement function
    void Generate()
    {
        for (int col = 0; col < matrixLevelSize; col++)
        {
            for (int row = 0; row < matrixLevelSize; row++)
            {
                this.elementType = this.levelTemplate[col, row];
                string[] elementParts = this.elementType.Split("-");
                this.rawElementType = elementParts[0];
                this.elementModifier = elementParts.Length > 1 ? elementParts[1] : "";
                this.elementPosition.x = row;
                this.elementPosition.y = 0f;
                this.elementPosition.z = -col;
                this.elementDirection = Quaternion.identity;
                GenerateElement();
            }
        }
    }

    // Function where filter the element types (With floor and without floor)
    void GenerateElement()
    {
        if (ElementHasFloor())
        {
            GenerateElementWithFloor();
        }
        else
        {
            GenerateElementWithoutFloor();
        }
    }

    // Function to decide what type of element with floor will be generated
    void GenerateElementWithFloor()
    {
        GameObject floor = GenerateBlock(levelTransform);
        floor.GetComponent<MeshRenderer>().material = this.floorMaterial;

        if (this.rawElementType == "floor")
        {
            floor.name = "Floor";
            return;
        }
        else if (this.rawElementType == "wall")
        {
            floor.name = "Wall";
            GenerateWall(floor.transform);
            return;
        }
        else if (this.rawElementType == "iWall")
        {
            floor.name = "InternalWall";
            GenerateInternalWall(floor.transform);
            return;
        }
        else if (this.rawElementType == "gelem")
        {
            floor.name = "Spawn";
            SpawnGelem(levelTransform);
            return;
        }
        else if (this.rawElementType == "fire")
        {
            floor.name = "Trap";
            GenerateFireTrap(floor.transform);
            return;
        }
        else if (this.rawElementType == "exit")
        {
            floor.name = "Exit";
            GenerateExitPoint(floor.transform);
            return;
        }
    }

    // Function to decide what type of element without floor will be generated
    void GenerateElementWithoutFloor()
    {
        if (this.elementType == "hole")
        {
            GenerateHole();
        }
    }

    // Function to generate blocks
    GameObject GenerateBlock(Transform parent)
    {
        // Adding offset
        Vector3 onPosition = this.elementPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Adjusting direction
        Quaternion onDirection = Quaternion.Euler(-90f, 0f, 0f);
        // Instantiating the block
        GameObject newBlock = Instantiate(this.blockPrefabs[UnityEngine.Random.Range(0, this.blockPrefabs.Length)], onPosition, onDirection, parent);
        return newBlock;
    }

    // Function to generate a Wall
    void GenerateWall(Transform parent)
    {
        for (int yPosition = 1; yPosition < wallHeight; yPosition++)
        {
            this.elementPosition.y = yPosition;
            GameObject wallBlock = GenerateBlock(parent);
            wallBlock.name = "WallBlock";
        }
    }

    // Function to generate a internal Wall
    void GenerateInternalWall(Transform parent)
    {
        for (int yPosition = 1; yPosition < internalWallHeight; yPosition++)
        {
            this.elementPosition.y = yPosition;
            GameObject wallBlock = GenerateBlock(parent);
            wallBlock.name = "WallBlock";
        }
    }

    // Function to spawn gelem
    void SpawnGelem(Transform parent)
    {
        // Adding offset
        Vector3 onPosition = this.elementPosition * this.blockDistanceOffset;
        // Adjusting y position
        onPosition.y = 17.5f;
        if (ShouldModifyDirection())
        {
            ApplyDirectionModifier();
        }
        // Spawning Gelem
        GameObject gelem = Instantiate(this.gelemPrefab, onPosition, this.elementDirection, parent);
        // Assigning gelem
        LevelManager.Instance.instructionsExecutor.gelem = gelem.GetComponent<Gelem>();
    }

    // Function to generate FireTrap
    void GenerateFireTrap(Transform parent)
    {
        // Adding offset
        Vector3 onPosition = this.elementPosition * this.blockDistanceOffset;
        // Adjusting y position
        onPosition.y = 25f;
        //Index to control another modifiers
        int typeOfFireIndex = 2;
        if (ShouldModifyDirection())
        {
            ApplyDirectionModifier();
        }
        else
        {
            typeOfFireIndex = 1;
        }
        FireTrap fireTrap = fireTrapPrefab.GetComponent<FireTrap>();
        // Get another parts
        string[] elementParts = this.elementType.Split("-");
        // Setting the fireTrap type
        fireTrap.type = elementParts[typeOfFireIndex] == "l" ? FireTrapType.LOW : FireTrapType.UPPER;
        // Setting duration
        fireTrap.duration = (elementParts.Length > typeOfFireIndex + 1) ? int.Parse(elementParts[typeOfFireIndex + 1]) : 0;
        // Setting initial tick
        fireTrap.initialRemainingTicks = (elementParts.Length > typeOfFireIndex + 2) ? int.Parse(elementParts[typeOfFireIndex + 2]) : fireTrap.duration;
        // Generate fireTrap
        Instantiate(this.fireTrapPrefab, onPosition, this.elementDirection, parent);

    }

    // Function to generate exitPoint
    void GenerateExitPoint(Transform parent)
    {
        this.elementPosition.y = 1;
        // Adding offset
        Vector3 onPosition = this.elementPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Instantiating the exitPoint
        GameObject exitPoint = Instantiate(this.exitPointPrefab, onPosition, Quaternion.identity, parent);
        // Add extra wall if is no just exit element
        if (ShouldAddExtraWall())
        {
            AddExtraWall(parent);
        }
    }

    // Function to generate exitPoint
    void GenerateHole()
    {
        // Adding offset
        Vector3 onPosition = this.elementPosition * this.blockDistanceOffset;
        // Adjusting y offset
        onPosition.y /= 2;
        // Instantiating the hole
        GameObject newBlock = Instantiate(this.holePrefab, onPosition, Quaternion.Euler(-90f, 0f, 0f), this.levelTransform);
    }

    // MODIFIERS
    void ApplyDirectionModifier()
    {
        if (this.elementType.Contains("dtl"))
        {
            this.elementDirection = Quaternion.Euler(0f, -90f, 0f);
        }
        else if (this.elementType.Contains("dtr"))
        {
            this.elementDirection = Quaternion.Euler(0f, 90f, 0f);
        }
        else // For "ewb"
        {
            this.elementDirection = Quaternion.Euler(0f, 180f, 0f);
        }
    }

    void AddExtraWall(Transform parent)
    {
        DefineExtraWallPosition();
        GenerateWall(parent);
    }
    void DefineExtraWallPosition()
    {
        if (this.elementType.Contains("ewf"))
        {
            this.elementPosition.z += 1;
        }
        else if (this.elementType.Contains("ewb"))
        {
            this.elementPosition.z -= 1;
        }
        else if (this.elementType.Contains("ewr"))
        {
            this.elementPosition.x += 1;
        }
        else if (this.elementType.Contains("ewl"))
        {
            this.elementPosition.x += 1;
        }
    }

    // Validations
    bool ElementHasFloor()
    {
        return Array.Exists(this.elementsWithFloor, element => element == this.rawElementType);
    }

    bool ShouldModifyDirection()
    {
        return Array.Exists(this.directionModifiers, element => element == this.elementModifier);
    }

    bool ShouldAddExtraWall()
    {
        return Array.Exists(this.extraWallsModifiers, element => element == this.elementModifier);
    }
}