using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<Mole> moles;

    [Header("Objetos de UI")]
    [SerializeField] private GameObject botonPlay;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject textoSinTiempo;
    [SerializeField] private TMPro.TextMeshProUGUI textoTiempo;
    [SerializeField] private TMPro.TextMeshProUGUI textoPuntaje;
    [SerializeField] private TMPro.TextMeshProUGUI textoMoneda;
    [SerializeField] private GameObject botonVolver;

    [Header("Sonidos")]
    [SerializeField] private AudioSource errado;
    [SerializeField] private AudioSource moneda;

    [Header("Managers")]
    [SerializeField] private CoinManager coinManager;


    // Tiempo de juego.
    private float tiempoInicial = 30f;


    // variables grales
    private float tiempoRestante;
    private HashSet<Mole> molesActuales = new HashSet<Mole>();
    private int puntaje;
    private bool jugando = false;

    
    public void IniciarJuego()
    {
        botonPlay.SetActive(false);
        botonVolver.SetActive(false);
        textoSinTiempo.SetActive(false);
        gameUI.SetActive(true);

        for (int i = 0; i <moles.Count; i++)
        {
            moles[i].Esconder();
            moles[i].SetIndex(i);
        }

        molesActuales.Clear();

        tiempoRestante = tiempoInicial;
        puntaje = 0;
        textoPuntaje.text = "0";
        textoMoneda.text = coinManager.GetCoinCount().ToString();
        jugando = true;
    }
    


    public void SumaPuntos(int moleIndex)
    {
        puntaje += 1;
        textoPuntaje.text = $"{puntaje}";
        tiempoRestante += 1;
        moneda.Play();
        if(puntaje % 30 == 0)
        {
            coinManager.IncrementCoinCount();
            textoMoneda.text = coinManager.GetCoinCount().ToString();
        }

        molesActuales.Remove(moles[moleIndex]);
    }

    public void Errados(int moleIndex)
    {
        errado.Play();
        tiempoRestante -= 2;
        molesActuales.Remove(moles[moleIndex]);
    }

    public void GameOver(int type)
    {
        // Mostrar fuera de tiempo
            textoSinTiempo.SetActive(true);
        
        // esconder los moles que quedan
        foreach (Mole mole in moles)
        {
            mole.FinalizarJuego();
        }
        // Detener el juego y volver a mostrar el boton de jugar / volver al mundo
        
        jugando = false;
        botonPlay.SetActive(true);
        botonVolver.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (jugando)
        {
            // actualizar tiempo de juego.
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                GameOver(0);
            }
   
            textoTiempo.text = $"{(int)tiempoRestante / 60}:{(int)tiempoRestante % 60:D2}";
          
            
            // verificar si necesitamos mas moles
            if (molesActuales.Count <= (puntaje / 10))
            {
                // Elegir un mole aleatoriamente
                int index = Random.Range(0, moles.Count);
                // en caso que este haciendo algo, volvemos a intentar en la siguiente frame.
                if (!molesActuales.Contains(moles[index]))
                {
                    molesActuales.Add(moles[index]);
                    moles[index].Comenzar(puntaje / 10);
                }
            }
        }
    }
}
