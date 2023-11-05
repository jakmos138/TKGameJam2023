using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject mainMenu;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider master;
    
    private void Start()
    {
        master.value = PlayerPrefs.GetFloat("Master", 0);
        Time.timeScale = 1;
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Master", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume)*20);
    }



    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

   
}
