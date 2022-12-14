using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letrero : MonoBehaviour
{

    public GameObject letrero;
    public TMPro.TextMeshProUGUI cuadroTexto;
    public string texto;
    public bool jugadorEnRango;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            letrero.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jugadorEnRango)
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
