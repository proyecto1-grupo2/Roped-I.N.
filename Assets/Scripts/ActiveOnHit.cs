using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnHit : MonoBehaviour {

    public Door door; //Referencia de la puerta que queramos abrir
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MovGancho>())
        {
                door.OpenDoor();
        }
    }
}
