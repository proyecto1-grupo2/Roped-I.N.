using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Activa un mecanismo (puerta) cuando una caja cae sobre el objeto
public class ActiveOnTop : MonoBehaviour {
    public Door door; //Referencia de la puerta que queramos abrir
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            ContactPoint2D p = other.GetContact(0);
            if (Mathf.Abs(Vector2.Angle(p.normal, transform.up)) - 180 < 0.1f) //Comprueba que la colisión ha sido en la parte superior del botón
            {
                Pressed();
                door.OpenDoor();
            }
        }
    }

    public void Pressed() { anim.SetBool("Pressed", true); }
}
