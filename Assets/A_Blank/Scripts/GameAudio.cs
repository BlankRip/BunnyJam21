using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameAudio : MonoBehaviour
{
    public static GameAudio instance;
    [SerializeField] AudioSource sfxAudio;
    [SerializeField] AudioSource playerSfxAudio;
    
    private void Awake() {
        if(instance == null) {
            instance = this;
            sfxAudio.loop = false;
            playerSfxAudio.loop = false;
        }
    }

    public void PlaySFx(AudioClip clip, AudioMixerGroup group) {
        SwitchAudio(sfxAudio, clip, group);
    }

    public void PlaySFxOneShot(AudioClip clip, AudioMixerGroup group) {
        sfxAudio.outputAudioMixerGroup = group;
        sfxAudio.PlayOneShot(clip);
    }
    public void PlayPlayerSFxOneShot(AudioClip clip, AudioMixerGroup group, float pitch) {
        playerSfxAudio.outputAudioMixerGroup = group;
        playerSfxAudio.pitch = pitch;
        playerSfxAudio.PlayOneShot(clip);
    }

    private void SwitchAudio(AudioSource source, AudioClip clip, AudioMixerGroup group) {
        source.Stop();
        source.outputAudioMixerGroup = group;
        source.clip = clip;
        source.Play();
    }
}
