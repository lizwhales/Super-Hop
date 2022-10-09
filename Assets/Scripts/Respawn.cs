using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    public float respawnHeight = -2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < respawnHeight)
        {
            RespawnPoint();
        }
    }
    
    void RespawnPoint()
    {
        transform.position = spawnPoint.position;
        Debug.Log("respawning");
    }
}
