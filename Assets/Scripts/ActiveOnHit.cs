using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Abre una puerta cuando el gancho entra en contacto con el botón que tiene esta componente

public class ActiveOnHit : MonoBehaviour {

    public Door door; //Referencia de la puerta que queramos abrir
    Animator anim;      

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<MovGancho>())
        {
            Pressed();
            door.OpenDoor();
        }
    }

    //Cambia la animación del botón al estar presionado
    public void Pressed() { anim.SetBool("Pressed", true); }
}
