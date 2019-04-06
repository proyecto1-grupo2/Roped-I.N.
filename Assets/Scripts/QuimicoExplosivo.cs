using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuimicoExplosivo : MonoBehaviour
{
    Animator ani;
    HookState estadoGancho;
    MovGancho estadoAct;
    public int daño;//el daño que causa a los enemigos
    bool dañar;//variable para poder hacer daño
    bool ida;//variable para soltar el quimico
    int vecesDañadoJug, vecesDañadoEne;//variables para causar daño solo una vez

    void Start()
    {
        dañar = false;
        ida = false;
        ani = GetComponent<Animator>();
        estadoAct = null;
        vecesDañadoJug = 0;
        vecesDañadoEne = 0;
    }

    //Coge el estado del gancho y llama a soltar el quimico
    void Update()
    {
        if (estadoAct != null && transform.parent)
        {
            estadoGancho = estadoAct.daEstado();
            SoltarQuimico();
        }
    }

    //Coge la componente MovGancho del gancho 
    private void OnTriggerEnter2D(Collider2D other)
    {

        //cogemos la referencia al gancho
        if (other.gameObject.CompareTag("Gancho"))
        {
            estadoAct = other.gameObject.GetComponent<MovGancho>();
        }
        //daño jugador
        else if (other.gameObject.CompareTag("Player") && dañar && vecesDañadoJug == 0)
        {
            other.gameObject.GetComponent<Vida>().LoseLife(1);
            vecesDañadoJug = 1;
        }
        //daño enemigo
        else if (other.gameObject.CompareTag("Enemy") && dañar && vecesDañadoEne == 0)
        {
            other.gameObject.GetComponent<Vida>().LoseLife(daño);
            vecesDañadoEne = 1;
        }
    }


    //destruccion Muro Quebradizo
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Quebradizo") && dañar)
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
            Destroy(gameObject.GetComponent<AtraeQuimicos>());
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
        vecesDañadoEne = 0;
        vecesDañadoJug = 0;
        dañar = false;
        ida = false;
    }

    //Cambia el tamaño del collider cuando inicia la explosion
    void CambiaTamCollider()
    {
        transform.GetComponent<BoxCollider2D>().size *= 2;
    }

}