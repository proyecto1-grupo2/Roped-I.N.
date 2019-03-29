using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraeQuimicos : MonoBehaviour {
    
    public LayerMask Mask;
    Rigidbody2D Cajarb;
    Animator ani;
  

    bool colision;
    public Transform target;

   
    void OnTriggerEnter2D(Collider2D other)// metodo para detectar colision
    {
        // condicion para detectar si colisiona con el gancho del player y 
        
        MovGancho hook = other.gameObject.GetComponent<MovGancho>();
        if (hook != null)
        {

            // this.transform.SetParent(hook.transform);
            colision = true;
            hook.cambiaEstado(HookState.Vuelta);
            transform.GetComponent<BoxCollider2D>().isTrigger = true;

        }
        else colision = false;
        
    }
    void Start()
    {
        ani = GetComponent<Animator>();
        Cajarb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (Cajarb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        colision = false;

    }


    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.left) * 0.5f, Color.black); // "
        if (colision)
        {
            transform.SetParent(target.transform);
            //Debug.Log("siiiii");
            transform.GetComponent<Rigidbody2D>().isKinematic = true;
            
        }
        /*else
        {
            transform.GetComponent<Rigidbody2D>().isKinematic = false;
        }*/

    }
    private void FixedUpdate() //metodo para cuando colision es cierta cambie la posicion de la caja al gancho
    {
        ////Detectar si ha llegado hasta el jugador, si es cierto se desactiva el booleano colision
        //if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), 1, Mask) ||
        //   Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1, Mask))
        //{
        //    colision = false;
        //    //transform.SetParent(Padreaux.transform);


        //}


    }
}
