using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnlocker : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip step;

    public void Unlock()
    {
        player.UnlockMovement();
    }
    public void LightStep()
    {
        audioSource.volume = 0.125f;
        audioSource.pitch = Random.Range(2.5f, 3f);
        audioSource.PlayOneShot(step);
    }
    public void MediumStep()
    {
        audioSource.volume = 0.1875f;
        audioSource.pitch = Random.Range(2f, 2.5f);
        audioSource.PlayOneShot(step);
    }
    public void HeavyStep()
    {
        audioSource.volume = 0.5f;
        audioSource.pitch = Random.Range(1f, 2f);
        audioSource.PlayOneShot(step);
    }
}