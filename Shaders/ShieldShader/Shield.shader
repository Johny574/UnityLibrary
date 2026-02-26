Shader "Unlit/Shield"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _Intensity("_Intensity", float) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            Blend One One
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float _Intensity;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 normals   : TEXCOORD1;
                float3 worldPos : TEXCOORD2;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normals = UnityObjectToWorldNormal(v.normals);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float angle = _Time.y / 2; 
                float cosA = cos(angle);
                float sinA = sin(angle);
                float2 rotatedUv = i.uv;
                rotatedUv.x += cosA;
                
                fixed4 col = tex2D(_MainTex, rotatedUv);    

                float4 shield = (col * _Color * _Intensity) + _Color;
                return shield;
            }
            ENDCG
        }
    }
}
