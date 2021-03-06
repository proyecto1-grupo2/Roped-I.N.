﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;
    bool muerto = false;
    Death dead;

    private void Start()
    {
        dead = this.GetComponent<Death>();
    }

    private void FixedUpdate()
    {
        if (this.GetComponent<MovGancho>() == null && dead!=null)
            muerto = dead.GetDead();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Vida>())
        {
            if (muerto == true)
            {
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.GetComponent<Collider2D>(), true);
            }
            else {
                //daño a enemigos
                if (other.gameObject.CompareTag("Enemy") && this.GetComponent<MovGancho>() != null && this.GetComponent<MovGancho>().daEstado() != HookState.Quieto)
                {
                        SoundManager.instance.CallSoundManager("enemyDamage");
                        other.GetComponent<Vida>().LoseLife(damage);
                }
                //daño a jugador si no es inmune. Lo hacemos inmune 
                else if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<Vida>().DaInmune())
                {
                    other.GetComponent<Vida>().LoseLife(damage);
                    other.transform.GetComponent<Vida>().Inmune();
                }
            }
            
        }
    }
    
}
