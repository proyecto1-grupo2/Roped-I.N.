﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    PursuitTarget oga;
    bool dead = false, hurt;
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
        oga = this.GetComponent<PursuitTarget>();
        if (oga != null && oga.GetGenerated()) //Podria especificarse que el padre es la ogaPool
        {
            //Debug.Log("Soy un oga spawneado");
            oga.DecreaseCount();
            Destroy(this.gameObject);
        }
        else
        {
            dead = true;
            Invoke("DeadFalse", 2f);
            //Debug.Log("Soy otra cosa");
            
        }

        
    }
    void DeadFalse()
    {
        Destroy(this.gameObject);
        dead = false;
    }
}