using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlide : MonoBehaviour
{
    public float sliderSetting;
    public float musicCheck;
    public Slider volumeSlider;
    public Toggle igVolumeToggle;

    void Start()
    {
        musicCheck = PlayerPrefs.GetFloat("IGMusic");

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }

        if (volumeSlider.value == 0)
        {
            AudioListener.volume = volumeSlider.value;
        }


        if (musicCheck == 1)
        {
            PlayerPrefs.SetFloat("IGMusic", 1);
            igVolumeToggle.isOn = true;
        }
        else
        {
            PlayerPrefs.SetFloat("IGMusic", 0);
            igVolumeToggle.isOn = false;
        }
    }

    public void VolumeChanger()
    {
        AudioListener.volume = volumeSlider.value;
        sliderSetting = AudioListener.volume;
        SaveVolume();
    }

    public void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    public void EnableIngameMusic()
    {
        if(igVolumeToggle.isOn == true)
        {
            PlayerPrefs.SetFloat("IGMusic", 1);
        }
        else
        {
            PlayerPrefs.SetFloat("IGMusic", 0);
        }
    } 
}
