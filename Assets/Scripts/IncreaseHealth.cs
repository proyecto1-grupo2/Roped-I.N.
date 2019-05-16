using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealth : MonoBehaviour
{
    // Si el quimico colisiona con el jugador, le suma una vida y destruye el quimico
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.CallSoundManager("salud");
            int vida = other.gameObject.GetComponent<Vida>().vida;
            GameManager.instance.PlayerGetLife(vida);
            if (other.gameObject.GetComponent<Vida>().vida <3) {
                other.gameObject.GetComponent<Vida>().vida += 1;
            }
            Destroy(this.gameObject);
            
        }
        

    }
  
}




