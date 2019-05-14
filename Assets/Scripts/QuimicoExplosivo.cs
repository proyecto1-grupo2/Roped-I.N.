using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuimicoExplosivo : MonoBehaviour
{
    public GameObject explosion;
    Animator ani;
    HookState estadoGancho;
    MovGancho estadoAct;
    public int daño;//el daño que causa a los enemigos
    bool ida;//variable para soltar el quimico

    void Start()
    {
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

    }

    //Coge la componente MovGancho del gancho 
    private void OnTriggerEnter2D(Collider2D other)
    {

        //cogemos la referencia al gancho
        if (other.gameObject.CompareTag("Gancho"))
        {
            estadoAct = other.gameObject.GetComponent<MovGancho>();
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
        }
        /*
         * la segunda vez que vuelve. Deshacemos los cambios que hace el componente AtraeQuimico 
         * y además lo desactivamos para que no pueda mover el quimico mientras explota
         * por ultimo se ejecuta la animacion de explosion
         */
        else if ((ida && estadoGancho == HookState.Vuelta) || transform.parent==null)
        {
            //Debug.Log("Soltado");
            ida = false;
            transform.parent = null;
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.GetComponent<BoxCollider2D>().isTrigger = false;
            Destroy(gameObject.GetComponent<PullChemical>());
            Invoke("InstantiateExplosion", 2);
        }
        //Debug.Log("Current State: " + estadoGancho);
    }

    private void InstantiateExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }

}