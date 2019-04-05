using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CajaDestruible : MonoBehaviour {
    public GameObject Quimico;//Es el quimico que vamos a crear cuando se destruye la caja

    void Start()
    {
        if (Quimico == null)
        {
            Debug.Log("No sabe que quimico crear");
        }
    }

    //si colisiona el gancho con la caja se destruye e instancia un quimico en la posicion de la caja
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gancho"))
        {
            Destroy(gameObject);
            Instantiate(Quimico, transform.position, transform.rotation);
        }
    
    }
}
