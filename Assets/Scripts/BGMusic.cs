using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusic : MonoBehaviour
{
    public AudioSource bgMusic;

    public void Start()
    {
        bgMusic.volume = PlayerPrefs.GetFloat("IGMusic");

        if(bgMusic.volume == 1)
        {
            bgMusic.volume = 0.3f;
        }
    }
}
