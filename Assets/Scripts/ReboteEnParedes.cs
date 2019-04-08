using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboteEnParedes : MonoBehaviour
{
    //Diferencia las colisiones con paredes normales y con paredes pegajosas
    int layerMask;
    MovGancho gancho;
    private void Start()
    {
        layerMask = LayerMask.GetMask("Gancho", "Terreno", "Enganches");
        gancho = this.GetComponent<MovGancho>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (LayerMask.LayerToName(other.gameObject.layer) == "Enganches")
        {
            gancho.cambiaEstado(HookState.Enganchado);
        }
        else gancho.cambiaEstado(HookState.Vuelta);

    }
}