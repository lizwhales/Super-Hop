Shader "Custom/PostProcessShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    // Numerous passes are made, so use CGINCLUDE
    CGINCLUDE
// Upgrade NOTE: excluded shader from DX11, OpenGL ES 2.0 because it uses unsized arrays
#pragma exclude_renderers d3d11 gles
        #include "UnityCG.cginc"
        // #define PI 3.14159265359
        // #define E 2.71828182846

        sampler2D _MainTex; //, _SourceTex;
        float4 _MainTex_TexelSize;
        half4 _Filter;
        half _Intensity;

        // float[] gaussianKernel = {
        //     0.00390625, 0.015625, 0.0234375, 0.015625, 0.00390625,
        //     0.015625, 0.0625, 0.09375, 0.0625, 0.015625,
        //     0.0234375, 0.09375, 0.140625, 0.09375, 0.0234375,
        //     0.015625, 0.0625, 0.09375, 0.0625, 0.015625,
        //     0.00390625, 0.015625, 0.0234375, 0.015625, 0.00390625
        // };

        // int kernelOffset = gaussianKernel.length() / 2;

        // int _Iterations;
        // float neighborSize;
        // float _StdDev;

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

        half3 RgbSample (float2 uv)
        {
            return tex2D(_MainTex, uv).rgb;
        }

    ENDCG

    SubShader
    {
        //Cull Off ZTest Always ZWrite Off
        Pass // pass 0
        {
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                fixed4 frag (v2f i) : SV_TARGET
                {
                    return tex2D(_MainTex, i.uv);
                }
            ENDCG
        }

        // Pass // pass 1
        // {
        //     CGPROGRAM
        //         #pragma vertex vert
        //         #pragma fragment frag
        //         fixed4 frag (v2f i) : SV_TARGET
        //         {
        //             fixed4 col = tex2D(_MainTex, i.uv);
        //             return col;
        //         }
        //     ENDCG
        // }

        // Pass // pass 2
        // {
        //     CGPROGRAM
        //         #pragma vertex vert
        //         #pragma fragment frag
        //         fixed4 frag (v2f i) : SV_TARGET
        //         {
        //             fixed4 col = tex2D(_MainTex, i.uv);
        //             return col;
        //         }
        //     ENDCG
        // }

        // Pass // pass 3
        // {
        //     CGPROGRAM
        //         #pragma vertex vert
        //         #pragma fragment frag
        //         fixed4 frag (v2f i) : SV_TARGET
        //         {
        //             fixed4 col = tex2D(_MainTex, i.uv);
        //             return col;
        //         }
        //     ENDCG
        // }
    }
}
