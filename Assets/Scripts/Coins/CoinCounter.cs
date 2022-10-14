using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounter : MonoBehaviour
{

    public static int coins = 0;

    public static void addCoins(int amount) {
        coins = coins + amount;
        Debug.Log("Added coins: " + amount);
        Debug.Log("Remaining coins: " + coins);
    }

    public static bool removeCoins(int amount) {
        if (amount > coins) {
            Debug.Log("Not enough coins: " + amount);
            Debug.Log("Remaining coins: " + coins);
            return false;
        } else {
            coins = coins - amount;
            Debug.Log("Removed coins: " + amount);
            Debug.Log("Remaining coins: " + coins);
            return true;
        }
    }

}
