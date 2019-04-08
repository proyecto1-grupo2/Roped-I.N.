using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Activa un mecanismo (puerta) cuando una caja cae sobre el objeto
public class ActiveOnTop : MonoBehaviour {
    public Door door; //Referencia de la puerta que queramos abrir
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            ContactPoint2D p = other.GetContact(0);
            if (Mathf.Abs(Vector2.Angle(p.normal, transform.up)) - 180 < 0.1f) //Comprueba que la colisión ha sido en la parte superior del botón
            {
                door.OpenDoor();
            }
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("Caja")) door.OpenDoor();
    //}
}
