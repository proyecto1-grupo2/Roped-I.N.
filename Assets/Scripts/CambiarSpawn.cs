using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lo que hace es cambiar el spawn del agua a la posicion de este trigger cuando lo toca el player
public class CambiarSpawn : MonoBehaviour
{
    public GameObject agua;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { agua.GetComponent<DeadZone>().CambioSpawn(transform); }
    }
}
