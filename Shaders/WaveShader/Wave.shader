Shader "Unlit/NewUnlitShader"
{
    Properties {
        _ColorA("ColorA", Color ) = (1,1,1,1)
        _ColorB("ColorB", Color ) = (1,1,1,1)
        _ColorStart("ColorStart", Range(0,1)) = 1
        _ColorEnd("ColorEnd ", Range(0,1)) = 0
    }

    SubShader {

        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass {
            
            ZWrite Off
            Cull Off
            // Blend One One
            // Blend DstColor Zero
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #define TAU 6.28318

            float4 _ColorA;
            float4 _ColorB;
            float _ColorStart;
            float _ColorEnd;

            struct appdata { // per vertex
                float4 vertex : POSITION; //vertex position
                float3 normals : NORMAL;
                // float4 color : COLOR;
                // float4 tangeant : TANGEANT;
                float2 uv : TEXCOORD0; // uv coordinates
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normals);
                o.uv = v.uv;

                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float InverseLerp(float start, float end, float  v)
            { 
                return (v - start)/(end-start);
            }
            
            float4 frag (v2f i) : SV_Target {
                float yOffset = cos(i.uv.x  * TAU * 8 ) * 0.01;
                float t = cos((i.uv.y + yOffset - _Time.y * .1) * TAU * 5) * .5 + .5;
                t *= 1-i.uv.y;

                float topBottomRemover = (abs(i.normal.y) < .999);
                float4 waves = t * topBottomRemover; 
                float4 gradient = lerp(_ColorA, _ColorB, i.uv.y);

                return gradient * waves;
            }
            ENDCG
        }
    }
}
