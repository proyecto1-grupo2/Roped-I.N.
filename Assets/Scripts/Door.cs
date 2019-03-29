using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    //La puerta bloquea una zona; para abrirla, se desactiva
    public void OpenDoor()
    {
        this.gameObject.SetActive(false);
    }
}
