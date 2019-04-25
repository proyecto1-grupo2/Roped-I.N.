using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongelaAgua : MonoBehaviour
{
    //public float dragHielo, tCong;
    DeadZone dz;
    BoxCollider2D bc2D;
    Animator anim;

    //Al colisionar un quimico de hielo con una zona de agua
    //    desactiva la DeadZone, la vuelve solida y aplica un
    //    drag predefinido en el inspector
    private void Start()
    {
        dz = gameObject.GetComponent<DeadZone>();
        bc2D = gameObject.GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("QuimicoHielo"))
        {
            dz.DeadZoneOnOff();
            bc2D.isTrigger = false;
            //gameObject.GetComponent<Rigidbody2D>().drag = dragHielo;
            Destroy(other.gameObject);
            anim.SetBool("Congelada", true);
            SoundManager.instance.CallSoundManager("hielo");
            Debug.Log("Congelada");
            //Cambiar sprite (De momento solo cambia el tono a un más azulado)
            //SpriteRenderer sprit = gameObject.GetComponent<SpriteRenderer>();
            //sprit.material.color = Color.blue;
            
            //Debug.Log("Drag " + gameObject.GetComponent<Rigidbody2D>().drag);
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