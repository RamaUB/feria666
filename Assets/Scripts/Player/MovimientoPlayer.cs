using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class MovimientoPlayer : MonoBehaviour
{
    public PlayerState currentState;
    public float velocidad;
    private Rigidbody2D myRigidBody;
    private Vector3 cambio;
    private Animator animator;
    public FloatValue currentHealth;
    public Signaler playerHealthSignal;
    public VectorValue startingPosition;

    [SerializeField] private TMPro.TextMeshProUGUI textoMoneda;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame

    private void Update()
    {
        textoMoneda.text = CoinManager.instance.GetCoinCount().ToString();
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
    }
    void FixedUpdate()
    {
        cambio = Vector3.zero;
        cambio.x = Input.GetAxisRaw("Horizontal");
        cambio.y = Input.GetAxisRaw("Vertical");
       
        if(currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimYMovimiento();
        }
        
        
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimYMovimiento()
    {
        if (cambio != Vector3.zero)
        {
            MoverPlayer();
            animator.SetFloat("moveX", cambio.x);
            animator.SetFloat("moveY", cambio.y);
            animator.SetBool("movimiento", true);
        }
        else
        {
            animator.SetBool("movimiento", false);
        }
    }
    void MoverPlayer()
    {
        cambio.Normalize();
        myRigidBody.MovePosition(
            transform.position + cambio * velocidad * Time.deltaTime
        );
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();

        if (currentHealth.RuntimeValue > 0) 
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }


}
