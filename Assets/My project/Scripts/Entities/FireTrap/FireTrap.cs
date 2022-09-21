using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireTrap : MonoBehaviour
{
    public FireTrapType type;
    public int duration;
    public int initialRemainingTicks;
    public int currentTick;
    public bool isAlwaysOn;
    private FireFireTrap fire;
    private ParticleSystem[] fireParticleSystems;
    public IndicatorFireTrap[] indicators;

    void Awake()
    {
        GetIndicators();

    }
    void Start()
    {
        SetFire();
        Initialize();
        LevelManager.Instance.NextTick += NextTick;
    }

    public void Initialize()
    {
        EnableFire();
        if (this.duration == 0) this.isAlwaysOn = true;
        // Hide All indicators
        foreach (var indicator in this.indicators)
        {
            indicator.Disable();
        }
        // No indicators will be modified
        if (this.isAlwaysOn)
        {
            return;
        }
        // Set initial count
        this.currentTick = this.duration - this.initialRemainingTicks;
        // Set count before enable
        for (int i = 0; i < duration; i++)
        {
            if (i < this.currentTick)
            {
                this.indicators[i].Off();
            }
            else
            {
                this.indicators[i].On();
            }
        }
    }

    void NextTick(object sender, EventArgs e)
    {
        // Do nothing if is always on
        if (this.isAlwaysOn) return;
        // Update currentTick
        this.currentTick += 1;
        // Validate current tick
        if (IsNecessaryResetCurrentTick())
        {
            this.currentTick = 1;
        }
        // Change state of current indicator
        if (IsFireOn())
        {
            this.indicators[this.currentTick - 1].Off();
        }
        else
        {
            this.indicators[this.currentTick - 1].On();
        }
        // Change fireState if is necessary
        if (IsNecessaryToggleFireState())
        {
            ToggleFireState();
        }
    }

    void GetIndicators()
    {
        this.indicators = GetComponentsInChildren<IndicatorFireTrap>();
    }

    void SetFire()
    {
        FireFireTrap[] fires = GetComponentsInChildren<FireFireTrap>(true); // 0 = Low ; 1: Upper
        // Disable fires
        foreach (var fire in fires)
        {
            fire.Disable();
        }
        // Set the fire that will be usable
        if (this.type == FireTrapType.LOW)
        {
            this.fire = fires[0];
        }
        else
        {
            this.fire = fires[1];
        }
    }

    void SetFireParticleSystems()
    {
        this.fireParticleSystems = this.fire.GetComponentsInChildren<ParticleSystem>();
        foreach (var fireParticleSystem in this.fireParticleSystems)
        {
            var mainFireParticleSystem = fireParticleSystem.main;
            mainFireParticleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
        }
    }

    void ToggleFireState()
    {
        if (IsFireOn()) DisableFire();
        else EnableFire();
    }

    void EnableFire()
    {
        this.fire.Enable();
    }

    void DisableFire()
    {
        this.fire.Disable();
    }

    //Validations
    bool IsNecessaryResetCurrentTick()
    {
        return this.currentTick > this.duration;
    }

    bool IsNecessaryToggleFireState()
    {
        return this.currentTick == this.duration;
    }

    bool IsFireOn()
    {
        return this.fire.gameObject.activeInHierarchy;
    }

}

public enum FireTrapType
{
    UPPER,
    LOW
}