using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Activa un mecanismo (puerta) cuando una caja cae sobre el objeto que tenga esta componente

public class ActiveOnTop : MonoBehaviour {
    public Door door; //Referencia de la puerta que queramos abrir
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    //Comprueba que la colisión ha sido en la parte superior del botón y con la caja
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Caja"))
        {
            ContactPoint2D p = other.GetContact(0);
            if (Mathf.Abs(Vector2.Angle(p.normal, transform.up)) - 180 < 0.1f)
            {
                Pressed();
                door.OpenDoor();
            }
        }
    }

    //Cambia la animación del botón al estar presionado
    public void Pressed() { anim.SetBool("Pressed", true); }
}
