Shader "Custom/DepthMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ProjParamZ("Projection Param Z Multiplier", Range(0.0001, 0.5)) = 0.04
    }
    SubShader
    {
        Tags { "RenderType"="All" }
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            sampler2D _CameraDepthTexture;
            float _ProjParamZ;
            // UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float linearDepth : TEXCOORD1;
                float4 screenPos : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                // UNITY_TRANSFER_DEPTH(o.depth);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.linearDepth = -(UnityObjectToViewPos(v.vertex).z*(_ProjectionParams.w/1));
                return o;
            }

            sampler2D _MainTex;

            fixed4 frag (v2f i) : SV_Target
            {
                // Camera Depth
                float2 uv = i.screenPos.xy / i.screenPos.w; // Normalized screens-space position
                float4 camDepth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, uv).r);
                camDepth = Linear01Depth(camDepth);
                // return camDepth;

                // Object Depth
                float sceneZ = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.screenPos)));
                //float depth = sceneZ - i.screenPos.z;
                float depth = tex2D(_CameraDepthTexture, i.uv).r;
                // Linear depth b/w camera and far clipping plane
                depth = Linear01Depth(depth);
                depth = depth * (_ProjectionParams.z * _ProjParamZ);
                return depth;
                // UNITY_OUTPUT_DEPTH(i.depth);
            }
            ENDCG
        }
    }
}
