using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionQuimicoPared : MonoBehaviour {

    //Hace que el quimico detecte colision con las paredes y el gancho vuelva
    //si el gancho esta quieto el quimico puede entrar en las paredes pero no puede disparar
    AtraeQuimicos quimico;
    private void Start()
    {
        //layerMask = LayerMask.GetMask("Gancho", "Terreno", "Enganches");
        quimico = this.GetComponent<AtraeQuimicos>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (transform.parent != null && LayerMask.LayerToName(other.gameObject.layer) == "Terreno")
        {
            //Debug.Log("hola");
            transform.parent.GetComponent<MovGancho>().cambiaEstado(HookState.Vuelta);

        }

    }
    
}

