using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public static bool PausedGame = false;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _crosshair;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    { // Prevent menu from opening if the win or lose panel is active
        if (Input.GetKeyDown(KeyCode.Escape) && !_winPanel.activeInHierarchy && !_losePanel.activeInHierarchy){
            if (PausedGame){
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame(){
        Time.timeScale = 0f;
        PausedGame = true;
        _pauseMenu.SetActive(true);
        _crosshair.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame(){
        Time.timeScale = 1f;
        PausedGame = false;
        _pauseMenu.SetActive(false);
        _crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    public void MainMenu(){
        _pauseMenu.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
