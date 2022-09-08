using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gelem : MonoBehaviour
{
    GelemState currentState;
    Vector3 initialPosition;
    Quaternion initialRotation;
    float movementSpeed = 12.5f;
    float turningSpeed = 45f;
    public TurningDirection turningDirection;
    Animator animator;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private float rayDistance;

    void Awake()
    {
        this.animator = GetComponent<Animator>();
        this.initialPosition = this.transform.position;
        this.initialRotation = this.transform.rotation;
    }
    void Start()
    {
        this.currentState = GelemState.IDLE;
        LevelManager.Instance.ResetLevel += ResetInitialValues;
    }

    void ResetInitialValues(object sender, EventArgs e)
    {
        this.transform.position = initialPosition + Vector3.up * 5.0f;
        this.transform.rotation = this.initialRotation;
    }

    bool CanMoveForward()
    {
        ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
        Debug.DrawLine(ray.origin, ray.origin + (ray.direction * rayDistance), Color.green);
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("ExitPoint"))
            {
                return true;
            }
            return false;
        }
        else
        {
            return true;
        }
    }
    public void MoveForward()
    {
        if (CanMoveForward())
        {
            this.currentState = GelemState.MOVING;
            this.animator.SetTrigger("Walk");
            StartCoroutine("ReturnToIdle");
        }
        else
        {
            StartCoroutine("DoNothing");
        }
    }
    public void Turn()
    {
        this.currentState = GelemState.TURNING;
        if (this.turningDirection == TurningDirection.RIGHT)
        {
            this.animator.SetTrigger("TurnRight");
        }
        else
        {
            this.animator.SetTrigger("TurnLeft");
        }

        StartCoroutine("ReturnToIdle");
    }
    IEnumerator ReturnToIdle()
    {
        yield return new WaitForSeconds(2);
        this.animator.SetTrigger("ReturnToIdle");
        if (currentState == GelemState.FALLING)
        {
            ChangeState(GelemState.IDLE);
            LevelManager.Instance.ExecuteResetLevel();
        }
        else
        {
            ChangeState(GelemState.IDLE);
            LevelManager.Instance.ExecuteNextTick();
        }
    }

    IEnumerator DoNothing()
    {
        yield return new WaitForSeconds(2);
        LevelManager.Instance.ExecuteNextTick();
    }

    void MovingForward()
    {
        this.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
    }

    void Turning()
    {
        float direction = this.turningDirection == TurningDirection.LEFT ? -1.0f : 1.0f;
        this.transform.Rotate(Vector3.up * direction * turningSpeed * Time.deltaTime, Space.Self);
    }


    void Update()
    {
        if (this.currentState == GelemState.MOVING || this.currentState == GelemState.FALLING)
        {
            MovingForward();
        }

        if (this.currentState == GelemState.TURNING)
        {
            Turning();
        }

        if (this.currentState == GelemState.IDLE && Input.GetKeyDown(KeyCode.W))
        {
            MoveForward();
        }
        if (this.currentState == GelemState.IDLE && Input.GetKeyDown(KeyCode.A))
        {
            this.turningDirection = TurningDirection.LEFT;
            Turn();
        }
        if (this.currentState == GelemState.IDLE && Input.GetKeyDown(KeyCode.D))
        {
            this.turningDirection = TurningDirection.RIGHT;
            Turn();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LevelManager.Instance.ExecuteNextTick();
        }
    }

    void ChangeState(GelemState newState)
    {
        this.currentState = newState;
        Debug.Log("Change to " + newState);
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Hole"))
        {
            ChangeState(GelemState.FALLING);
            this.animator.SetTrigger("Fall");
        }
        else if (trigger.CompareTag("ExitPoint"))
        {
            LevelManager.Instance.wasExitPointReached = true;
        }
    }
}
public enum GelemState
{
    IDLE,
    FALLING,
    MOVING,
    TURNING
}

public enum TurningDirection
{
    LEFT,
    RIGHT
}
