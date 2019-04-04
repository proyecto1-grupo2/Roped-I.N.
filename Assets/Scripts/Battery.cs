using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Activa un mecanismo (puerta) cuando la bateria se carga
public class Battery : MonoBehaviour {
    //Referencia de la puerta que queramos abrir
    public Door door; 
    public int cargaNecesaria;

    //Resta una carga a cargaNecesaria
    //cuando llega a 0 llama al método OpenDoor()
    //de la componente Door en la puerta correspondiente
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("QuimicoElectrico"))
        {
            cargaNecesaria--;
            if (cargaNecesaria <= 0)
            {
                door.OpenDoor();
            }
        }
    }
}
