using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {
    public int damage;
    bool isInmune = false;
    public float tiempoinmune;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Vida>())
        {
            if (!isInmune)
            {   if(gameObject.CompareTag("Enemy") && gameObject.GetComponent<ImpQuimicos>().daEstado()!=EnemyState.Congelado )
                other.GetComponent<Vida>().LoseLife(damage);
                //transform.GetComponent<PlayerController>().EnemyKnockBack(transform.position.x);
                isInmune = true;

                Invoke("noInmune", tiempoinmune);
            }
        }
    }

    void noInmune()
    {
        isInmune = false;
    }
}
