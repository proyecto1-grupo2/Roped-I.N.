using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Opened", false);
    }
    //La puerta bloquea una zona; para abrirla, se desactiva
    public void OpenDoor()
    {
        anim.SetBool("Opened", true);
    }

    public void Deactivate() { this.gameObject.SetActive(false); }
}
