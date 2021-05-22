Shader "BBJ/General"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
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


        uniform fixed3 waveOrigin, waveColor, shadowColor;
        uniform fixed waveSpread = 10, soundStrength, soundSpeed, stepCount, timeShiftEffect;
        fixed3 wavePos, viewDir;

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = saturate(floor(dot (s.Normal, lightDir) * stepCount)/stepCount);
            half4 c;
            fixed3 soundDir = wavePos - waveOrigin;
            fixed rayLength = length(soundDir);
            fixed soundRing = saturate(min(waveSpread - rayLength, 1) * max(sin((rayLength - _Time.x * soundSpeed) * 10), 0));

            fixed3 normalLook = s.Albedo * _LightColor0.rgb * (NdotL * lerp(shadowColor, 1, atten)) + waveColor * soundRing * soundStrength;

            // c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten) + waveColor * soundRing * soundStrength;
            fixed bwValue = ((s.Albedo.r + s.Albedo.g + s.Albedo.b) / 3) , lightLvl = max(dot (s.Normal, lightDir), 0) * (_LightColor0.r + _LightColor0.g + _LightColor0.b)/3;
            fixed scrollSpeed = 80;
            fixed4 timeStop = 0;
            timeStop.xyz = (bwValue * lightLvl + fixed3(0.3,0.3,0.3) * max(pow(saturate(
            tex2Dlod(matrixTex, fixed4(wavePos.x * 10 + _Time.y * scrollSpeed, wavePos.z * 10 - _Time.y * scrollSpeed,1,1) * 0.01).r *
            tex2Dlod(matrixTex, fixed4(wavePos.x * 10 - _Time.y * scrollSpeed, wavePos.z * 10 + _Time.y * scrollSpeed,1,1) * 0.005).r * 2
            ), 2), 0.3)) * atten;
            //  fixed4(0.1,0,0,0) * step(tex2Dlod(matrixTex, fixed4( ((atan2(soundDir.x, soundDir.z)/ 3.14) * 0.5 + 0.5) * 20, pow(range,2) - _Time.z, 0,0) * 0.5), 0.5));
            c.rgb = lerp(normalLook, timeStop, saturate(timeShiftEffect));
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput  o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
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
