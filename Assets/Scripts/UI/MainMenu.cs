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
        levelIdentifier = GameObject.Find("Level_ID");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playLevel1(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Assets/Levels/test.txt";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void playLevel2(){
        
        levelIdentifier.GetComponent<LevelID>().LevelFile = "Assets/Levels/slow_level.txt";  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
