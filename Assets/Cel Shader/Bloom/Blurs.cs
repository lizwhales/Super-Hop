using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// USE THIS ONE - BOX BLUR
// [ExecuteInEditMode]
public class Blurs : MonoBehaviour
{
    public Shader blurShader;
    public Material material;
    private Camera cam;

    [Range(0.0f, 2.0f)]
    public float threshold = 1.0f;

    [Range(0.0f, 2.0f)]
    public float intensity = 1.0f;

    void OnRenderImage(RenderTexture src, RenderTexture dst){
        if (material == null){ 
            material = new Material(blurShader);
        }
        // SOURCE STUFF
        int width = src.width;
        int height = src.height;
        material.SetFloat("_BThreshold", threshold);
        material.SetFloat("_BIntensity", intensity);

        // DOWNSAMPLING
        // END RENDER TEXTURE - First Blit
        RenderTexture endRT = RenderTexture.GetTemporary(width,height,0,src.format);
        Graphics.Blit(src, endRT, material,0);
        RenderTexture startRT = endRT;
        
        // Down Sample For Loop
        for (int i = 0; i < 3; i++)
        {
            width = width / 2;
            height = height / 2;
            endRT = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(startRT, endRT, material, 1);
            RenderTexture.ReleaseTemporary(startRT);
            startRT = endRT;
        }

        for (int i = 2; i >= 0; i--)
        {
            endRT = RenderTexture.GetTemporary(width,height,0,src.format);
            Graphics.Blit(startRT, endRT, material, 2);
            RenderTexture.ReleaseTemporary(startRT);
            startRT = endRT;
        }
        
        // OUTPUT
        Shader.SetGlobalTexture("_BloomPassRT", startRT);
        Graphics.Blit(src, dst, material, 3);
        RenderTexture.ReleaseTemporary(startRT);
    }

}
