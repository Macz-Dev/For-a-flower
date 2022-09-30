using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorFireTrap : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    public Material disableMaterial;
    public Material onMaterial;
    public Material offMaterial;

    void Awake()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Disable()
    {
        this.meshRenderer.material = this.disableMaterial;
    }

    public void On()
    {
        this.meshRenderer.material = this.onMaterial;
    }

    public void Off()
    {
        this.meshRenderer.material = this.offMaterial;
    }
}
