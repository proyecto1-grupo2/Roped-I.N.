using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void Pressed() { anim.SetBool("Pressed", true); }
}
