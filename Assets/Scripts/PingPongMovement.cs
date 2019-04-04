using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour {

    public float speed;
    public Vector2 Ainicio, Bfin;
    Vector2 destino;
    
    void Start()
    {
        destino = new Vector2(1, 1);
    }

    void Update()
    {
        
            //movimiento (comprueba hacia qué punto hay que ir)
            if (transform.position.x >= Bfin.x)
            {
                destino = Ainicio;
            }

            else if (transform.position.x <= Ainicio.x)
            {
                destino = Bfin;
            }

            float step = speed * Time.deltaTime;

            // mover el sprite hacia la posición de destino
            transform.position = Vector2.MoveTowards(transform.position, destino, step);
        
    }
    
    //Cambia de direccion si colisiona con algo
    void OnCollisionEnter2D()
    {
        if (destino == Ainicio)
        {
            destino = Bfin;
        }
        else destino = Ainicio;
    }
    //Cambia de direccion si colisiona con algo
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Gancho"))
        {
            if (destino == Ainicio)
            {
                destino = Bfin;
            }
            else destino = Ainicio;
        }
      
    }
}
