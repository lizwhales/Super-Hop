using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
    // declaring identifiers for different objects in the text file
    private const string sVoid = "-";
    private const string sTile = "x";
    private const string sGoalBox = "G";
    private const string sGoal = "g";
    private const string sStart = "S";
    private const string sWall = ">";
    private const string sWall2 = "<";
    private const string sBackWall = "=";
    private const string sSlow = "s";
    private const string sIce = "i";
    private const string sCoinBox = "C";
    private const string sCoin = "c";
    private const string sSpike = "^";

    public int NUM_LAYERS;
    public int LEVEL_HEIGHT;
    public int LEVEL_WIDTH;
    // layer 0 to 5 :
    // layer 0 -> y = 0 level
    // layer 1 -> y = 1 level
    // layer 2 -> y = 2 level
    // layer 3 -> y = 3 level
    // layer 4 -> left wall
    // layer 5 -> right wall

    void Start() {
    }

    string generateStart() {
        string startLine = "";
        for (int i = 0; i < LEVEL_WIDTH; i++) {
            startLine += "x";
        }
        return startLine;
    }

    public string generatePerlinLevel(int NUM_LAYERS, int LEVEL_HEIGHT, int LEVEL_WIDTH, int LEVEL_DEPTH) {
        char[,,] levelArray = new char[LEVEL_WIDTH, LEVEL_DEPTH, LEVEL_HEIGHT];

        float startX = Random.value;
        float startY = Random.value;
        float xyscale = 0.5F;
        int zscale = LEVEL_HEIGHT;

        // Generate full floored perlin noise level
        for (int i = 0; i < LEVEL_WIDTH; i++) {
            for (int j = 0; j < LEVEL_DEPTH; j++) {
                float x_c = startX + i * xyscale;
                float y_c = startY + j * xyscale;
                int height = (int) (Mathf.PerlinNoise(x_c, y_c) * zscale);
                for (int k = 0; k < LEVEL_HEIGHT; k++) {
                    if (k == height) {
                        levelArray[i, j, k] = 'x';
                    } else {
                        levelArray[i, j, k] = '-';
                    }
                }
            }
        }

        // Delete random floors
        float platformDensity = 0.13F;
        for (int i = 0; i < LEVEL_WIDTH; i++) {
            for (int j = 0; j < LEVEL_DEPTH; j++) {
                for (int k = 0; k < LEVEL_HEIGHT; k++) {
                    if (levelArray[i, j, k] == 'x') {
                        if (Random.value < 1.0 - platformDensity) {
                            levelArray[i, j, k] = '-';
                        }
                    }
                }
            }
        }

        // Add some coins and spikes on platforms
        float coinPlatformDensity = 0.1F;
        float spikePlatformDensity = 0.1F;
        for (int i = 0; i < LEVEL_WIDTH; i++) {
            for (int j = 0; j < LEVEL_DEPTH; j++) {
                for (int k = 0; k < LEVEL_HEIGHT; k++) {
                    if (levelArray[i, j, k] == 'x') {
                        float rnd = Random.value;
                        if (rnd < coinPlatformDensity) {
                            levelArray[i, j, k] = 'C';
                        } else if (rnd < coinPlatformDensity + spikePlatformDensity) {
                            levelArray[i, j, k] = '^';
                        }
                    }
                }
            }
        }

        // Mutate environment
        float startX_env = Random.value;
        float xyscale_env = 0.8F;

        float ice = 0.3F;
        float mud = 0.3F;

        for (int i = 0; i < LEVEL_WIDTH; i++) {
            for (int j = 0; j < LEVEL_DEPTH; j++) {
                float x_c = startX_env + j * xyscale_env;
                float environment = Mathf.PerlinNoise(x_c, 0.0F);
                for (int k = 0; k < LEVEL_HEIGHT; k++) {
                    if (levelArray[i, j, k] == 'x') {
                        if (environment < ice) {
                            levelArray[i, j, k] = 'i';
                        } else if (environment > 0.5 && environment < 0.5 + mud) {
                            levelArray[i, j, k] = 's';
                        }
                    }
                }
            }
        }

        // Add some coins in air
        float coinAirDensity = 0.05F;
        for (int i = 0; i < LEVEL_WIDTH; i++) {
            for (int j = 0; j < LEVEL_DEPTH; j++) {
                for (int k = 0; k < LEVEL_HEIGHT; k++) {
                    if (levelArray[i, j, k] == '-') {
                        if (Random.value < coinAirDensity) {
                            levelArray[i, j, k] = 'c';
                        }
                    }
                }
            }
        }

        // Force starting line to be safe
        for (int i = 0; i < LEVEL_WIDTH; i++){
            for (int j = 0; j < LEVEL_HEIGHT; j++) {
                if (j == 0) {
                    levelArray[i, 0, j] = 'x';
                } else {
                    levelArray[i, 0, j] = '-';
                }
            }
        }

        // Force goal line to be safe
        for (int i = 0; i < LEVEL_WIDTH; i++){
            for (int j = 0; j < LEVEL_HEIGHT; j++) {
                for (int k = LEVEL_DEPTH-3; k < LEVEL_DEPTH; k++) {
                    if (j == 0) {
                        levelArray[i, k, j] = 'x';
                    } else {
                        levelArray[i, k, j] = '-';
                    }
                }

            }
        }

        string levelText = buildLevelText(levelArray);
        Debug.Log(levelText);
        return levelText;
    }

    string buildLevelText(char[,,] levelArray) {
        int LEVEL_DEPTH = levelArray.GetLength(1);
        int LEVEL_WIDTH = levelArray.GetLength(0);
        int LEVEL_HEIGHT = levelArray.GetLength(2);
        int NUM_LAYERS = LEVEL_HEIGHT + 2;
        int levelFileLength = (LEVEL_DEPTH + 1) * NUM_LAYERS - 1;
        string[] levelText = new string[levelFileLength];

        int lineCounter = 0;
        string slice = "";

        // LayerN
        for (int n = 0; n < LEVEL_HEIGHT; n++) {

            if (n != 0) {
                string layerName = "Layer " + (n+1).ToString() + " #";
                levelText[lineCounter] = layerName;
                lineCounter++;
            }

            for (int i = 0; i < LEVEL_DEPTH; i++) {
                slice = "";
                for (int j = 0; j < LEVEL_WIDTH; j++) {
                    slice += levelArray[j, i, n];
                    slice += " ";
                }
                slice = slice.Trim();
                slice += ",";
                levelText[lineCounter] = slice;
                lineCounter++;
            }

        }

        // Left Wall
        levelText[lineCounter] = "Left Wall #";
        lineCounter++;

        double glowyChance = 0.1;
        
        for (int i = 0; i < LEVEL_DEPTH; i++) {
            slice = "";
            for (int j = 0; j < LEVEL_HEIGHT * 2; j++) {
                if (Random.value < glowyChance) {
                    slice += "< "; // Glowy boy
                } else {
                    slice += "> "; // Standard boy
                }

            }
            slice = slice.Trim();
            slice += ",";
            levelText[lineCounter] = slice;
            lineCounter++;
        }

        // Right Wall
        levelText[lineCounter] = "Right Wall #";
        lineCounter++;

        for (int i = 0; i < LEVEL_DEPTH; i++) {
            slice = "";
            for (int j = 0; j < LEVEL_HEIGHT * 2; j++) {
                if (Random.value < glowyChance) {
                    slice += "< "; // Glowy boy
                } else {
                    slice += "> "; // Standard boy
                }
            }
            slice = slice.Trim();
            slice += ",";
            levelText[lineCounter] = slice;
            lineCounter++;
        }
        levelText[lineCounter-1] = levelText[lineCounter-1].Remove(levelText[lineCounter-1].Length - 1, 1);

        return System.String.Join("\r\n", levelText);

    }
    
}   

