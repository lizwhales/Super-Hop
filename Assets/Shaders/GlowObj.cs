using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObj : MonoBehaviour
{
    public Material glowMat;

    public void OnEnable()
    {
        GlowSystem.instance.Add(this);
    }
    void Start()
    {
        GlowSystem.instance.Add(this);
    }

    public void OnDisable()
    {
        GlowSystem.instance.Remove(this);
    }
}
