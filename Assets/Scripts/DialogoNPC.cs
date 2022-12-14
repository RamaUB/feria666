using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogoNPC : MonoBehaviour
{

    public GameObject dialogoNpc;
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
            dialogoNpc.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jugadorEnRango)
        {
            if (dialogoNpc.activeInHierarchy)
            {
                dialogoNpc.SetActive(false);
            } 
            else
            {
                dialogoNpc.SetActive(true);
                cuadroTexto.text = texto;
            }
        }
    }
}
