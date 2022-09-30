using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance;
    public AudioMixer audioMixer;
    public AudioSource sounds;
    public AudioSource audioSourceParchment;
    public Toggle toggle;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        toggle.onValueChanged.AddListener((value) => toggleVolume(value));
    }

    void toggleVolume(bool isActive)
    {
        if (isActive)
        {
            audioMixer.SetFloat("SoundsVolume", 0f);
            sounds.mute = false;
        }
        else
        {
            audioMixer.SetFloat("SoundsVolume", -80f);
            sounds.mute = true;
        }
    }
}
