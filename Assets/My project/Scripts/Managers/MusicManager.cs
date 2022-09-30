using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;
    public Toggle toggle;

    void Start()
    {
        toggle.onValueChanged.AddListener((value) => toggleVolume(value));
    }

    void toggleVolume(bool isActive)
    {
        if (isActive)
        {
            music.mute = false;
        }
        else
        {
            music.mute = true;
        }
    }
}
