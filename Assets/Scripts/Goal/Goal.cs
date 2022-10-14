using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            goalReached();
        }
    }

    public static void goalReached()
    {
        Debug.Log("winner winner chicken dinner");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(0);
    }
}
