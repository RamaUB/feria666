using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int cantidad;
    //public GameObject moneda;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            CoinManager.instance.IncrementCoinCount(cantidad);
            this.gameObject.SetActive(false);
        }
    }
}
