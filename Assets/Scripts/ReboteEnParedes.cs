using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReboteEnParedes : MonoBehaviour
{
    //Diferencia las colisiones con enganches y el resto de paredes

    MovGancho gancho;
    private void Start()
    {

        gancho = this.GetComponent<MovGancho>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enganches"))
        {
            gancho.cambiaEstado(HookState.Enganchado);
            gancho.Player.CambiaEstado(true);
        }
        else gancho.cambiaEstado(HookState.Vuelta);


    }
}