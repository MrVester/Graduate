using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private AudioSource audioSrc;

    void Start()
    {
        JSONSave.Start(JSONSaveConfig.GetConfig());
        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = JSONSave.GetFloat("SaveVolume");
    }

}
