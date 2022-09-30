using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public Transform blockPosition;
    public GameObject[] blocks;
    public float spawnTime;
    public int matrixSize;
    public int height;
    public int quantityToInstantiate;
    public int quantityInstantiated;
    public float distanceOffset;
    public int xPosition;
    public int yPosition;
    public int zPosition;
    public int currentMatrixSize;
    public Stack<GameObject> instantiatedBlocks;

    void Start()
    {
        this.instantiatedBlocks = new Stack<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (this.instantiatedBlocks.Count > 0)
            {
                DeleteInstantiates();
            }
            StopCoroutine("InstantiateBlock");
            StartToInstantiate();
        }
    }

    public void StartToInstantiate()
    {
        // If configure matrix size override quantity to instantiate
        if (this.matrixSize > 0)
        {
            this.quantityToInstantiate = this.matrixSize * this.matrixSize * this.height;
        }
        // Restart values for square Algorithm
        this.quantityInstantiated = 0;
        this.currentMatrixSize = 0;
        this.xPosition = 0;
        this.zPosition = 0;
        this.yPosition = 0;
        // Start to instantiate
        StartCoroutine("InstantiateBlock");
    }

    public void DeleteInstantiates()
    {
        // Destroy all saved instances
        foreach (GameObject instantiatedBlock in this.instantiatedBlocks)
        {
            Destroy(instantiatedBlock);
        }
    }

    public IEnumerator InstantiateBlock()
    {
        // Validate if have been instantiated the required blocks
        if (this.quantityInstantiated < this.quantityToInstantiate)
        {
            // Creating and saving the instance
            GameObject newBlock = Instantiate(this.blocks[Random.Range(0, this.blocks.Length)], new Vector3(this.xPosition, this.yPosition, this.zPosition) * this.distanceOffset, Quaternion.identity);
            newBlock.name = "Level " + yPosition + " (" + xPosition + "," + zPosition + ")";
            if (yPosition == 0)
            {
                newBlock.transform.SetParent(blockPosition);
            }
            else
            {
                Transform floor = blockPosition.Find("Level 0" + " (" + xPosition + "," + zPosition + ")");
                newBlock.transform.SetParent(floor);
            }
            this.quantityInstantiated++;
            this.instantiatedBlocks.Push(newBlock);

            yield return new WaitForSeconds(this.spawnTime);

            CalculateNextPosition();
            // Instantiate another block
            StartCoroutine("InstantiateBlock");
        }
    }

    // Square algorithm
    public void CalculateNextPosition()
    {
        // Horizontal matrix is already
        if (this.xPosition == (this.matrixSize - 1) && this.zPosition == (this.matrixSize - 1))
        {
            this.currentMatrixSize = 0;
            this.xPosition = 0;
            this.zPosition = 0;
            this.yPosition += 1;
            return;
        }
        // The last block to form the square was placed
        if (this.xPosition == this.currentMatrixSize && this.zPosition == this.currentMatrixSize)
        {
            this.currentMatrixSize += 1;
            this.zPosition = 0;
            this.xPosition = this.currentMatrixSize;
            return;
        }
        // The z row is already filled
        if (this.xPosition == this.currentMatrixSize && this.zPosition + 1 == this.currentMatrixSize)
        {
            this.xPosition = 0;
            this.zPosition += 1;
            return;
        }
        // Filling the z row
        if (this.xPosition == this.currentMatrixSize)
        {
            this.zPosition += 1;
        }
        // Filling the x row
        else
        {
            this.xPosition += 1;
        }
    }
}
