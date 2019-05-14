using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    
    bool dead = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("IsDead", dead);
    }
    //Distingue el caso de la muerte de un enemigo spawneado y hace que los otros objetos mueran
    public void OnDead()
    {
        PursuitTarget oga = this.GetComponent<PursuitTarget>();
        PlayerController player = GetComponent<PlayerController>();
        if (oga != null && oga.GetGenerated()) //Podria especificarse que el padre es la ogaPool
        {
            oga.DecreaseCount();
            dead = true;
            Invoke("DeadFalse", 0.7f);
        }
        else
        {
            dead = true;
            Invoke("DeadFalse", 1f);            
        }       
    }
    void DeadFalse()
    {
        Destroy(this.gameObject);
        dead = false;
    }

    public bool GetDead()
    {
        return dead;
    }
}