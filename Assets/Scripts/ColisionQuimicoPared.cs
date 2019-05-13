using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionQuimicoPared : MonoBehaviour {

    //Hace que el quimico detecte colision con las paredes y el gancho vuelva
    //si el gancho esta quieto el quimico puede entrar en las paredes pero no puede disparar

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (transform.parent != null  && (other.CompareTag("Suelo") || other.CompareTag("Enganches")))
        {
            //Debug.Log("hola");
            if (transform.parent.parent.GetComponent<MovGancho>() != null)
            {
                //Debug.Log("adios");
                transform.parent.parent.GetComponent<MovGancho>().cambiaEstado(HookState.Vuelta);
            }
                

        }


    }
    
}

