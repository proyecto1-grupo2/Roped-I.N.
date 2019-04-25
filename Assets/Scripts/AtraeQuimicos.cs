using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtraeQuimicos : MonoBehaviour
{

    public LayerMask Mask;
    Rigidbody2D Cajarb;
    //Animator ani;
    MovGancho hook;

    bool izquierda;

    bool colision;
    //public Transform target;


    void OnTriggerEnter2D(Collider2D other)// metodo para detectar colision
    {
        // condicion para detectar si colisiona con el gancho del player y 
        if (other.gameObject.CompareTag("Gancho"))
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
    /*void OnCollisionEnter2D(Collision2D other)
    {
        if (hook != null && other.gameObject.CompareTag("Suelo"))
        {
            hook.cambiaEstado(HookState.Vuelta);
        }
    }*/

    void Start()
    {
        //ani = GetComponent<Animator>();
        Cajarb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (Cajarb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        /*if (target == null)
        {
            Debug.Log("No encuentro al gancho");
        }*/
        colision = false;

    }

    void Update()
    {

        if (hook != null && hook.GetComponent<MovGancho>().daEstado()==HookState.Quieto)
        {
            Vector2 dir = hook.transform.parent.GetComponent<PlayerController>().DevuelveDireccion();
            if (dir == Vector2.up)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
            }
            else if (dir == Vector2.down)
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else if (dir == Vector2.right)
            {
                
                transform.rotation = new Quaternion(0, 0, 0, 1);//Rotacion del spritegancho

            }
        }
        

    }

    
    //Hace hijo al quimico del spritegancho 
    public void Enganche()
    {
       if (hook.transform.GetChild(0).childCount == 0)
       {
           //Debug.Log("enganchado");
           ModifyQuimico(true, hook.transform.GetChild(0));
       }
       else
       {
           ModifyQuimico(false, null);
       }
    }
    //Metodo auxiliar para hacer hijo al quimico del spritegancho (es el que más hace realmente)
    void ModifyQuimico(bool enganchado, Transform parent)
    {
        transform.GetComponent<BoxCollider2D>().isTrigger = enganchado;
        transform.SetParent(parent);
        //este es mejor visualmente pero da problemas cuando coges el quimico cuando el gancho esta en vuelta
        //transform.position = new Vector3(transform.position.x , transform.position.y + 0.9f, transform.position.z); 
        transform.position = new Vector3(hook.transform.GetChild(0).position.x, hook.transform.GetChild(0).transform.position.y + 0.2f, transform.position.z);


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
