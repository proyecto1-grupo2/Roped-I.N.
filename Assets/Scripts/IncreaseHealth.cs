using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    // condicion para detectar si colisiona con el gancho del player y incrementar su vida
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.CallSoundManager("salud");
            int vida = other.gameObject.GetComponent<Vida>().vida;
            GameManager.instance.PlayerGanaVida(vida);
            if (other.gameObject.GetComponent<Vida>().vida <3) {
                other.gameObject.GetComponent<Vida>().vida += 1;
            }
            Destroy(this.gameObject);
            
        }
        

    }
  
}




