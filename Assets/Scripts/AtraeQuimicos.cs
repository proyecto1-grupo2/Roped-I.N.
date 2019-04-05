using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraeQuimicos : MonoBehaviour {
    
    public LayerMask Mask;
    Rigidbody2D Cajarb;
    //Animator ani;
    MovGancho hook;

    bool colision;
    public Transform target;

   
    void OnTriggerEnter2D(Collider2D other)// metodo para detectar colision
    {
        // condicion para detectar si colisiona con el gancho del player y 
        if (other.gameObject.CompareTag("Gancho") )
        {
            hook = other.gameObject.GetComponent<MovGancho>();
            colision = true;
        }

        else if (hook != null)//para que cuando el gancho suelte al quimico, el gancho vuelva
        {
            hook.cambiaEstado(HookState.Vuelta);
        }
       
        else colision = false;
        
    }


    void Start()
    {
        //ani = GetComponent<Animator>();
        Cajarb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (Cajarb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        if (target == null)
        {
            Debug.Log("No encuentro al gancho");
        }
        colision = false;

    }


    void Update()
    {
        /*
         *Si un quimico colisiona con el gancho se hace hijo del gancho
         * para ello es necesario que el gancho no tenga hijos(no tenga mas quimicos)
         */ 
        if (colision)
        {
            if (hook.transform.childCount==0) {
                transform.GetComponent<BoxCollider2D>().isTrigger = true;
                transform.SetParent(target.transform);
                transform.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            
            //hook.cambiaEstado(HookState.Vuelta);

        }
       

        /*else
        {
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
        }*/

    }
    
}
