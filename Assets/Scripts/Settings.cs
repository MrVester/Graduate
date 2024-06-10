using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
   [SerializeField] private AudioSource audioSrc;
   [SerializeField] private Toggle toggleVolume;
   [SerializeField] private Slider slider;
   [SerializeField] private TextMeshProUGUI soundValue;

    void Start()
    {
        toggleVolume.onValueChanged.AddListener((value) => SetVolumeZero());
        slider.onValueChanged.AddListener((value) => SetVolume());
        if (JSONSave.HasKey("SaveVolume"))
        {
            slider.value = audioSrc.volume = JSONSave.GetFloat("SaveVolume");
        }
        else
        {
            slider.value = audioSrc.volume = 0.2f;
        }
        SetVolumeText();

    }

    public void SetVolume()
    {
        SetVolumeText();
        audioSrc.volume = slider.value;
        JSONSave.SetFloat("SaveVolume", slider.value);
        if (slider.value > 0 && !toggleVolume.isOn)
        {
            toggleVolume.isOn = true;

        }
        if (slider.value == 0 && toggleVolume.isOn)
        {
            toggleVolume.isOn = false;
        }
        
    }
    public void SetVolumeZero()
    {
        if (slider.value > 0 && !toggleVolume.isOn)
        {
            JSONSave.SetFloat("VolumeBuf", slider.value);
            JSONSave.SetFloat("SaveVolume", 0f);
            audioSrc.volume = slider.value = 0f;
        }

        if (slider.value == 0 && toggleVolume.isOn)
        {
            audioSrc.volume = slider.value = JSONSave.GetFloat("VolumeBuf");

        }
    }
    private void SetVolumeText()
    {
        soundValue.text = Mathf.Round(f: slider.value * 100) + "%";
    }
}
