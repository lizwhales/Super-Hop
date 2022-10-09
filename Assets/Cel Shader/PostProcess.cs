using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class PostProcess : MonoBehaviour
{
    // [SerializeField] Shader bloomShader;
    // [SerializeField] Material bloom;
    // [Range(1,5)] public int downSampleIterations = 1;
    // [Range(0,10)] public int blurIterations = 1;
    // [Range(0,1)] public float neighborSize = .1f;
    // RenderTexture[] upSampleTextures = new RenderTexture[5];

    // [Range(0, .2f)] public float stdDev = .02f;

    void Awake () // Limiting fps to 60
    {
        QualitySettings.vSyncCount = 1;
    }

    // void OnRenderImage(RenderTexture src, RenderTexture dst) {
    //     if (bloom == null)
    //     {   
    //         Debug.Log("Bloom is null. Making new material.");
    //         bloom = new Material(bloomShader);
    //     }

    //     int width = src.width / 2;
    //     int height = src.height / 2;

    //     RenderTextureFormat format = src.format;
    //     RenderTexture currDst = RenderTexture.GetTemporary(width, height, 0, format);
    //     Graphics.Blit(src, currDst);//, bloom);
    //     RenderTexture currSrc = currDst;

    //     int i = 1;
    //     for (i = 1; i < downSampleIterations; i++)
    //     {
    //         width /= 2;
    //         height /= 2;
    //         //Debug.Log("DownSample. I is: " + i );
    //         if (height < 2){ break; }

    //         currDst = upSampleTextures[i] = RenderTexture.GetTemporary(width, height, 0, format);
    //         Graphics.Blit(currSrc,currDst);//, bloom);
    //         //RenderTexture.ReleaseTemporary(currSrc);
    //         currSrc = currDst;
    //     }

    //     for (i -= 2; i >= 0; i--)
    //     {   
    //         //Debug.Log("UpSample. I is: " + i);
    //         currDst = upSampleTextures[i];
    //         upSampleTextures[i] = null;
    //         Graphics.Blit(currSrc, currDst);//, bloom);
    //         RenderTexture.ReleaseTemporary(currSrc);
    //         currSrc = currDst;
    //     }

    //     RenderTexture.ReleaseTemporary(currDst);
    //     Graphics.Blit(currSrc, dst);//, bloom);
    //     // if (blurIterations <= 1)
    //     // {
    //     //     Graphics.Blit(currSrc, dst);
    //     // } else {
    //     //     bloom.SetInt("_Iterations", blurIterations);
    //     //     bloom.SetFloat("neighborSize", neighborSize);
    //     //     bloom.SetFloat("_StdDev", stdDev);

    //     //     var tmp = RenderTexture.GetTemporary(currSrc.width, currSrc.height);
    //     //     Graphics.Blit(currSrc, dst, bloom, 0);
    //     //     RenderTexture.ReleaseTemporary(tmp);
    //     // }
    //     // Graphics.Blit(currSrc, dst, bloom);



    //     // RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height,0, src.format);
    //     // Graphics.Blit(src, temp, bloom);
    //     // Graphics.Blit(temp, dst);
    //     // RenderTexture.ReleaseTemporary(temp);
        
    //     //Graphics.Blit(src, dst, bloom);
    // }

    // Need Convolution
    // Downscale - preferrably a power of 2x, 4x, 8x, 16x
    // Apply bright-pass filter
    // Gaussian blur filter - 5x5 kernel size
    // Bilinear filtering up
    // Blend_RGB_Add -> Additively blend resulting texture

}
