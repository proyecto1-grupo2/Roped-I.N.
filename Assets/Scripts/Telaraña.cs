using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telaraña : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("QuimicoFuego"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            MovGancho mov = other.GetComponentInParent<MovGancho>();
            if (mov != null) mov.cambiaEstado(HookState.Vuelta);
            
        }

       

    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        
    }
    
}
