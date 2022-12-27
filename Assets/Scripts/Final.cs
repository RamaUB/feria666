using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject final;
    public TMPro.TextMeshProUGUI cuadroTexto;
    public string texto;
    public bool jugadorEnRango;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.isTrigger)
        {
            jugadorEnRango = true;
            EnRango();

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            EnRango();
            jugadorEnRango = false;
        }
    }

    private void EnRango()
    {
        if (jugadorEnRango)
        {
            if (final.activeInHierarchy)
            {
                final.SetActive(false);
            }
            else
            {
                final.SetActive(true);
                cuadroTexto.text = texto;
            }
        }
    }

}
