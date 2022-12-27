using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salida : MonoBehaviour
{
    public GameObject andaPaya;
    public GameObject letrero;
    public TMPro.TextMeshProUGUI cuadroTexto;
    public string texto;
    public bool jugadorEnRango;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            jugadorEnRango = true;

            if (CoinManager.instance.GetCoinCount() >= 100)
            {
                andaPaya.SetActive(false);
            }
            else
            {
                andaPaya.SetActive(true);
                EnRango();

            }

            

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            EnRango();
            jugadorEnRango = false;
            letrero.SetActive(false);
        }
    }

    private void EnRango()
    {
        if ( jugadorEnRango)
        {
            if (letrero.activeInHierarchy)
            {
                letrero.SetActive(false);
            }
            else
            {
                letrero.SetActive(true);
                cuadroTexto.text = texto;
            }
        }
    }

}
