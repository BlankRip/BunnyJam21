Shader "J_Shaders/PEShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaveStrength("Wave Strength Divider // Higher = Weak // Lower = Stronger >", Float) = 50
        // 10 & 15 good vals
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _WaveStrength;

            fixed4 frag(v2f i) : SV_Target
            {
                
                // just invert the colors

                //fixed4 col = tex2D(_MainTex, i.uv);
                //col.rgb = 1 - col.rgb;
                //col.r = 1;
                //return col;
                

                float2 uvsCentered = i.uv * 2 - 1;

                float radDis = length(uvsCentered);

                //clip(1-(radDis -0.001));

                float wave = cos((radDis - _Time.y * 1) * 10) * 0.5 + 0.5;
                wave *= 1-radDis;

                
                if (_WaveStrength > 0)
                {
                    fixed4 col = tex2D(_MainTex, i.uv + wave / _WaveStrength);//wave / 100);
                    return col;
                }
                else
                {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    return col;
                }

                //return float4(wave.xxx, 1);
                //return float4(radDis.xxx, 1);

                
            }
            ENDCG
        }
    }
}
