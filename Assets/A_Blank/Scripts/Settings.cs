using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioSource effectSource;
    [SerializeField] AudioClip[] clickClips;
    [SerializeField] float lowerLimit = -50;

    private bool clicked;

    private void Start() {
        if(!PlayerPrefs.HasKey("MusicLevels"))
            PlayerPrefs.SetFloat("MusicLevels", 1);
        if(!PlayerPrefs.HasKey("SfxLevels"))
            PlayerPrefs.SetFloat("SfxLevels", 1);
        
        if(musicSlider != null)
            musicSlider.value = PlayerPrefs.GetFloat("MusicLevels");
        if(sfxSlider != null)
            sfxSlider.value = PlayerPrefs.GetFloat("SfxLevels");

        mixer.SetFloat("MusicVolume", Mathf.Lerp(lowerLimit, 0, PlayerPrefs.GetFloat("MusicLevels")));
        mixer.SetFloat("SfxVolume", Mathf.Lerp(lowerLimit, 0, PlayerPrefs.GetFloat("SfxLevels")));
        clicked = false;
    }

    private void Update() {
        if(clicked && Input.GetTouch(0).phase == TouchPhase.Ended) {
            effectSource.Stop();
            effectSource.clip = clickClips[Random.Range(0, clickClips.Length)];
            effectSource.Play();
            clicked = false;
        }
    }

    public void ShowSettings() {
        panel.SetActive(true);
    }

    public void HideSettings() {
        panel.SetActive(false);
    }

    public void SetMusicLevels() {
        PlayerPrefs.SetFloat("MusicLevels", musicSlider.value);
        mixer.SetFloat("MusicVolume", Mathf.Lerp(lowerLimit, 0, PlayerPrefs.GetFloat("MusicLevels")));
    }

    public void SetSfxLevels() {
        clicked = true;
        PlayerPrefs.SetFloat("SfxLevels", sfxSlider.value);
        mixer.SetFloat("SfxVolume", Mathf.Lerp(lowerLimit, 0, PlayerPrefs.GetFloat("SfxLevels")));
    }
}
