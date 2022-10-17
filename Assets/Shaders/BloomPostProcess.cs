using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloomPostProcess : MonoBehaviour
{
    public Shader bloomShader;
    public Material mat;
    private Camera cam;

    [Range(0.0f, 2.0f)]
    public float threshold = 1.0f;

    [Range(0.0f, 2.0f)]
    public float intensity = 1.0f;

    void Start(){
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst){
        if (mat == null){ 
            mat = new Material(bloomShader);
        }

        // SRC info and pass Shader values
        int width = src.width;
        int height = src.height;
        mat.SetFloat("_Threshold", threshold);
        mat.SetFloat("_Intensity", intensity);

        // First Blit - Use temporary RTs
        RenderTexture tmpDstRT = RenderTexture.GetTemporary(width,height,0,src.format);
        Graphics.Blit(src, tmpDstRT, mat, 0);
        RenderTexture tmpSrcRT = tmpDstRT;
        
        // Down Sample For Loop
        for (int i = 0; i < 3; i++)
        {
            width = width / 2;
            height = height / 2;
            tmpDstRT = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(tmpSrcRT, tmpDstRT, mat, 1);
            RenderTexture.ReleaseTemporary(tmpSrcRT);
            tmpSrcRT = tmpDstRT;
        }

        // Up Sample For
        for (int i = 2; i >= 0; i--)
        {
            tmpDstRT = RenderTexture.GetTemporary(width,height,0,src.format);
            Graphics.Blit(tmpSrcRT, tmpDstRT, mat, 2);
            RenderTexture.ReleaseTemporary(tmpSrcRT);
            tmpSrcRT = tmpDstRT;
        }
        
        // OUTPUT
        Shader.SetGlobalTexture("_BloomPassRT", tmpSrcRT);
        Graphics.Blit(src, dst, mat, 3);
        RenderTexture.ReleaseTemporary(tmpSrcRT);
    }

}
