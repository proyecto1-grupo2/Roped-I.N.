using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuimicoExplosivo : MonoBehaviour
{
    Animator ani;
    HookState estadoGancho;
    MovGancho estadoAct;
    public int daño;
    bool dañar;
    bool ida;


    void Start()
    {
        dañar = false;
        ida = false;
        ani = GetComponent<Animator>();
        estadoAct = null;
    }

    //Coge el estado del gancho y llama a soltar el quimico
    void Update()
    {
        if (estadoAct != null)
        {
            estadoGancho = estadoAct.daEstado();
            SoltarQuimico();
        }
        
           // Debug.Log("perdida");
        
       
    }

    //Coge la componente MovGancho del gancho
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    estadoAct = other.gameObject.GetComponent<MovGancho>();
    //    Debug.Log("Cogido");

    //}
  
    




    //Causa daño a los enemigos
    void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") && dañar))
        {
            other.gameObject.GetComponent<Vida>().LoseLife(daño);
        }
        else if ((other.gameObject.CompareTag("Player") && dañar))
        {
            other.gameObject.GetComponent<Vida>().LoseLife(1);
        }
        else if (other.gameObject.CompareTag("Quebradizo") && dañar)
        {
            //Debug.Log("adio655s");
            Destroy(other.gameObject);
        }

    }
  

    /*
     * Suelta el quimico cuando el gancho vuelve por segunda vez
     *(cuando coge el quimico el gancho vuelve, luego cuando usa el gancho cambia a ida 
     * y cuando vuelve suelta el quimico)
     */
    void SoltarQuimico()
    {
        //la primera vez que va
        if (estadoGancho == HookState.Ida && !ida)
        {
            
            ida = true;
            //Debug.Log(ida);
        }
        /*
         * la segunda vez que vuelve. Deshacemos los cambios que hace el componente AtraeQuimico 
         * y además lo desactivamos para que no pueda mover el quimico mientras explota
         * por ultimo se ejecuta la animacion de explosion
         */
        else if (ida && estadoGancho == HookState.Vuelta)
        {
            //Debug.Log("Soltado");
            ida = false;
            transform.parent = null;
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<AtraeQuimicos>().enabled = false;
            Invoke("Explosion", 2);
        }
        //Debug.Log("Current State: " + estadoGancho);
    }

    //se activa la animacion de explosion y el poder hacer daño
    void Explosion()
    {
        ani.SetBool("Contacto", true);
        dañar = true;

    }

    //se destruye al terminar la animacion
    void Destruccion()
    {
        Destroy(this.gameObject);
    }

    //Cambia el tamaño del collider cuando inicia la explosion
    void CambiaTamCollider()
    {
        transform.GetComponent<BoxCollider2D>().size *= 2;
    }
    
}