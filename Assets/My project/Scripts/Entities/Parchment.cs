using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Parchment : MonoBehaviour
{
    public int quantityPieces;

    void Start()
    {
        LevelManager.Instance.ResetLevel += ResetInitialValues;
    }

    void ResetInitialValues(object sender, EventArgs e)
    {
        this.gameObject.SetActive(true);
    }

    void OnDestroy()
    {
        LevelManager.Instance.ResetLevel -= ResetInitialValues;
    }
}
