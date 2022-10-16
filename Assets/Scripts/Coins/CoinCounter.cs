using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI coinsText = null;
    public static int coins = 0;

    void Start()
    {   
        // Debug.Log(coinsText);
        addCoins(30);
    }
    public static void addCoins(int amount) {
        coins = coins + amount;
        // coinsText = GetComponent<TextMeshProUGUI>();
        // updateText(coinsText, coins);
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

            // updateText(coinsText, coins);
            Debug.Log("Removed coins: " + amount);
            Debug.Log("Remaining coins: " + coins);
            return true;
        }
    }

    public static void updateText(TextMeshProUGUI text, int amount){
        text.text = amount.ToString();
    }

}
