using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public TMP_Text coinsText;
    public static int coins = 0;

    public void Start()
    {   
        addCoins(5);
    }

    public static void addCoins(int amount) {
        coins = coins + amount;
        TMP_Text coinsText= GameObject.Find("coinCountText").GetComponent<TMP_Text>();
        coinsText.SetText(coins.ToString());
        // Debug.Log("Added coins: " + amount);
        // Debug.Log("Remaining coins: " + coins);
    }

    public static bool removeCoins(int amount) {
        if (amount > coins) {
            // Debug.Log("Not enough coins: " + amount);
            // Debug.Log("Remaining coins: " + coins);
            return false;
        } else {
            coins = coins - amount;
            TMP_Text coinsText= GameObject.Find("coinCountText").GetComponent<TMP_Text>();
            coinsText.SetText(coins.ToString());
            // Debug.Log("Removed coins: " + amount);
            // Debug.Log("Remaining coins: " + coins);
            return true;
        }
    }

    public void OnDisable()
    {
        coins = 0;
    }
}
