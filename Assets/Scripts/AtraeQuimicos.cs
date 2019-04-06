﻿using System.Collections;
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
            //Debug.Log("enganchado");
            hook = other.gameObject.GetComponent<MovGancho>();
            colision = true;
            //EnganchaQuimico();
            Enganche();
        }

        else if (hook != null)//para que cuando el gancho suelte al quimico, el gancho vuelva
        {
            hook.cambiaEstado(HookState.Vuelta);
        }
       
        else colision = false;
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (hook!=null && other.gameObject.CompareTag("Suelo"))
        {
            hook.cambiaEstado(HookState.Vuelta);
        }
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


    //void Update()
    //{

    //    /*
    //     *Si un quimico colisiona con el gancho se hace hijo del gancho
    //     * para ello es necesario que el gancho no tenga hijos(no tenga mas quimicos)
    //     */ 
    //    if (colision)
    //    {
    //        if (hook.transform.childCount == 0)
    //        {
    //            transform.GetComponent<BoxCollider2D>().isTrigger = true;
    //            transform.SetParent(target.transform);
    //            transform.GetComponent<Rigidbody2D>().isKinematic = true;
    //        }
    //        else colision = false;
            
    //        //hook.cambiaEstado(HookState.Vuelta);

    //    }
       

    //    /*else
    //    {
    //        transform.GetComponent<Rigidbody2D>().isKinematic = false;
    //    }*/

    //}

    public void Enganche()
    {
       if (hook.transform.childCount == 0)
       {
            Debug.Log("enganchado");
           ModifyQuimico(true, hook.transform);
       }
       else
       {
           ModifyQuimico(false, null);
       }
    }

    void ModifyQuimico(bool enganchado, Transform parent)
    {
        transform.GetComponent<BoxCollider2D>().isTrigger = enganchado;
        transform.SetParent(parent);
        transform.GetComponent<Rigidbody2D>().isKinematic = enganchado;
    }
    //void EnganchaQuimico()
    //{
    //    if (hook.transform.childCount == 0)
    //    {
    //        transform.GetComponent<BoxCollider2D>().isTrigger = true;
    //        transform.SetParent(target.transform);
    //        transform.GetComponent<Rigidbody2D>().isKinematic = true;
    //    }
    //}
    
    //public void DesenganchaQuimico()
    //{
    //    transform.GetComponent<BoxCollider2D>().isTrigger = false;
    //    transform.parent = null;
    //    transform.GetComponent<Rigidbody2D>().isKinematic = false;
    //}
}
