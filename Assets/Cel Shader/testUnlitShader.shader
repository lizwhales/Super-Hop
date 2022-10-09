// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
// https://docs.unity3d.com/2022.1/Documentation/Manual/built-in-shader-examples-vertex-data.html

Shader "Custom/testUnlitShader"
{
    Properties
    {
        // For Outline - Cel Shading
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        _OutlineSize("Outline Size", Range(1.0,1.5)) = 1.1
        _AmbientLight("Ambient Light", Color) = (0.2,0.32,0.44,0)
        _ObjectColor("Object Color", Color) = (1,1,1,1)

        // Diffuse, and Specular || Step & Posterize
        _DiffuseStep("Diffuse Step", Range(0.001, 0.5)) = 0.1
        _SpecularStep("Specular Step", Range(0.25,0.95)) = 0.5
        _PosterizeDiffuse("Posterize Diffuse", Range(0,16)) = 0.0
        _PosterizeSpecular("Posterize Specular", Range(0,16)) = 0.0
    }
    SubShader
    {
        // to get lighting data: sets main directional light
        Tags{
            "LightMode" = "ForwardBase"
            "RenderType" = "Opaque"
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
     
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"
            #include "Lighting.cginc"

            // Inputs from the Mesh
            struct appdata
            {
                float4 vertex : POSITION; // vertex position
                float2 uv0 : TEXCOORD0; // texture coordinate
                float3 normal : NORMAL; 
            };

            // Vertex data to fragment data
            struct v2f
            {
                float4 vertex : SV_POSITION; // Clip Space position
                float2 uv0 : TEXCOORD0; //texture coordinate
                float3 normal :TEXCOORD1;
                float4 diffuse : COLOR0;
                float3 worldPos : TEXCOORD2;
            };

            float4 _ObjectColor, _AmbientLight;
            float _Gloss, _DiffuseStep, _SpecularStep, _PosterizeDiffuse, _PosterizeSpecular;

            // vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                o.uv0 = v.uv0;
                o.normal = v.normal;
                o.vertex = UnityObjectToClipPos(v.vertex); // Gets position of the clip space
                o.worldPos = mul(unity_ObjectToWorld, v.vertex); // Gets position of the world space
                return o;
            }

            float posterize(float steps, float value)
            {
                return floor(value * steps)/steps;
            }

            // Fragment shader  
            fixed4 frag (v2f i) : SV_Target
            {
                // Setup
                float2 uv = i.uv0;
                float3 normal = normalize(i.normal); // This has been interpolated
                
                // Lighting
                float3 lightDir = _WorldSpaceLightPos0.xyz;
                float lightFalloff = max(0,dot(i.normal, lightDir));

                if (_PosterizeDiffuse == 0){
                    lightFalloff = step(_DiffuseStep, lightFalloff);
                } else {
                    lightFalloff = posterize(_PosterizeDiffuse, lightFalloff);
                }

                // Direct diffues light
                i.diffuse = lightFalloff * _LightColor0;
                float4 diffuseLight = i.diffuse + _AmbientLight;

                // Direct Specular light. Dir from object to camera, need to define view vector
                float3 camPos = _WorldSpaceCameraPos;
                float3 fragToCam = camPos - i.worldPos;
                float3 viewDir = normalize (fragToCam);
                float3 viewReflect = reflect(-viewDir, normal);

                float specularFalloff = max(0,dot(viewReflect, lightDir));

                if (_PosterizeSpecular == 0){
                    specularFalloff = step(_SpecularStep, specularFalloff);
                } else {
                    specularFalloff = posterize(_PosterizeSpecular, specularFalloff);
                }

                //specularFalloff = step(_SpecularStep, specularFalloff);
                float4 directSpecular = _LightColor0 * specularFalloff;

                // Composite everything together and return the final look
                float4 col = diffuseLight * _ObjectColor + directSpecular;
                return col;
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
                float4 vertex:SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex=UnityObjectToClipPos(v.vertex*_OutlineSize);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}
