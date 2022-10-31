Shader "Glow/CmdDepthShader"
{
    // Used in the GlowObj script with the GlowTargetMat
    Properties
    {
        _GlowColor ("Glow Color", Color) = (1,0,0,1)
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
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
                float linDepth : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = ComputeScreenPos(o.vertex); // Screen Position
                COMPUTE_EYEDEPTH(o.screenPos.z);
                // Normalize the depth value with 1/FarPlane
                o.linDepth = -(UnityObjectToViewPos(v.vertex).z 
                                *(_ProjectionParams.w)); 
                return o;
            }
            
            sampler2D _MainTex, _CameraDepthTexture;
            float4 _GlowColor;

            fixed4 frag (v2f i) : SV_Target
            {
                float4 col = float4(0,0,0,1);
                float2 uv = i.screenPos.xy / i.screenPos.w;
                float4 camDepth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv).r);
                camDepth = Linear01Depth(camDepth);

                float diff = saturate(i.linDepth - camDepth);
                if(diff < 0.001){
                    col = _GlowColor;
                }
                return col;
            }
            ENDCG
        }
    }

    // In case it doesn't work
    Fallback "Diffuse"
}
