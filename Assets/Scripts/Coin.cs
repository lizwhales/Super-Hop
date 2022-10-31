using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter(Collider col){
        if (col.tag == "Player") {
            CoinCounter.addCoins(1);
            Timer.AddTime();
            Destroy(this.gameObject);
        }
    }

}
