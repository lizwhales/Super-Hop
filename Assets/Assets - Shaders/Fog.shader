Shader "Assets - Shaders/Fog"
{
    Properties {
        _FogColour ("FogColour", Color) = (0.4, 0, 0.6, 0)
        _MaxDistance ("MaxDistance", Float) = 4
        _ObjectColour("ObjectColour", Color) = (0, 0, 0, 0)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _FogColour;
            fixed4 _ObjectColour;
            float _MaxDistance;
            sampler2D _MainTex;

            struct v2f {
                float4 pos : SV_POSITION;
                float4 colour : COLOR0;
                float dis : TEXCOORD0;
            };

            v2f vert (
                float4 color : COLOR,
                float4 vertex : POSITION // vertex position input
                )
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                o.dis = length(ObjSpaceViewDir(vertex)) * 2;
                o.colour = color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float distanceToObject = i.dis;

                float frac = clamp((_MaxDistance - distanceToObject) / (_MaxDistance), 0.0, 1.0);
                fixed4 objectFrac = frac * _ObjectColour;
                fixed4 fogFrac = (1.0 - frac) * _FogColour;

                fixed4 finalColour = objectFrac + fogFrac;

                return finalColour;
            }
            ENDCG
        }
    }
}