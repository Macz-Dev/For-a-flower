using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform[] positions;
    private int currentPosition;
    private int positionsQuantity;

    void Start()
    {
        this.mainCamera = GetComponent<Camera>();
        this.positionsQuantity = this.positions.Length;
        this.currentPosition = 0;
        ChangePosition();
    }

    void ChangePosition()
    {
        if (this.currentPosition < this.positionsQuantity)
        {
            this.mainCamera.transform.rotation = positions[this.currentPosition].rotation;
            this.mainCamera.transform.position = positions[this.currentPosition].position;
        }
    }

    public void NextCameraPosition()
    {
        if (this.positionsQuantity > 0)
        {
            int nextPosition = this.currentPosition + 1;
            this.currentPosition = nextPosition < this.positionsQuantity ? nextPosition : 0;
            ChangePosition();
        }
    }
}
