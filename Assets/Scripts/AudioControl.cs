using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    private static readonly string FirstTime = "FirstTime";
    private int firstTimeInt;
    private float bgFloat;

    public static AudioControl instance;
    [SerializeField] public Slider bgSlider;

    public AudioSource bgAudio;
    
    void Start()
    {
        firstTimeInt = PlayerPrefs.GetInt(FirstTime);

        if(firstTimeInt == 0)
        {
            PlayerPrefs.SetFloat("BGVolume", 0.75f);
            PlayerPrefs.SetInt(FirstTime,  -1);
            LoadSettings();
        }
        else
        {
            LoadSettings();
        }
    }

    private void OnSceneLoaded()
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
        bgSlider.value = bgFloat;
        bgAudio.volume = bgFloat;
    }    

    public void ChangeVolume()
    {
        bgAudio.volume = bgSlider.value;
        Save();
    }

    public void Save()
    {
            PlayerPrefs.SetFloat("BGVolume", bgSlider.value);
    }
}
