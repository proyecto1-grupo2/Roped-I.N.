using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongelaAgua : MonoBehaviour {
    public float dragHielo/*, tCong*/;
    DeadZone dz;
    BoxCollider2D bc2D;

    //Al colisionar un quimico de hielo con una zona de agua
    //    desactiva la DeadZone, la vuelve solida y aplica un
    //    drag predefinido en el inspector
    private void Start()
    {
        dz = gameObject.GetComponent<DeadZone>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();    
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QuimicoHielo"))
        {    
            dz.DeadZoneOnOff();
            bc2D.isTrigger = false;
            gameObject.GetComponent<Rigidbody2D>().drag = dragHielo;
            Debug.Log("Drag " + gameObject.GetComponent<Rigidbody2D>().drag);
            //Invoke("FreezeOff", tCong);
        }
    }
    //Este método es llamado al cabo de tCong segundos para descongelar el agua
    //void FreezeOff()
    //{
    //    dz.DeadZoneOnOff();
    //    bc2D.isTrigger = true;
    //}
}
