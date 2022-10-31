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
        addCoins(10);
    }

    public static void addCoins(int amount)
    {
        coins = coins + amount;
        TMP_Text coinsText= GameObject.Find("coinCountText").GetComponent<TMP_Text>();
        coinsText.SetText(coins.ToString());
    }

    public static bool removeCoins(int amount)
    {
        if (amount > coins)
        {
            return false;
        } else {
            coins = coins - amount;
            TMP_Text coinsText= GameObject.Find("coinCountText").GetComponent<TMP_Text>();
            coinsText.SetText(coins.ToString());
            return true;
        }
    }

    public void OnDisable()
    {
        coins = 0;
    }
}
