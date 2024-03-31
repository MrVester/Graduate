using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioSource audioSrc;

    public Toggle toggleVolume;
    public Slider slider;
    public TextMeshProUGUI soundValue;

    void Start()
    {
        JSONSave.Start(JSONSaveConfig.GetConfig());
        toggleVolume.onValueChanged.AddListener((value) => SetVolumeZero());
        slider.onValueChanged.AddListener((value) => SetVolume());
        //audioSrc = GetComponent<AudioSource>();
        if (JSONSave.HasKey("SaveVolume"))
        {
            slider.value = audioSrc.volume = JSONSave.GetFloat("SaveVolume");
        }
        else
        {
            slider.value = audioSrc.volume = 0.2f;
        }

    }





    void Update()
    {

        soundValue.text = Mathf.Round(f: slider.value * 100) + "%";
    }

    public void SetVolume()
    {
        audioSrc.volume = slider.value;
        JSONSave.SetFloat("SaveVolume", slider.value);
        if (slider.value > 0 && !toggleVolume.isOn)
        {
            //JSONSave.SetFloat("VolumeBuf", 0f);
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





    /*[Header("Components")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private Slider slider;
    [SerializeField] private Text text;

    [Header("Keys")]
    [SerializeField] private string saveVolumeKey;

    [Header("Tags")]
    [SerializeField] private string sliderTag;
    [SerializeField] private string textVolumeTag;

    [Header("Parameters")]
    [SerializeField] public float volume;

    private void Awake()
    {
        if(JSONSave.HasKey(this.saveVolumeKey))
        {
            this.volume = JSONSave.GetFloat(this.saveVolumeKey);
            this.audio.volume = this.volume;

            GameObject sliderObj = GameObject.FindWithTag(this.sliderTag);
            if(sliderObj != null)
            {
                this.slider = sliderObj.GetComponent<Slider>();
                this.slider.volume = this.volume;
            }
        }
        else
        {
            this.volume = 0.5f;
            JSONSave.SetFloat(this.saveVolumeKey, this.volume);
            this.audio.volume = this.volume;
        }
    }

private void LateUpdate()
    {
        GameObject sliderObj = GameObject.FindWithTag(this.sliderTag);
        if(sliderObj != null)
        {
            this.slider = sliderObj.GetComponent<slider>();
            this.volume = slider.value;

            if(this.audio.volume != this.volume)
            {
                JSONSave.SetFloat(this.saveVolumeKey, this.volume); 
            }

            GameObject textObj = GameObject.FindWithTag(this.textVolumeTag);
            if(textObj != null)
            {
                this.text = textObj.GetComponent<Text>();

                this.text.text = Mathf.Round(f: this.volume * 100) + "%";
            }
        }

        this.audio.volume = this.volume;
    }*/
}
