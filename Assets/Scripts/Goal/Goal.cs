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
            Transform winPanel = panel.transform.GetChild(2);
            Transform objPanel = panel.transform.GetChild(5);
            GameObject crosshair = GameObject.Find("crosshair");
            goalReached(winPanel,objPanel,crosshair);
        }
    }

    // When here we shouldn't be able to hit escape and open the pause menu.
    public static void goalReached(Transform winPanel, Transform objPanel, GameObject crosshair)
    {
        // Debug.Log("winner winner chicken dinner");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crosshair.SetActive(false);
        objPanel.gameObject.SetActive(false);
        winPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        //SceneManager.LoadScene(0);
    }
}
