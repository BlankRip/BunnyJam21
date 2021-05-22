Shader "BBJ/Enemy"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _RimGlow ("Rim GLow", Color) = (1,1,1,1)
        _FlatEffect ("Flat Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _KillMark("KIll mark", range(0,1)) = 0.3
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert fullforwardshadows

        #pragma target 3.0

        uniform sampler2D matrixTex;
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
            float3 viewDir;
        };

        uniform fixed3 waveOrigin, waveColor;
        uniform fixed waveSpread = 10, soundStrength, soundSpeed, stepCount, timeShiftEffect, _KillMark;
        fixed3 wavePos, viewDir;

        fixed4 _Color, _FlatEffect, _RimGlow;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = saturate(floor(dot (s.Normal, lightDir) * atten * stepCount)/stepCount);
            half4 c;
            fixed3 soundDir = wavePos - waveOrigin;
            fixed rayLength = length(soundDir);
            fixed soundRing = saturate(min(waveSpread - rayLength, 1) * max(sin((rayLength - _Time.x * soundSpeed) * 10), 0));

            fixed3 normalLook = s.Albedo * _LightColor0.rgb * (NdotL * atten) + waveColor * soundRing * soundStrength;

            // c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten) + waveColor * soundRing * soundStrength;
            fixed bwValue = ((s.Albedo.r + s.Albedo.g + s.Albedo.b) / 3) , lightLvl = (_LightColor0.r + _LightColor0.g + _LightColor0.b)/3;
            c.rgb = lerp(normalLook, _FlatEffect, timeShiftEffect * 0.5);
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput  o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            fixed rimValue = saturate(dot (normalize(IN.viewDir), o.Normal));
            o.Emission = lerp(_KillMark * _RimGlow * step(rimValue, 0.5) + 0.2 * _RimGlow * saturate(1 - rimValue), 0, timeShiftEffect);
            wavePos = IN.worldPos;
            viewDir = IN.viewDir;
            // Metallic and smoothness come from slider variables
            //    o.Metallic = _Metallic;
            //   o.Smoothness = _Glossiness;
            //  o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
