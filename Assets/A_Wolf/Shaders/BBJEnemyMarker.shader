Shader "BBJ/EnemyMarker"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", color) = (1,1,1,1)
    }
    SubShader
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

        ZWrite Off
        Blend One One
        
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
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST, _Color;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv.xy;

                // billboard mesh towards camera

                

                float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
                float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
                float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
                float4 outPos = mul(UNITY_MATRIX_P, viewPos);

                o.pos = mul(UNITY_MATRIX_P, 
                mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0))
                + float4(v.vertex.x, v.vertex.y, 0.0, 0.0)
                * float4(1, 1, 1.0, 1.0));
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;
                return col;
            }
            ENDCG
        }
    }
}