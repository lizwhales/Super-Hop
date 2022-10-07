using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelID : MonoBehaviour
{
    public static LevelID control;

    public string LevelFile = "";

    void Awake() {
        control = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
