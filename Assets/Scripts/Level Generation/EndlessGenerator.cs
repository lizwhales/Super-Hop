using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessGenerator : MonoBehaviour
{
    private List<GameObject> sceneObjects;
    private float distanceToGoal;
    private GameObject goalInstance = null;
    private bool proc;

    void Start() {
        GameObject level = GameObject.Find("Level_ID");
        proc = level.GetComponent<LevelID>().LevelFile == "Procedural";
    }

    void Update() {

        if (proc) {
            if (goalInstance == null) {
                goalInstance = GameObject.FindWithTag("Finish");
            }

            distanceToGoal = Vector3.Distance(this.gameObject.transform.position, goalInstance.transform.position);

            if (distanceToGoal < 50) {
                RemovePast();
                goalInstance = null;
            }
        }

    }

    void RemovePast() {
        sceneObjects = new List<GameObject> (GameObject.FindGameObjectsWithTag("Block"));
        sceneObjects.AddRange(new List<GameObject> (GameObject.FindGameObjectsWithTag("Obstacle")));

        foreach (GameObject obs in sceneObjects) {
            
            float dist = Vector3.Distance(obs.transform.position, goalInstance.transform.position);
            if (dist > 200) {
                Destroy(obs);
            }
        }

        GameObject[] goalObjects = GameObject.FindGameObjectsWithTag("Finish");
        float newGoalZ = goalObjects[0].transform.position[2];

        foreach (GameObject wall in goalObjects) {
            Destroy(wall);
        }

        GameObject scripts = GameObject.Find("GameScripts");
        int NUM_LAYERS = 6; // hard-coded
        scripts.GetComponent<LevelGenerator>().loadProceduralLevel(NUM_LAYERS, newGoalZ + 1.5F);

    }
}