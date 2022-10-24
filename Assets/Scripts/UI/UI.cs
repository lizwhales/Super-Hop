using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    public static bool PausedGame = false;
    public GameObject _pauseMenu;
    public GameObject _crosshair;
    public GameObject _winPanel;
    public GameObject _losePanel;
    public GameObject _objPanel;

    public GameObject level1Text;
    public GameObject level2Text;
    public GameObject level3Text;

    GameObject gameScripts;
    string levelIdentifier;    


    // Start is called before the first frame update
    void Start()
    {
        // Set up Cursor and Time
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        
        // Set up UI panel
        _objPanel.SetActive(true);
        _crosshair.SetActive(false);

        // Set up Tutorial Text
        gameScripts = GameObject.Find("GameScripts");
        levelIdentifier = gameScripts.GetComponent<LevelGenerator>().levelFile;

        if (levelIdentifier == "Assets/Levels/level_1.txt")
        {
            level3Text.SetActive(false);
            level2Text.SetActive(false);
            level1Text.SetActive(true);
        }
        
        if (levelIdentifier == "Assets/Levels/level_2.txt")
        {
            level3Text.SetActive(false);
            level1Text.SetActive(false);
            level2Text.SetActive(true);
        }

        if (levelIdentifier == "Assets/Levels/ice_level.txt")
        {
            level1Text.SetActive(false);
            level2Text.SetActive(false);
            level3Text.SetActive(true);
        }
    }

    public void RemoveTutText(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        _crosshair.SetActive(true);
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _pauseMenu.SetActive(false);
        _crosshair.SetActive(true);
        ManagePauseMenu();
    }

    public void ManagePauseMenu(){
        GameObject pauseMainMenu = _pauseMenu.transform.GetChild(0).GetChild(0).gameObject;
        GameObject optionsMenu = _pauseMenu.transform.GetChild(0).GetChild(1).gameObject;

        if(optionsMenu.activeSelf == true)
        {
            optionsMenu.SetActive(false);
            pauseMainMenu.SetActive(true);
        }
    }

    public void MainMenu(){
        _pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        PausedGame = false;
        _objPanel.SetActive(false);
        _crosshair.SetActive(false);
        ManagePauseMenu();
        SceneManager.LoadScene(0);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
