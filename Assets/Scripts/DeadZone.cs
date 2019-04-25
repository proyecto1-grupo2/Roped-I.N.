using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Transform spawnPoint;
    public int damage;
    int i = 0;//sirve para evitar un bug
    bool congelada = false;

    //si el Player, llama al metodo onDead de ese componente
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() && i == 0 && !congelada)
        {
            i = 1;
            other.GetComponent<Vida>().OnDeadZone(spawnPoint);
            other.GetComponent<Vida>().LoseLife(damage);
        }
        else if (other.CompareTag("QuimicoHielo"))
        {
            Destroy(other.gameObject);
        }

    }

    //Si no ponemos esto, ocurre un bug que el jugador pierde dos vidas si cae rapido al DeadZone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            i = 0;
        }
    }
    public void DeadZoneOnOff()
    {
        congelada = !congelada;
    }
    public void CambioSpawn(Transform newspawn)
    {
        spawnPoint = newspawn;
    }
}

