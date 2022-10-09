using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalDetected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void goalReached()
    {
        Debug.Log("winner winner chicken dinner");
        SceneManager.LoadScene(0);
    }
}
