using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcess : MonoBehaviour
{
    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        Debug.Log("Hemlo!");
        Graphics.Blit(src, dst);

    }
}
