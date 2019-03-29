using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform spawnPoint;
    public int damage;
    //si el objeto que activa el trigger tiene un componente PlayerDead, llama al metodo onDead de ese componente
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
           
            other.GetComponent<Vida>().OnDeadZone(spawnPoint);
            other.GetComponent<Vida>().LoseLife(damage);
            
        }

        //esto seria por si la caja toca el agua para destruirla, no es necesario pero por si ocurre algun fallo
        /*else if (other.GetComponent<ArrastraCaja>())
        {
            Destroy(other.gameObject);
        }*/
    }
}

