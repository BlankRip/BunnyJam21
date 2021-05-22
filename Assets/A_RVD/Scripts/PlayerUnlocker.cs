using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlocker : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] AudioClip step;
    [SerializeField] GameAudio gameAudio;

    public void Unlock()
    {
        player.UnlockMovement();
    }
    public void LightStep()
    {
        gameAudio.PlayPlayerSFxOneShot(step, MixerDataBase.instance.sfxLow, Random.Range(2.5f, 3f));
    }
    public void MediumStep()
    {
        gameAudio.PlayPlayerSFxOneShot(step, MixerDataBase.instance.sfxNormal, Random.Range(2f, 2.5f));
    }
    public void HeavyStep()
    {
        gameAudio.PlayPlayerSFxOneShot(step, MixerDataBase.instance.sfxLoud, Random.Range(1f, 2f));
    }
}