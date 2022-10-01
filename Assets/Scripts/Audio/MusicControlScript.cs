using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlScript : MonoBehaviour
{
    public static MusicControlScript instance;
    private float bgFloat, sfxFloat;

    [SerializeField] Slider bgSlider;
    [SerializeField] Slider sfxSlider;
    
    public AudioSource bgAudio;
    public AudioSource[] sfxAudio;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        LoadSettings();
    }

    public void LoadSettings()
    {
        bgFloat = PlayerPrefs.GetFloat("BGVolume");
        sfxFloat = PlayerPrefs.GetFloat("SFXVolume");

        bgSlider.value = bgFloat;
        sfxSlider.value = sfxFloat;

        bgAudio.volume = bgFloat;
        for(int i = 0; i < sfxAudio.Length; i++){
            sfxAudio[i].volume = sfxFloat;
        }
    }    

   
}
