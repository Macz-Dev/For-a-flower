using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Mikeas : MonoBehaviour
{
    public Material material;

    private Color initialColor = new Color(990566f, 0.8199199f, 0.6681648f);
    public Vector3 initialPosition;

    void Start()
    {
        this.initialPosition = this.transform.position;
        ResetInitialValues();
        LevelManager.Instance.ResetLevel += ResetInitialValues;
    }

    void ResetInitialValues()
    {
        this.material.color = this.initialColor;
        this.transform.position = initialPosition;
    }

    void ResetInitialValues(object sender, EventArgs e)
    {
        ResetInitialValues();
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Fire"))
        {
            LevelManager.Instance.instructionsExecutor.gelem.ChangeState(GelemState.MIKEAS_BURNED);
            this.material.color = Color.black;
        }
    }

    void OnDestroy()
    {
        LevelManager.Instance.ResetLevel -= ResetInitialValues;
    }
}
