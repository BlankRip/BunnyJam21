using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour
{
    [SerializeField] AudioClip punch;
    public void Impact()
    {
        GameAudio.instance.PlaySFxOneShot(punch, MixerDataBase.instance.sfxLow);
    }
}
