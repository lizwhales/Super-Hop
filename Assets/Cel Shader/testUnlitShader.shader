// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// https://docs.unity3d.com/2022.1/Documentation/Manual/built-in-shader-examples-vertex-data.html

Shader "Unlit/testUnlitShader"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0, 1, 0, 1)
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineSize("Outline Size", Range(1.0,1.5))=1.1

        _SpecColor("Spec Color", Color) = (0.9, 0.9, 0.9, 1)
        _Shinniness("Shinniness", Float) = 2

        _GlowColor("Glow Color", Color) = (1, 1, 1, 1)
     
    
    }
    SubShader
    {
        
        //Tags { "RenderType"="Opaque" }
        LOD 100

        // to get lighting data: sets main directional light
        Tags{
            "LightMode" = "ForwardBase"
            "PassFlags" = "OnlyDirectional"
        }
        Pass
        {

            
            CGPROGRAM
            //vetex sader
            #pragma vertex vert
            //fragment pixel (fragment) shader
            #pragma fragment frag
     
            #include "UnityCG.cginc"

            // Shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
                float3 normal : NORMAL; 
            };

            // Vertex to fragment
            struct v2f
            {
                float2 uv : TEXCOORD0; //texture coordinate
                float4 vertex : SV_POSITION; // clip space position
                float3 worldNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
    
            // vertex shader
            v2f vert (appdata v)
            {
                // position -> clip space = model*view*projection matrix
                v2f o;
                // UNITY_MATRIX_MVP -> global variable
                o.vertex = UnityObjectToClipPos(v.vertex);
                // pass the texture coordinates
                //o.uv = v.uv;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // normalize object space to world space
                o.worldNormal = UnityObjectToWorldNormal(v.normal);

                return o;
            }

            // for the color
            float4 _BaseColor;
            float lightIntensity;
            // Fragment shader  
            fixed4 frag (v2f i) : SV_Target
            {
                // Blinn-Phong shading vector
                float3 normal = normalize(i.worldNormal);
               
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                if (NdotL > 0){
                    lightIntensity = 1;
                } else {
                    lightIntensity = 0.3;
                }
                // sample the texture
                float4 sample = tex2D(_MainTex, i.uv);
                // Base COlor
                // return _BaseColor;
                // Cell Shade
                
                return _BaseColor * sample * lightIntensity;
                // Normal shading
                //return _BaseColor * sample * NdotL;

                // add emission -> Put this in another pass/function, as this function operates on Shadow the Hedgehog
                // return _BaseColor * sample * (_Emission + lightIntensity);

            }
            ENDCG
        }
        pass
        {
            // Very generic outline shader
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _OutlineColor;
            float _OutlineSize;

            struct appdata
            {
                float4 vertex:POSITION;
            };

            struct v2f
            {
                float4 clipPos:SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.clipPos=UnityObjectToClipPos(v.vertex*_OutlineSize);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }

        Pass
        {
            // Specular Lighting
           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            uniform float4 _LightColor0;

            #include "UnityCG.cginc"
            
            
            // Shader inputs
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv : TEXCOORD0; // texture coordinate
                float3 normal : NORMAL; 
            };

            // Vertex to fragment
            struct v2f
            {
                float2 uv : TEXCOORD0; //texture coordinate
                float4 vertex : SV_POSITION; // clip space position
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float _Shinniness;
            float4 _SpecColor;

            float4 _GlowColor;
         
    
            // vertex shader
            v2f vert (appdata v)
            {
                // position -> clip space = model*view*projection matrix
                v2f o;
                // UNITY_MATRIX_MVP -> global variable
                o.vertex = UnityObjectToClipPos(v.vertex);
                // pass the texture coordinates
                //o.uv = v.uv;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // normalize object space to world space
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = WorldSpaceViewDir(v.vertex);
               
                return o;
            }

            // for the color
            float4 _BaseColor;
            float lightIntensity;
            // Fragment shader  
            fixed4 frag (v2f i) : SV_Target
            {
                // Blinn-Phong shading vector
                float3 normal = normalize(i.worldNormal);
               
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                if (NdotL > 0){
                    lightIntensity = 1;
                } else {
                    lightIntensity = 0.3;
                }
                // sample the texture
                float4 sample = tex2D(_MainTex, i.uv);

                // specular lighting 
                float3 viewDir = normalize(i.viewDir);
                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float NdotH = dot(normal, halfVector);

                float specIntensity = pow(NdotH * lightIntensity, _Shinniness * _Shinniness);
                float specIntensitySmooth = smoothstep(0.005, 0.01, specIntensity);
                float4 spec = specIntensitySmooth * _SpecColor;

                
                //return _BaseColor * sample * lightIntensity + spec; 
                
                // glow calcs
                float glowDot = 1 - dot(viewDir, normal);
                float4 glow = glowDot * _GlowColor;
                return _BaseColor * sample * lightIntensity + spec + glow; 
                

            }
            ENDCG
        }
    }
}
