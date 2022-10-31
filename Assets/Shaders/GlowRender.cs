using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GlowSystem
{
    static GlowSystem instant;

    static public GlowSystem instance
    {
        get
        {
            if (instant == null)
            {
                instant = new GlowSystem();
            }
            return instant;
        }
    }

    internal HashSet<GlowObj> glowObjs = new HashSet<GlowObj>();

    public void Add(GlowObj o)
    {
        Remove(o);
        glowObjs.Add(o);
    }

    public void Remove(GlowObj o)
    {
        glowObjs.Remove(o);
    }
}

public class GlowRender : MonoBehaviour
{
    private CommandBuffer glowCMD;
    private Dictionary<Camera, CommandBuffer> cameras = new Dictionary<Camera, CommandBuffer>();
    // Start is called before the first frame update
    private void Cleanup()
    {
        foreach(var cam in cameras)
        {
            if(cam.Key){
                cam.Key.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, cam.Value);
            }
            
        }
        cameras.Clear();
    }

    public void OnDisable()
    {
        Cleanup();
    }

    public void OnEnable()
    {
        Cleanup();
    }

    public void OnRenderObject()
    {
        var render = gameObject.activeInHierarchy && enabled;
        if(!render){ Cleanup(); return; }

        var cam = Camera.current;
        if(!cam){ return; }
        if(cameras.ContainsKey(cam)){ return; }

        // Make new CMD Buffer with RT for the object mask
        glowCMD = new CommandBuffer();
        glowCMD.name = "Glow Buffer"; // Visible in Frame Debugger
        cameras[cam] = glowCMD;

        // Create an instance of the Glow System
        var glowSystem = GlowSystem.instance;
        
        // Temporary RT for the object mask
        int tmpID = Shader.PropertyToID("_Tmp1");

        // Create temp RT then set to black
        glowCMD.GetTemporaryRT(tmpID, -1, -1, 24, FilterMode.Bilinear);
        glowCMD.SetRenderTarget(tmpID);
        glowCMD.ClearRenderTarget(true, true, Color.black);

        // Loop through each object in the list, but we only need one, really.
        foreach(GlowObj o in glowSystem.glowObjs)
        {
            Renderer r = o.GetComponent<Renderer>();
            Material glowMat = o.glowMat;
            
            if (r && glowMat){ glowCMD.DrawRenderer(r, glowMat); }
        }

        // Taken outside the foreach so the render isn't done several times over
        // Make this RT global and add the CMD buffer to the pipeline
        glowCMD.SetGlobalTexture("_GlowMap", tmpID);
        cam.AddCommandBuffer(CameraEvent.BeforeImageEffects, glowCMD);
    }
}
