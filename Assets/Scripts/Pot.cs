using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    private Animator anim;
    public GameObject moneda;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Smash()
    {
        anim.SetBool("smash", true);
        StartCoroutine(breakCo());
        moneda.gameObject.SetActive(true);
    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
        
    }
}
