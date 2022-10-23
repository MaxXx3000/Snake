using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioSource background;
    //public float backgroundVolume = 1f;
        
    void Start()
    {
        background = GetComponent<AudioSource>();
    }

    void Update()
    {
        background.volume = backgroundVolume;
    }

    public void SetVol(float vol)
    {        
        backgroundVolume = Mathf.RoundToInt(vol); ;

        //backVol = Mathf.RoundToInt(vol * 10);
    }

    public int backgroundVolume
    {
        get => PlayerPrefs.GetInt(backgroundVolumeKey, 0);
        private set
        {
            PlayerPrefs.SetInt(backgroundVolumeKey, value);
            PlayerPrefs.Save();
        }
    }
    public const string backgroundVolumeKey = "backgroundVolume";
}
