using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public int coinCount = 0;

      
    void Awake() 
    { 
        DontDestroyOnLoad(gameObject); 
    }

    public void IncrementCoinCount()
    {
        coinCount += 1;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

}
