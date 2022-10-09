using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "DeathTrigger")
        {
            Debug.Log("Player has collided with the death trigger!");
            //respawnPos = new Vector3(8,1.5f,0);
            transform.position = spawnPoint.position;
        }
    }
    
    void RespawnPoint()
    {
        transform.position = spawnPoint.position;
        Debug.Log("respawning");
    }
}
