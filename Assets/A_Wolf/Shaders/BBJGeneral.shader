Shader "BBJ/General"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf SimpleLambert fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };


        uniform fixed3 waveOrigin;
        uniform fixed waveSpread = 10;
        fixed3 wavePos;

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
            half NdotL = saturate(dot (s.Normal, lightDir));
            half4 c;

            fixed soundDir = wavePos - waveOrigin;
            fixed rayLength = length(wavePos - waveOrigin);
            fixed soundRing = saturate(min(waveSpread - rayLength, 1) * max(sin((rayLength - _Time.x) * 10), 0));

            c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten) + fixed3(0.2,0.3,0) * soundRing;
            c.a = s.Alpha;
            return c;
        }

        void surf (Input IN, inout SurfaceOutput  o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            wavePos = IN.worldPos;
            // Metallic and smoothness come from slider variables
            //    o.Metallic = _Metallic;
            //   o.Smoothness = _Glossiness;
            //  o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
