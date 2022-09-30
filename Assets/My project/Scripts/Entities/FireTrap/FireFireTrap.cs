using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFireTrap : MonoBehaviour
{
    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
