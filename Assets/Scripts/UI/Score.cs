using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    private static float score = 0.0F;
    private GameObject playerInstance;

    void Start(){
        playerInstance = GameObject.FindWithTag("Player");
        score = playerInstance.transform.position[2];
    }

    void Update(){
        score = Mathf.Max(playerInstance.transform.position[2], score);
    }

    public static int getScore() {
        return (int)(score*1000);
    }
}