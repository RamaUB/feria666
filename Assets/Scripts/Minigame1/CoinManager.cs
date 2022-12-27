using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public int coinCount;

      
    void Awake() 
    { 
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void IncrementCoinCount(int cant = 1)
    {
        coinCount += cant;
    }

    public int GetCoinCount()
    {
        return coinCount;
    }

}
