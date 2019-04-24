using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionSpriteGancho : MonoBehaviour {

    /*
     * Lo que hace es rotar al sprite del gancho para que salga bien girado dependiendo de hacia donde se mueva
     */ 

    public Transform gancho;
    Vector2 dir;
    Quaternion rot;
    bool izquierda = false;
    void Start () {
        dir = Vector2.right;
        rot = transform.rotation;

    }


    void Update () {
        if (gancho.GetComponent<MovGancho>().daEstado() == HookState.Quieto)
        {
            if (transform.GetComponentInParent<PlayerController>()!=null)//por si cuando estas enganchado saltas puede dar error
                dir = GetComponentInParent<PlayerController>().DevuelveDireccion(out izquierda);
            
            
            //transform.rotation = transform.parent.transform.rotation;
            transform.rotation = rot;
        }
        if (dir == Vector2.up)
        {
            
            transform.rotation = new Quaternion(0, 0, 0,1);//Rotacion del spritegancho
           // Debug.Log("Rotacion arriba de " + transform.name + " : " + transform.rotation);
            //transform.rotation = new Quaternion(rot.x, rot.y, transform.parent.transform.rotation.z, rot.w);
        }
        else if (dir == Vector2.down)
        {
            
            transform.rotation = new Quaternion(0, 0, 1, 0);//Rotacion del spritegancho
           // Debug.Log("Rotacion abajo de " + transform.name + " : " + transform.rotation);
            //transform.rotation = new Quaternion(rot.x, rot.y, transform.parent.transform.rotation.z, rot.w);
        }
        else if (dir == Vector2.right)
        {
            if (transform.parent.transform.rotation.y == 0)
            {
                //Debug.Log("Rotacion derecha de " + transform.name + " : " + transform.rotation);
                transform.rotation = new Quaternion(0.7f, 0.7f, 0, 0);//rotacion del sprite gancho

            }
            else if(izquierda)
            {
                //Debug.Log("Rotacion izquierda de " + transform.name + " : " + transform.rotation);
                transform.rotation = new Quaternion(-0.7f, 0.7f, 0, 0);//Rotacion del spritegancho

            }
        }
    }
}
