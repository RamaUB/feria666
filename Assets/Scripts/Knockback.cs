using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{

    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pot>().Smash();
        }


        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {

                Vector2 difference = (hit.transform.position - transform.position);
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (collision.gameObject.CompareTag("enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }
                if (collision.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<MovimientoPlayer>().currentState = PlayerState.stagger;
                    collision.GetComponent<MovimientoPlayer>().Knock(knockTime);
                }
            }
        }
    }
}
