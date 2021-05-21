using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSForcer : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_ANDROID
        Application.targetFrameRate = 120;
    }