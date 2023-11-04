using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ResolutionManager : MonoBehaviour
{
    
    [SerializeField] TMPro.TMP_Dropdown resolutionDropdown;
    

    Resolution[] resulutions;



    private void Start()
    {
        
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resulutions.Length; i++)
        {
            options.Add(resulutions[i].width + "x" + resulutions[i].height);

            if (resulutions[i].width == Screen.currentResolution.width && resulutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", currentResolutionIndex);
        resolutionDropdown.RefreshShownValue();
       
    }


    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(resulutions[resolutionIndex].width, resulutions[resolutionIndex].height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }
}
