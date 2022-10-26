using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public Transform spawnPoint;
    private GameObject kb;
    private Vector3 kbPos;

    
    
    public int spikePenalty = 3;
    private float z;
    void Start(){
        kb = GameObject.Find("KillBox");
        kbPos = kb.transform.position;
    }
    void Update() {
        z = player.transform.position.z;
        kb.transform.position = new Vector3(kbPos.x, kbPos.y, z);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DeathTrigger")
        {
            Debug.Log("Player has collided with the death trigger!");
            RespawnPoint();
        } else if (other.gameObject.tag == "Obstacle") {
            CoinCounter.removeCoins(spikePenalty);
            Debug.Log("3 Coins lost!");
            RespawnPoint();
        }
    }
    
    public void RespawnPoint()
    {
        GameObject level = GameObject.Find("Level_ID");
        string levelFile = level.GetComponent<LevelID>().LevelFile;
        if (levelFile == "Procedural")
        {
            GameObject UI = GameObject.Find("UI");
            UI.transform.Find("Timer").GetComponent<Timer>().LoseGame();
        } else {
            transform.position = spawnPoint.position;
            Debug.Log("respawning");
        }


    }
}
