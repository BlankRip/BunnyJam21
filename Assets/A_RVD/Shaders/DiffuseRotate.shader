Shader "DiffuseRotate"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Main Texture", 2D) = "white" {}
        _Ramp("Ramp Texture", 2D) = "white" {}
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Range(0, 1024)) = 32
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1		
        _Bands("Bands", Range(0, 0.25)) = 0.15
        _Angle("Rotation angle", Range(-360, 360)) = 0
        _RotateSpeed("spinning speed", Range(-10, 10)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque"}
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
                float3 viewDir : TEXCOORD1;	
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Ramp;
            float4 _Ramp_ST;
            float4 _Color;
            float4 _AmbientColor;
            float4 _SpecularColor;
            float _Glossiness;		
            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;	
            float _Bands;	
            float _RotateSpeed;
            float _Angle;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);		
                o.viewDir = WorldSpaceViewDir(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                TRANSFER_SHADOW(o)
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //The interval of the uv value is (0, 1), so the center point is (0.5, 0.5)
                float center = float2(0.5, 0.5);
                //Move the uv coordinates to the center
                float2 uv = i.uv.xy - center;
                
                float3 normal = normalize(i.normal);
                float3 viewDir = normalize(i.viewDir);

                float NdotL = dot(_WorldSpaceLightPos0, normal);
                uv = float2(1 - (NdotL * _Bands + _Bands), _Bands);
                float4 bandSample = tex2D(_Ramp, uv);
                
                float shadow = SHADOW_ATTENUATION(i);
                float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);

                float4 light = lightIntensity * _LightColor0;
                
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);
                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;		

                float rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                float4 sample = tex2D(_MainTex, i.uv);
                //The entered _Angle value is set to -360-360 for easy understanding. The converted angle value is (-pi * 2)-(pi*2) interval
                float angle = _Angle * (3.14 * 2 / 360);
                angle = _RotateSpeed != 0?_RotateSpeed * _Time.y : angle;
                //Matrix rotation
                uv = float2(uv.x * cos(angle) - uv.y * sin(angle),
                uv.x * sin(angle) + uv.y * cos(angle));
                
                //Restore uv coordinates
                uv += float2(0.5, 0.5);

                return tex2D(_MainTex , uv) * (light + _AmbientColor + specular + rim) * _Color * sample * bandSample;
            }
            ENDCG
        }
        UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
