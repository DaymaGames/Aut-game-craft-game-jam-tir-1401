using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    public AudioMixer mainMixer;
    public Slider volumeSlider;

    private const string VOLUME_SAVE_NAME = "VolumeSave";

    private void Start()
    {
        SetSavedVolume();
    }

    #region Volume
    private void SetSavedVolume()
    {
        float volume = GetSavedVolume();
        volumeSlider.value = volume;
    }

    public void OnSliderValueChange(float value)
    {
        mainMixer.SetFloat("MainVolume", value);
    }

    private void OnDestroy()
    {
        SaveVolume();
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(VOLUME_SAVE_NAME, volumeSlider.value);
    }
    private float GetSavedVolume()
    {
        if (PlayerPrefs.HasKey(VOLUME_SAVE_NAME) == false)
            return 0;

        return PlayerPrefs.GetFloat(VOLUME_SAVE_NAME);
    }
    #endregion

    public void ButtonStartGame()
    {
        Debug.Log("<color=green>Starting game!</color>");
    }
    public void ButtonExitGame()
    {
        Debug.Log("<color=blue>Quiting</color>");
        Application.Quit();
    }
}
