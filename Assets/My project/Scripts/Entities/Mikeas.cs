using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mikeas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Fire"))
        {
            LevelManager.Instance.instructionsExecutor.gelem.ChangeState(GelemState.MIKEAS_BURNED);
            Debug.Log("Burned");
        }
    }
}
