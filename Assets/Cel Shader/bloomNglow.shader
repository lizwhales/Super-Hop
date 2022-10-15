Shader "Custom/BloomNGlowShader"
{
    Properties
    {   
        _GScale("Glow Scale", Range(0.001, 20.0)) = 0.5
        _GIntensity("Glow Intensity", Range(0.1,10.0)) = 0.25
        _BScale("Bloom Scale", Range(0.001, 20.00)) = 0.5
        _BIntensity("Bloom Intensity", Range(0.1,10.0)) = 0.25
    }

    // https://forum.unity.com/threads/is-it-possible-to-create-a-shader-property-for-cull-ztest.13794/
    // Try this:

    // Property
    // [Enum(Off,0,On,1)]_ZWrite ("ZWrite", Float) = 1.0
    // [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", Float) = 4 //"LessEqual"
    // [Enum(UnityEngine.Rendering.CullMode)] _Culling ("Culling", Float) = 0

    // Pass
    // ZWrite [_ZWrite]
    // ZTest [_ZTest]
    // Cull [_Culling]

    // ------------------- CGINCLUDE ----------------- //
    CGINCLUDE
    #pragma vertex vert
    #pragma fragment frag
    #include "UnityCG.cginc"

    sampler2D _MainTex, _GlowMap; // or just be glow/bloom map - don't need two
    float4 _MainTex_TexelSize, _GlowMap_TexelSize;
    float _UpScale;

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

    half3 Prefilter (half3 col)
    { // _Threshold used in Blurs.cs -> Change to Bloom Threshold
        half brightness = max(col.r, max(col.g, col.));
        half contribution = max(0, brightness - _Threshold);
        contribution /= max(brightness, 0.00001);
        return col * contribution;
    }

    float3 blurSample (sampler2D tex, float2 uv, float2 xy, float scale, int prefilter)
    {
        float3 ans;
        if (prefilter == 1){
            ans = 
            Prefilter(tex2D(tex, uv + xy * float2(-scale, -scale))) +
            Prefilter(tex2D(tex, uv + xy * float2(-scale, scale))) +
            Prefilter(tex2D(tex, uv + xy * float2(scale, -scale))) +
            Prefilter(tex2D(tex, uv + xy * float2(scale, scale)));
        } else {
            ans =
            tex2D(tex, uv + xy * float2(-scale, -scale)) +
            tex2D(tex, uv + xy * float2(-scale, scale)) +
            tex2D(tex, uv + xy * float2(scale, -scale)) +
            tex2D(tex, uv + xy * float2(scale, scale));
        }
        return ans;
    }

    ENDCG
    // ------------------- CGINCLUDE ----------------- //

    SubShader
    {
        // No Culling or Depth
        Cull Off ZWrite Off ZTest Always // This is off in CameraGlowShader, but on in BloomShader
        Pass // 0 - Downsample with Prefilter (BLOOM)
        {
            CGPROGRAM
            float scale = 1.0;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = blurSample(_MainTex, i.uv, _MainTex_TexelSize.xy, scale, 1);
                return fixed4(blurSample*0.25,1);
            }
            ENDCG
        }

        Pass // 1 - Downsample without Prefilter -> Can use for Bloom and Glow
        {
            CGPROGRAM
            float scale = 1.0;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = blurSample(_MainTex, i.uv, _MainTex_TexelSize.xy, scale, 0);
                return fixed4(blurSample*0.25,1);
            }
            ENDCG
        }

        Pass // 2 - Upsample -> Can use for Bloom and Glow
        {
            Blend One One
            CGPROGRAM
            float scale = 0.5;

            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = blurSample(_MainTex, i.uv, _MainTex_TexelSize.xy, scale, 0);
                return fixed4(blurSample*0.25,1)*0.25; // Make the last float a variable passed through script
            }
            ENDCG
        }

        Pass // 3 - Bloom Pass -> Used for Bloom
        {
            CGPROGRAM
            float _Intensity; // Used in Blurs.cs change to _BIntensity
            sampler2D _BloomPassRT;
            float4 _BloomPassRT_TexelSize;
            float scale = 0.5;

            half4 frag (v2f i) : SV_Target
            {
                float3 blurSample = blurSample(_BloomPassRT, i.uv, _BloomPassRT_TexelSize.xy, scale, 0);
                half4 c = tex2D(_BloomPassRT, i.uv);
                half4 mainTex = tex2D(_MainTex, i.uv);
                c.rgb += _Intensity * blurSample;
                return mainTex+(c*0.25);
            }
            ENDCG
        }

        Pass // 4 - Composite BlurMap with MainTex [Glow] -> Used for Glow
        {
            CGPROGRAM
            sampler2D _GBlurMap, _FinalTex;
            float _GlowIntensity;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainTex = tex2D(_FinalTex, i.uv);
                fixed4 blurMap = tex2D(_GBlurMap, i.uv);
                return mainTex + (blurMap*_GlowIntensity);
            }
            ENDCG
        }
    }
}