using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Vida>())
        {
            //daño a enemigos
            if (other.gameObject.CompareTag("Enemy"))
            {
                
                    other.GetComponent<Vida>().LoseLife(damage);
                
            }
            //daño a jugador si no es inmune. Lo hacemos inmune 
            else if (other.gameObject.CompareTag("Player") && !other.gameObject.GetComponent<Vida>().DaInmune())
            {
                other.GetComponent<Vida>().LoseLife(damage);
                //transform.GetComponent<PlayerController>().EnemyKnockBack(transform.position.x);

                other.transform.GetComponent<Vida>().Inmune();
            }


        }
    }


}
