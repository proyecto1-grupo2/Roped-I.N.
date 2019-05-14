using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeWater : MonoBehaviour
{
    DeadZone dz;
    BoxCollider2D bc2D;
    Animator anim;

    //Al colisionar un quimico de hielo con una zona de agua
    //    desactiva la DeadZone, la vuelve solida y cambia su animacion
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
            Destroy(other.gameObject);
            anim.SetBool("Congelada", true);
            SoundManager.instance.CallSoundManager("hielo");
        }
    }
    
}