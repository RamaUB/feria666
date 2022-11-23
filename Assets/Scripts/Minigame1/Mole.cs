using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    //Sprites asignables segun estado del Mole
    [Header("Sprites de los Moles")]
    [SerializeField] private Sprite mole;
    [SerializeField] private Sprite moleHit;
    [SerializeField] private Sprite moleHat;
    [SerializeField] private Sprite moleHatCrack;
    [SerializeField] private Sprite moleHatHit;

    [Header("GameManager")]
    [SerializeField] private GameManager gameManager;

    //Posiciones principales del Mole: Inicial (oculto) / Final (visible)
    private Vector2 posicionInicial = new Vector2(0.09f, -1.40f);
    private Vector2 posicionFinal = new Vector2(0.09f, 0.8f);
    
    //config de tiempos en que el Mole está visible y tiempo hasta que se oculta 
    private float periodoVisible = 0.5f;
    private float tiempoEspera = 1f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector2 colliderBoxOffset;
    private Vector2 tamanoColliderBox;
    private Vector2 colliderBoxOffsetEscondido;
    private Vector2 tamanoColliderBoxEscondido;

    //Parametros del Mole
    private bool atacable = true;
    //Enum para los dos tipos de Mole: normal y mas duro, con sombrero
    public enum TipoDeMole { Normal, Duro};
    private TipoDeMole tipoDeMole;
    private float tasaAparicionDuro = 0.25f;
    private int vidas;
    private int moleIndex = 0;

    public void SetIndex(int index)
    {
        moleIndex = index;
    }

    private IEnumerator MostrarOcultar(Vector2 inicio, Vector2 fin)
    {

        transform.localPosition = inicio;

        //loop para mostrar el Mole
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < periodoVisible)
        {
            transform.localPosition = Vector2.Lerp(inicio, fin, tiempoTranscurrido / periodoVisible);

            boxCollider2D.offset = Vector2.Lerp(colliderBoxOffsetEscondido, colliderBoxOffset, tiempoTranscurrido / periodoVisible);
            boxCollider2D.size = Vector2.Lerp(tamanoColliderBoxEscondido, tamanoColliderBox, tiempoTranscurrido / periodoVisible);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        //posicion final + collider
        transform.localPosition = fin;
        boxCollider2D.offset = colliderBoxOffset;
        boxCollider2D.size = tamanoColliderBox;

        yield return new WaitForSeconds(tiempoEspera);

        //loop para esconder el Mole
        tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < periodoVisible)
        {
            transform.localPosition = Vector2.Lerp(fin, inicio, tiempoTranscurrido / periodoVisible);

            boxCollider2D.offset = Vector2.Lerp(colliderBoxOffset, colliderBoxOffsetEscondido, tiempoTranscurrido / periodoVisible);
            boxCollider2D.size = Vector2.Lerp(tamanoColliderBox, tamanoColliderBoxEscondido, tiempoTranscurrido / periodoVisible);

            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        //posicion inicial + collider
        transform.localPosition = inicio;
        boxCollider2D.offset = colliderBoxOffsetEscondido;
        boxCollider2D.size = tamanoColliderBoxEscondido;

        if (atacable)
        {
            atacable = false;
            gameManager.Errados(moleIndex);
        }
    }


    private void OnMouseDown()
    {
        if (atacable)
        {
            switch (tipoDeMole)
            {
                case TipoDeMole.Normal:
                    spriteRenderer.sprite = moleHit;
                    gameManager.SumaPuntos(moleIndex);
                    StopAllCoroutines();
                    StartCoroutine(HacerDesaparecer());
                    atacable = false;
                    break;
                case TipoDeMole.Duro:
                    if (vidas == 2)
                    {
                        spriteRenderer.sprite = moleHatCrack;
                        vidas--;
                    } else
                    {
                        spriteRenderer.sprite = moleHatHit;
                        gameManager.SumaPuntos(moleIndex);
                        StopAllCoroutines();
                        StartCoroutine(HacerDesaparecer());
                        atacable = false;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator HacerDesaparecer()
    {
        yield return new WaitForSeconds(0.25f);

        if (!atacable)
        {
            Esconder();
        }
    }

    public void Esconder()
    {
        transform.localPosition = posicionInicial;
        boxCollider2D.offset = colliderBoxOffsetEscondido;
        boxCollider2D.size = tamanoColliderBoxEscondido;

    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        
        //config collider del sprite para que se pueda achicar cuando se esconde el mole
        colliderBoxOffset = boxCollider2D.offset;
        tamanoColliderBox = boxCollider2D.size;
        colliderBoxOffsetEscondido = new Vector2(colliderBoxOffset.x, -posicionInicial.y / 3f);
        tamanoColliderBoxEscondido = new Vector2(tamanoColliderBox.x, 0f);
    }

    public void Comenzar(int nivel)
    {
        SetNivel(nivel);
        Spawn();
        StartCoroutine(MostrarOcultar(posicionInicial, posicionFinal));
    }

    private void Spawn()
    {
        float random = Random.Range(0f, 1f);
        if(random < tasaAparicionDuro)
        {
            tipoDeMole = TipoDeMole.Duro;
            spriteRenderer.sprite = moleHat;
            vidas = 2;
        } else
        {
            tipoDeMole = TipoDeMole.Normal;
            spriteRenderer.sprite = mole;
            vidas = 1;
        }
        atacable = true;
    }

    private void SetNivel(int nivel)
    {
        //incrementar la aparicion de Moles con sombrero
        tasaAparicionDuro = Mathf.Min(nivel * 0.025f, 1f);

        //Incremento de velocidad a medida que avanzamos en nivel
        float tiempoEsperaMin = Mathf.Clamp(1 - nivel * 0.1f, 0.01f, 1f);
        float tiempoEsperaMax = Mathf.Clamp(2 - nivel * 0.1f, 0.01f, 2f);
        tiempoEspera = Random.Range(tiempoEsperaMin, tiempoEsperaMax);

    }

    public void FinalizarJuego()
    {
        atacable = false;
        StopAllCoroutines();
    }

}
