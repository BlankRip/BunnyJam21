using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VisualControl : MonoBehaviour
{

    [SerializeField] [Range(0, 10)] float soundRadius;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalFloat("waveSpread", soundRadius);
    }
}
