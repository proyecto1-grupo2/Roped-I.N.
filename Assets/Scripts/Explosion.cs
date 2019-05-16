using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rompe la pared quebradiza y se destruye tras haber explotado
public class Explosion : MonoBehaviour {


	void Start () {
        SoundManager.instance.CallSoundManager("explosivo");
    }
	
    //destruye el muro 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Quebradizo"))
        {
            Destroy(other.gameObject);
        }
    }

    //Se invoca tras la animación
    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
