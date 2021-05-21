using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudio : MonoBehaviour
{
    public static GameAudio instance;

    [SerializeField] AudioSource backGroundAudio;
    [SerializeField] AudioSource sfxAudio;
    
    private void Awake() {
        if(instance = null) {
            instance = this;
            backGroundAudio.loop = true;
            sfxAudio.loop = false;
        }
    }

    public void ChangeBackground(AudioClip clip, AudioMixerGroup group) {
        SwitchAudio(backGroundAudio, clip, group);
    }

    public void PlaySFx(AudioClip clip, AudioMixerGroup group) {
        SwitchAudio(sfxAudio, clip, group);
    }

    public void PlaySFxOneShot(AudioClip clip, AudioMixerGroup group) {
        sfxAudio.outputAudioMixerGroup = group;
        sfxAudio.PlayOneShot(clip);
    }

    private void SwitchAudio(AudioSource source, AudioClip clip, AudioMixerGroup group) {
        source.Stop();
        source.outputAudioMixerGroup = group;
        source.clip = clip;
        source.Play();
    }
}