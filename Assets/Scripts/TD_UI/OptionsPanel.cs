using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    protected void Start()
    {
        mixer.GetFloat("MasterVolume", out float masterVolume);
        masterVolumeSlider.value = (masterVolume + 80) / 80f;
        mixer.GetFloat("EffectsVolume", out float effectVolume);
        effectsVolumeSlider.value = (effectVolume + 80) / 80f;
        mixer.GetFloat("MusicVolume", out float musicVolume);
        musicVolumeSlider.value = (musicVolume + 80) / 80f;
    }

    public void ChangeMasterVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", -(80 - 80 * volume));
    }

    public void ChangeEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectsVolume", -(80 - 80 * volume));
    }

    public void ChangeMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", -(80 - 80 * volume));
    }
}
