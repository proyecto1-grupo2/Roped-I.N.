using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullChemical : MonoBehaviour
{

    Rigidbody2D rb;
    MovGancho hook;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (rb == null)
        {
            Debug.LogError("Falta RigidBody");
        }

    }

    
      //Cambia la rotacion del quimico cuando está enganchado para que siempre se vea recto
     
    void Update()
    {

        if (hook != null && hook.GetComponent<MovGancho>().daEstado() == HookState.Quieto)
        {
            if(transform.parent != null)
                rb.velocity = new Vector2(0, 0);//esto es para evitar un bug que el quimico se movía solo
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

                transform.rotation = new Quaternion(0, 0, 0, 1);

            }
        }


    }

    // metodo para detectar colision con el gancho del player y lo engancha
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gancho"))
        {
            hook = other.gameObject.GetComponent<MovGancho>();
            Enganche();
        }

    }


    //Hace hijo al quimico del spritegancho 
    public void Enganche()
    {
       if (hook.transform.GetChild(0).childCount == 0)
       {
           ModifyQuimico(true, hook.transform.GetChild(0));
       }
       else
       {
           ModifyQuimico(false, null);
       }
    }
    //Metodo auxiliar para hacer hijo al quimico del spritegancho (es el que más hace realmente)
    //Si está enganchado , se hace trigger y kinematic y pasa a ser hijo del sprite gancho(parent) y se le modifica su posicion para que quede mejor visualmente
    void ModifyQuimico(bool enganchado, Transform parent)
    {
        transform.GetComponent<BoxCollider2D>().isTrigger = enganchado;
        transform.SetParent(parent);
        transform.position = new Vector3(hook.transform.GetChild(0).position.x, hook.transform.GetChild(0).transform.position.y + 0.2f, transform.position.z);
        transform.GetComponent<Rigidbody2D>().isKinematic = enganchado;
    }

}
