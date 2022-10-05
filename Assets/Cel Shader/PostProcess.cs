using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class PostProcess : MonoBehaviour
{
    [SerializeField] Shader bloomShader;
    [SerializeField] Material bloom;
    [Range(0, 10)] public float threshold = 1;

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        // int width = src.width / 4;
        // int height = src.height / 4;
        // RenderTextureFormat format = src.format;

        // RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height,0, src.format);
        // Graphics.Blit(src, temp);
        // Graphics.Blit(temp, dst);
        // RenderTexture.ReleaseTemporary(temp);
        
        Graphics.Blit(src, dst);
    }
}
