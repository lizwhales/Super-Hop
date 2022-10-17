Shader "Custom/BloomShader"
{
    Properties
    { 
        _MainTex ("Texture", 2D) = "white" {}
        _Threshold("Prefilter Threshold", Range(0.1,2.0)) = 1.0
        _Intensity("Intensity", Range(0.0,5.0)) = 1.0
    }

    // Includes for each pass, reduce code repeats
    // -------------------CGINCLUDE--------------------//
    CGINCLUDE
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_TexelSize;

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
    ENDCG
    // -------------------CGINCLUDE--------------------//

    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass // 0 - Downsample with Prefilter
        {
            CGPROGRAM
            float scale = 1.0;
            float _Threshold;
            half3 Prefilter (half3 col)
            {
                half brightness = max(col.r, max(col.g, col.b));
                half contribution = max(0, brightness - _Threshold);
                contribution /= max(brightness, 0.00001);
                return col * contribution;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = 
                Prefilter(tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,-scale))) + 
                Prefilter(tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,scale))) +
                Prefilter(tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,-scale))) +
                Prefilter(tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,scale)));
                return fixed4(blurSample*0.25,1);
            }
            ENDCG
        }

        Pass // 1 - Downsample
        {
            CGPROGRAM
            float scale = 1.0;
            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = 
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,-scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,-scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,scale));
                return fixed4(blurSample*0.25,1);
            }
            ENDCG
        }

        Pass // 2 - Upsample
        {
            Blend One One
            CGPROGRAM
            float scale = 0.5;
            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = 
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,-scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale,scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,-scale)) +
                tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale,scale));
                return fixed4(blurSample*0.25,1)*0.25;
            }
            ENDCG
        }

        Pass // 3 - Bloom Pass
        {
            CGPROGRAM
                float _Intensity;// = 1;
                sampler2D _BloomPassRT;
                float4 _BloomPassRT_TexelSize;
                float scale = 0.5;
                half4 frag (v2f i) : SV_Target
                {
                    float3 blurSample = 
                    tex2D(_BloomPassRT, i.uv + _BloomPassRT_TexelSize.xy * float2(-scale,-scale)) +
                    tex2D(_BloomPassRT, i.uv + _BloomPassRT_TexelSize.xy * float2(-scale,scale)) +
                    tex2D(_BloomPassRT, i.uv + _BloomPassRT_TexelSize.xy * float2(scale,-scale)) +
                    tex2D(_BloomPassRT, i.uv + _BloomPassRT_TexelSize.xy * float2(scale,scale));
                    half4 c = tex2D(_BloomPassRT,i.uv);
                    half4 mainTex = tex2D(_MainTex, i.uv);
                    c.rgb += _Intensity * blurSample;
                    return mainTex+(c*0.25);
                }
            ENDCG
        }

    }
}
