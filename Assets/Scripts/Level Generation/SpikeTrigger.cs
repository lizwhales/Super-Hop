using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
   
    public int spikePenalty = 3;
    
    void OnTriggerEnter(Collider col) 
    {
        if(col.tag == "Player"){
            CoinCounter.removeCoins(spikePenalty);
            Debug.Log("3 coins lost!");
        }
        
    }
}
