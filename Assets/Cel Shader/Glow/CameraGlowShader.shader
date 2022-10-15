Shader "Glow/CameraGlowShader"
{
    // Used on the Camera with CameraGlowMat and GlowPostProcess
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
        _UpScale("Scale for Upsample", Range(0.001, 20.0)) = 0.5
        _Intensity("Glow Intensity", Range(0.1, 10.0)) = 0.25
    }

    // -------------------CGINCLUDE--------------------//
    CGINCLUDE
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        
        sampler2D _MainTex, _GlowMap;
        float4 _GlowMap_TexelSize;
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
    ENDCG
    // -------------------CGINCLUDE--------------------//

    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always

        Pass // 0 - Down Sample
        {
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                float scale = 1.0;
                float3 blurSample = 
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(-scale,-scale)) + //Down, Left
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(-scale,scale)) + //Down, Right
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(scale,-scale)) + //Up, left
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(scale,scale)); //Up, right
                return fixed4(blurSample*0.25,1)*0.5; // Can change 0.5 and add in a variable as a setting
            }
            ENDCG
        }

        Pass // 1 - Up Sample
        {
            CGPROGRAM
            fixed4 frag (v2f i) : SV_Target
            {
                float3 blurSample = 
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(-_UpScale,-_UpScale)) + //Down, Left
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(-_UpScale,_UpScale)) + //Down, Right
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(_UpScale,-_UpScale)) + //Up, left
                tex2D(_GlowMap, i.uv + _GlowMap_TexelSize.xy * float2(_UpScale,_UpScale)); //Up, right
                return fixed4(blurSample*0.25,1)*0.5;
            }
            ENDCG
        }

        Pass // 2 - Composite BlurMap with MainTex
        {
            //Blend One One
            CGPROGRAM
            sampler2D _BlurMap, _FinalTex;
            float _Intensity;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 mainTexture = tex2D(_FinalTex, i.uv);
                //return mainTex;
                fixed4 blurMap = tex2D(_BlurMap, i.uv);
                return mainTexture + (blurMap*_Intensity);
            }
            ENDCG
        }
    }
}