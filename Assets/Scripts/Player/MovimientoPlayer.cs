using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class MovimientoPlayer : MonoBehaviour
{
    public PlayerState currentState;
    public float velocidad;
    private Rigidbody2D myRigidBody;
    private Vector3 cambio;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {
            StartCoroutine(AttackCo());
        }
    }
    void FixedUpdate()
    {
        cambio = Vector3.zero;
        cambio.x = Input.GetAxisRaw("Horizontal");
        cambio.y = Input.GetAxisRaw("Vertical");
       
        if(currentState == PlayerState.walk)
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
        myRigidBody.MovePosition(
            transform.position + cambio * velocidad * Time.deltaTime
        );
    }

}
