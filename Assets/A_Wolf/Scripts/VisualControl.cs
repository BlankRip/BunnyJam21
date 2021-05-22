using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class VisualControl : MonoBehaviour
{
    public static VisualControl instance;

    [Range(0, 10)] public int lightSteps;
    [Range(0, 1)] public float effectState;
    [Range(0, 10)] public float soundRadius;
    [Range(0, 1)] public float soundStrength;
    [Range(0, 1)] public float sounSpeed;
    public Texture2D matrixTexture;
    public Transform soundPoint;
    public Color waveColor;

    private void Awake() {
        if(instance == null)
            instance = this;
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
