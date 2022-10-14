using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    
    public int spikePenalty = 3;

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
        transform.position = spawnPoint.position;
        Debug.Log("respawning");
    }
}
