using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public AudioMixer audioMixer;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        print(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void Update()
    {
        BackWithEscape();
    }

    private void BackWithEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
