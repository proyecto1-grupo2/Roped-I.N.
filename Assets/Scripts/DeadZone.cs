using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform spawnPoint;
    public int damage;
    bool congelada = false;

    //si es Player, llama al metodo onDead de ese componente,
    //si es un quimico, lo destruye (comprueba que esté en la layer Químicos que es la 16)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && !congelada )
        {
            other.GetComponent<Vida>().OnDeadZone(spawnPoint);
            other.GetComponent<Vida>().LoseLife(damage);
        }

        else if (other.gameObject.layer == 16)
        {
            Destroy(other.gameObject);
        }


    }
    //cuando se congela
    public void DeadZoneOnOff()
    {
        congelada = !congelada;
    }
    //para cambiar el punto de respawn
    public void CambioSpawn(Transform newspawn)
    {
        spawnPoint = newspawn;
    }
}

