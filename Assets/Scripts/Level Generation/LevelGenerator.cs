using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;



public class LevelGenerator : MonoBehaviour
{
    // declaring transformations for a given object
   
    public Transform cube;
    public Transform player;
    public Transform start;
    public Transform goal;
    public Transform coin;

    // declaring identifiers for different objects in the text file
    public const string sVoid = "-";
    public const string sTile = "x";
    public const string sGoal = "G";
    public const string sCoin = "c";


    string[][] readFile(string file){
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
        
        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)  {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }
    // Start is called before the first frame update
    void Start()
    {
        string[][] level = readFile("Assets/test.txt");
        Debug.Log(level);
     
        // create a level based on the text file
        for (int z = 0; z < level.Length; z++) {
            for (int x = 0; x < level[0].Length; x++) {
                switch (level[z][x]){
                case sTile:
                    Instantiate(cube, new Vector3(x*4, 0, z*4), Quaternion.identity);
                    break;
                case sGoal:
                    Instantiate(goal, new Vector3(x, 0, z), Quaternion.identity);
                    break;
                case sVoid:
                    break; 
                case sCoin:
                    // TODO: move down
                    Instantiate(coin, new Vector3(x*4, 2, z*4), coin.transform.rotation);
                    break; 
                }
            
            }        
        }
    }
    
}   

