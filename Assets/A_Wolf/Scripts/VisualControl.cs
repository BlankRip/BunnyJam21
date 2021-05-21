using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VisualControl : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] int lightSteps;
    [SerializeField] [Range(0, 1)] float effectState;
    [SerializeField] [Range(0, 10)] float soundRadius;
    [SerializeField] [Range(0, 1)] float soundStrength;
    [SerializeField] [Range(0, 1)] float sounSpeed;
    [SerializeField] Texture2D matrixTexture;
    [SerializeField] Transform soundPoint;
    [SerializeField] Color waveColor;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalInt("stepCount", lightSteps);
        Shader.SetGlobalFloat("timeShiftEffect", effectState);
        Shader.SetGlobalFloat("waveSpread", soundRadius);
        Shader.SetGlobalFloat("soundStrength", soundStrength);
        Shader.SetGlobalFloat("soundSpeed", sounSpeed);
        Shader.SetGlobalVector("waveOrigin", soundPoint.position);
        Shader.SetGlobalVector("waveColor", waveColor);
        Shader.SetGlobalTexture("matrixTex", matrixTexture);
    }
}
