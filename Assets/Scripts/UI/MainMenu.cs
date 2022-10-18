using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject levelIdentifier;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        levelIdentifier = GameObject.Find("Level_ID");
        
    }

    public void playLevel1(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Assets/Levels/level_1.txt";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); 
    }
    public void playLevel2(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Assets/Levels/slow_level.txt";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void playLevel3(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Assets/Levels/ice_level.txt";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void endlessLevel(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Procedural";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
