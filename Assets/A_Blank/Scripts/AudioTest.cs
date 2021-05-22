using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField] AudioSource testSource;
    [SerializeField] AudioClip testClip;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.F)) {
            testSource.Stop();
            testSource.clip = testClip;
            testSource.Play();
        }
    }
}
