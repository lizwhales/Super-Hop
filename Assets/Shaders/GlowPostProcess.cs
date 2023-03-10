using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used on the Camera with CameraGlowMat and CameraGlowShader
public class GlowPostProcess : MonoBehaviour
{
    public Shader glowShader;
    public Material mat;
    private Camera cam;
    
    void Awake(){
        Application.targetFrameRate = 120;
    }

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;
    }

    // Update is called once per frame
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (mat == null)
        {
            mat = new Material(glowShader);
        }

        // SRC info and pass Shader values
        int width = src.width;
        int height = src.height;

        // First Blit - Use temporary RTs
        RenderTexture tmpDstRT = RenderTexture.GetTemporary(width, height, 0, src.format);
        Shader.SetGlobalTexture("_FinalTex", src);
        Graphics.Blit(src, tmpDstRT);
        RenderTexture tmpSrcRT = tmpDstRT;

        // Downsample For Loop
        for (int i = 0; i < 4; i++)
        {
            width /= 2;
            height /= 2;
            
            tmpDstRT = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(tmpSrcRT, tmpDstRT, mat, 0);
            RenderTexture.ReleaseTemporary(tmpSrcRT);
            tmpSrcRT = tmpDstRT;
        }

        // Upsample For Loop
        for (int i = 3; i >= 0; i--)
        {
            width *= 2;
            height *= 2;
            
            tmpDstRT = RenderTexture.GetTemporary(width, height, 0, src.format);
            Graphics.Blit(tmpSrcRT, tmpDstRT, mat, 1);
            RenderTexture.ReleaseTemporary(tmpSrcRT);
            tmpSrcRT = tmpDstRT; 
        }

        // Output
        Shader.SetGlobalTexture("_GBlurMap", tmpSrcRT);
        RenderTexture.ReleaseTemporary(tmpSrcRT);
        Graphics.Blit(src, dst, mat, 2);
        
    }
}
