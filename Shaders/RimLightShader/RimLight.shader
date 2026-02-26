Shader "Unlit/RimLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color ) = (1,1,1,1)
        _GlowColor("Glow Color", Color) = (0,1,1,1)
        _GlowPower("Glow Sharpness", Range(1,8)) = 3
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {   
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _Color;
            float4 _GlowColor;
            float _GlowPower;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normals : NORMAL;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normals   : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex   = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normals   = UnityObjectToWorldNormal(v.normals);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float rim = 1.0 - saturate(dot(normalize(i.normals), viewDir));
                float c = _GlowPower + cos(_GlowPower * _Time.y);
                rim = pow(rim, c);
                return _Color + _GlowColor * rim;
            }
            ENDCG
        }
    }
}
