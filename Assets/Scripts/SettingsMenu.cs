using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMPro.TMP_Dropdown resolutionDropdown;        // "TMPro.TMP_Dropdown" is so I can use the text mesh pro dropdown menu instead of regular dropdown.

    Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();   // ensures that resolutions menu has a clean slate so that the values can be changed by unity engine.

        List<string> options = new List<string>();   // this creates a list of the different resolutions

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)  //loop through each element in the resolutions array
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;  // creates a formatted string that displays the resolution
            options.Add(option);                        // add the formatted string to the list

            if (resolutions[i].width == Screen.width &&  //changed from "resolutions[i].width == Screen.currentResolution.width" that brackeys showed to what a commentor said
                resolutions[i].height == Screen.height) // changed from "resolutions[i].height == Screen.currentResolution.height)" :--:
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);   // and finally adds the "options" list to the resolutions dropdown 
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
