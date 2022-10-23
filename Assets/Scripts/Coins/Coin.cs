using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Player") {
            CoinCounter.addCoins(1);
            // add 5 secs to timer here
            Timer.AddTime();
            Destroy(this.gameObject);
        }
    }

}
