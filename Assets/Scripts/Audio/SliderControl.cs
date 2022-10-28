using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderControl : MonoBehaviour
{
    private static readonly string FirstTime = "FirstTime";
    private int firstTimeInt;

    [SerializeField] public Slider bgSlider;
    // [SerializeField] public Slider sfxSlider;

    public AudioSource bgAudio;
    // public AudioSource[] sfxAudio;
    
    void Start()
        {
            firstTimeInt = PlayerPrefs.GetInt(FirstTime);

            if(firstTimeInt == 0)
            {
                PlayerPrefs.SetFloat("BGVolume", 1);
                // PlayerPrefs.SetFloat("SFXVolume", 1);
                PlayerPrefs.SetInt(FirstTime,  -1);
                Load();
            }
            else
            {
                Load();
            }
        }

    public void ChangeVolume()
        {
            bgAudio.volume = bgSlider.value;

            // for( int i = 0; i < sfxAudio.Length; i++)
            // {
            //     sfxAudio[i].volume = sfxSlider.value;
            // }

            Save();
        }

        private void Load(){
            bgSlider.value = PlayerPrefs.GetFloat("BGVolume");
            // sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        }

        public void Save(){
            PlayerPrefs.SetFloat("BGVolume", bgSlider.value);
            // PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        }
}
