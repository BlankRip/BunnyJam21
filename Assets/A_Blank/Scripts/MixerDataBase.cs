using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerDataBase : MonoBehaviour
{
    public static MixerDataBase instance;

    public AudioMixerGroup backNormal;
    public AudioMixerGroup backAdranel;

    public AudioMixerGroup sfxLow;
    public AudioMixerGroup sfxNormal;
    public AudioMixerGroup sfxLoud;

    private void Awake() {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
