using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemWallCollision : MonoBehaviour {

    //Hace que el quimico, cuando esté enganchado, detecte colision con las paredes y el gancho vuelva.
    //Si el gancho esta quieto el quimico puede entrar en las paredes pero no puede disparar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent != null  && (other.CompareTag("Suelo") || other.CompareTag("Enganches")))
        {
            if (transform.parent.parent.GetComponent<MovGancho>() != null)
            {
                transform.parent.parent.GetComponent<MovGancho>().cambiaEstado(HookState.Vuelta);
            }  
        }
    }    
}