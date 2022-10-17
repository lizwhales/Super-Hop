using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;



public class LevelGenerator : MonoBehaviour
{   
    // declaring constants

    // level constants
    public const int LAYER_SPACING = 2;
    public const int LEVEL_HEIGHT = 4;
    public const int LEVEL_WIDTH = 5;

    // tile constants
    public const int TILE_WIDTH = 4;
    public const int TILE_HEIGHT = 1;

    // wall constants
    public const int WALL_LENGTH = 4;
    public const int WALL_HEIGHT = 2;
    public const int WALL_WIDTH = 1;
    
    public const float LEFT_WALL_X_OFFSET = -2.5f;
    public const float RIGHT_WALL_X_OFFSET = LEVEL_WIDTH * TILE_WIDTH - 1.5f;
    public const float WALL_Y_OFFSET = -0.5f;
    
    // store which level will be loaded
    public string levelFile = " ";

    



    // declaring transformations for a given object
   
    public Transform cube;
    public Transform wall;
    public Transform wall2;
    public Transform backWall;
    public Transform frontWall;
    public Transform sideBackWall;
    public Transform floor;
    public Transform player;
    public Transform start;
    public Transform goal;
    public Transform slowCube;
    public Transform iceCube;
    public Transform coin;
    public Transform spikes;
    

    // declaring identifiers for different objects in the text file
    public const string sVoid = "-";
    public const string sTile = "x";
    public const string sGoalBox = "G";
    public const string sGoal = "g";
    public const string sStart = "S";
    public const string sWall = ">";
    public const string sWall2 = "<";
    public const string sBackWall = "=";
    public const string sSlow = "s";
    public const string sIce = "i";
    public const string sCoinBox = "C";
    public const string sCoin = "c";
    public const string sSpike = "^";

    public const int NUM_LAYERS = 6;
    // layer 0 to 5 :
    // layer 0 -> y = 0 level
    // layer 1 -> y = 1 level
    // layer 2 -> y = 2 level
    // layer 3 -> y = 3 level
    // layer 4 -> left wall
    // layer 5 -> right wall

    string[][] readLayerText(int layer, string text) {
        string [] layers = Regex.Split(text, "#");
        //Debug.Log("number of layers: "+ layers.Length);
        string curLayer = layers[layer];
        string[] lines = Regex.Split(curLayer, ",\r\n");
        //Debug.Log("This is layer: " + layer);
        for (int k = 0; k < lines.Length; k++){
            // Debug.Log(lines[k]);
        }
        int rows = lines.Length;
    
        string[][] layerBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)  {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            layerBase[i] = stringsOfLine;
        }
        return layerBase;
    }

    string[][] readLayer(int layer, string file){
        string text = System.IO.File.ReadAllText(file);
        return readLayerText(layer, text);
    }
    void generateLayer (int layer, string file, bool procedural = false){
        string[][] curLayer;
        if (procedural) {
            curLayer = readLayerText(layer, file);
        } else {
            curLayer = readLayer(layer, file);
        }
        
        if (layer < LEVEL_HEIGHT) {
            

            // create a layer based on the text file
            for (int z = 0; z < curLayer.Length - 1; z++) { // current layers length - 1 to remove empty field between layers
                for (int x = 0; x < curLayer[0].Length; x++) {
                    switch (curLayer[z][x]){
                    case sTile:
                        Instantiate(cube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sCoinBox:
                        Instantiate(cube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        Instantiate(coin, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING + 1.5f, z * TILE_WIDTH), coin.transform.rotation);
                        break;
                    case sCoin:
                        Instantiate(coin, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING + 1.5f, z * TILE_WIDTH), coin.transform.rotation);
                        break;
                    case sGoalBox:
                        Instantiate(cube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        Instantiate(goal, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING + 1.5f, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sGoal:
                        Instantiate(goal, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sSlow:
                        Instantiate(slowCube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sIce:
                        Instantiate(iceCube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sStart:
                        Instantiate(cube, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        Instantiate(player, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING + 2f, z * TILE_WIDTH), Quaternion.identity);
                        break;
                    case sSpike:
                        Instantiate(spikes, new Vector3(x * TILE_WIDTH, layer * LAYER_SPACING, z * TILE_WIDTH), Quaternion.identity);
                        
                        break;    
                    case sVoid:
                        break; 
                    } 
                    Instantiate(floor, new Vector3(x * TILE_WIDTH, -(LAYER_SPACING * LEVEL_HEIGHT), z * TILE_WIDTH), Quaternion.identity);
                    Instantiate(floor, new Vector3(x * TILE_WIDTH, LAYER_SPACING * LEVEL_HEIGHT * 2 - 1, z * TILE_WIDTH), Quaternion.identity);
                }        
            }
        } else {
            // create left wall based on the text file
            if (layer == 4){
                for (int z = 0; z < curLayer.Length - 1; z++) { 
                    for (int y = 0; y < curLayer[0].Length; y++) {
                        switch (curLayer[z][y]){
                        case sWall:
                            Instantiate(wall, new Vector3(LEFT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z*WALL_LENGTH), Quaternion.identity);
                            break;
                        case sWall2:
                            Instantiate(wall2, new Vector3(LEFT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z*WALL_LENGTH), Quaternion.identity);
                            break;
                        case sBackWall:
                            Instantiate(sideBackWall, new Vector3(LEFT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z*WALL_LENGTH), Quaternion.identity);
                            break;
                        case sVoid:
                            break; 
                        } 
                        Instantiate(wall, new Vector3(LEFT_WALL_X_OFFSET, -(y * WALL_HEIGHT + WALL_Y_OFFSET)-1, z*WALL_LENGTH), Quaternion.identity);
                    }        
                }
            // create right wall based on the text file
            } else if (layer == 5) {
                for (int z = 0; z < curLayer.Length; z++) { 
                    for (int y = 0; y < curLayer[0].Length; y++) {
                        switch (curLayer[z][y]){
                        case sWall:
                            Instantiate(wall, new Vector3(RIGHT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z * WALL_LENGTH), Quaternion.identity);
                            break;
                        case sWall2:
                            Instantiate(wall2, new Vector3(RIGHT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z * WALL_LENGTH), Quaternion.identity);
                            break;
                        case sBackWall:
                            Instantiate(sideBackWall, new Vector3(RIGHT_WALL_X_OFFSET, y * WALL_HEIGHT + WALL_Y_OFFSET, z * WALL_LENGTH), Quaternion.identity);
                            break;
                        case sVoid:
                            break; 
                        } 
                        //extend wall down to the bottom of the level
                        Instantiate(wall, new Vector3(RIGHT_WALL_X_OFFSET,-(y * WALL_HEIGHT + WALL_Y_OFFSET)-1, z * WALL_LENGTH), Quaternion.identity);
                    
                    }        
                }
                // Create front and back walls
                for (int x = 0; x < LEVEL_WIDTH; x++) {
                    for (int y = 0; y <= LEVEL_HEIGHT*2; y++) {
                        Instantiate(frontWall, new Vector3(x * TILE_WIDTH, y * WALL_HEIGHT + WALL_Y_OFFSET, -(TILE_WIDTH/2 + WALL_WIDTH/2)), Quaternion.identity);
                        Instantiate(backWall, new Vector3(x * TILE_WIDTH, y * WALL_HEIGHT + WALL_Y_OFFSET, (curLayer.Length) * TILE_WIDTH - 1.5f), Quaternion.identity);
                        Instantiate(frontWall, new Vector3(x * TILE_WIDTH, -(y * WALL_HEIGHT + WALL_Y_OFFSET), -(TILE_WIDTH/2 + WALL_WIDTH/2)), Quaternion.identity);
                        Instantiate(backWall, new Vector3(x * TILE_WIDTH, -(y * WALL_HEIGHT + WALL_Y_OFFSET), (curLayer.Length) * TILE_WIDTH - 1.5f), Quaternion.identity);
                    }
                }
            }
            
        }
    }
    // Start is called before the first frame update
    public void loadLevel(string filename, int numLayers){
        int i = 0;
        for (i = 0; i < numLayers; i++){
            generateLayer(i,filename);
        }
    }

    public void loadProceduralLevel(int numLayers) {
        int depth = 50;
        GameObject scripts = GameObject.Find("GameScripts");
        string generatedLevel = 
            scripts.GetComponent<ProceduralGenerator>().generatePerlinLevel(numLayers, LEVEL_HEIGHT, LEVEL_WIDTH, depth);
        for (int i = 0; i < numLayers; i++){
            generateLayer(i, generatedLevel, true);
        }
    }

    void Start()
    {
        GameObject level = GameObject.Find("Level_ID");
        levelFile = level.GetComponent<LevelID>().LevelFile;
        Debug.Log("LevelGenerator.cs: " + levelFile);
        if (levelFile == "Procedural")
        {
            loadProceduralLevel(NUM_LAYERS);
        } else {
            loadLevel(levelFile, NUM_LAYERS);
        }
        

        // loadProceduralLevel(NUM_LAYERS);
    }
    
}   

