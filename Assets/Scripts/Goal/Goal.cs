using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    GameObject panel;

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            panel = GameObject.Find("UI");
            // Debug.Log("panel.GetType(); " + panel.GetType());
            Transform winPanel = panel.transform.GetChild(2);
            // Debug.Log("winPanel.GetType(): " + winPanel.GetType());
            GameObject crosshair = GameObject.Find("crosshair");
            goalReached(winPanel,crosshair);
        }
    }

    // When here we shouldn't be able to hit escape and open the pause menu.
    public static void goalReached(Transform winPanel, GameObject crosshair)
    {
        // Debug.Log("winner winner chicken dinner");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        //SceneManager.LoadScene(0);
    }
}
