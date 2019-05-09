using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionSpriteGancho : MonoBehaviour {

    /*
     * Extiende la cadena según la posición del gancho
     * 
     */ 

    public Transform gancho;
    public SpriteRenderer cadena;
    public Transform ptoLanzamiento;


    void Update () {
        cadena.size = new Vector2(cadena.size.x, - Vector2.Distance(ptoLanzamiento.position, gancho.position));
    }

}
