using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaVida : MonoBehaviour
{
  
    void OnTriggerEnter2D(Collider2D other)// metodo para detectar colision
    {
        // condicion para detectar si colisiona con el gancho del player y 
        //que además el componente daño esté desactivado (lo que implica que esta usando el gancho de agarre)
        
        if (other.gameObject.CompareTag("Player")) 
        {
            int vida = other.gameObject.GetComponent<Vida>().vida;
            GameManager.instance.PlayerGanaVida(vida);
            if (other.gameObject.GetComponent<Vida>().vida <3) {
                other.gameObject.GetComponent<Vida>().vida += 1;
            }
            Destroy(this.gameObject);
            
        }
        

    }
  
}




