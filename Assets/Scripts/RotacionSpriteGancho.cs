using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionSpriteGancho : MonoBehaviour {

    /*
     * Lo que hace es rotar al sprite del gancho para que salga bien girado dependiendo de hacia donde se mueva
     */ 

    public Transform gancho;
    public SpriteRenderer cadena;
    public Transform ptoLanzamiento;
    Vector2 dir;
    Quaternion rot;
    bool izquierda = false;
    void Start () {
        dir = Vector2.right;
        rot = transform.rotation;

    }


    void Update () {
        cadena.transform.position = ptoLanzamiento.position;
        //if (gancho.GetComponent<MovGancho>().daEstado() == HookState.Quieto)
        //{
        //    //if (transform.GetComponentInParent<PlayerController>()!=null)//por si cuando estas enganchado saltas puede dar error
        //    //    dir = GetComponentInParent<PlayerController>().DevuelveDireccion(out izquierda);
        //    dir = GetComponentInParent<MovGancho>().DevuelveDireccion();

        //    //transform.rotation = transform.parent.transform.rotation;
        //    transform.rotation = rot;
        //}
        if (dir == Vector2.up)
        {

            transform.rotation = new Quaternion(0, 0, 0, 1);//Rotacion del spritegancho
            cadena.transform.localRotation = new Quaternion(0, 0, 0, 0);
            cadena.size = new Vector2(cadena.size.x, -Vector2.Distance(ptoLanzamiento.position, gancho.position));
            //Debug.Log("Disparo Arriba");
            //Debug.Log("Rotación cadena: " + cadena.transform.localRotation);
            // Debug.Log("Rotacion arriba de " + transform.name + " : " + transform.rotation);
            //transform.rotation = new Quaternion(rot.x, rot.y, transform.parent.transform.rotation.z, rot.w);
        }
        else if (dir == Vector2.down)
        {

            transform.rotation = new Quaternion(0, 0, 1, 0);//Rotacion del spritegancho
            cadena.transform.localRotation = new Quaternion(0, 0, 0, 0);
            cadena.size = new Vector2(cadena.size.x, Vector2.Distance(ptoLanzamiento.position, gancho.position));


            // Debug.Log("Rotacion abajo de " + transform.name + " : " + transform.rotation);
            //transform.rotation = new Quaternion(rot.x, rot.y, transform.parent.transform.rotation.z, rot.w);
            //Debug.Log("Disparo Abajo");
            //Debug.Log("Rotación cadena: " + cadena.transform.localRotation);

        }
        else if (dir == Vector2.right)
        {
            if (transform.parent.transform.rotation.y == 0)
            {
                //Debug.Log("Rotacion derecha de " + transform.name + " : " + transform.rotation);
                transform.rotation = new Quaternion(0.7f, 0.7f, 0, 0);//rotacion del sprite gancho
                cadena.transform.localRotation = new Quaternion(0, 0, 0.7f, 0.7f);
                cadena.size = new Vector2(cadena.size.x, Vector2.Distance(ptoLanzamiento.position, gancho.position));
                //Debug.Log("Disparo Dcha");
                //Debug.Log("Rotación cadena: " + cadena.transform.localRotation);


            }
            else 
            {
                //Debug.Log("Rotacion izquierda de " + transform.name + " : " + transform.rotation);
                transform.rotation = new Quaternion(-0.7f, 0.7f, 0, 0);//Rotacion del spritegancho
                cadena.transform.localRotation = new Quaternion(0, 0, -0.7f, 0.7f);
                cadena.size = new Vector2(cadena.size.x, -Vector2.Distance(ptoLanzamiento.position, gancho.position));
                //Debug.Log("Disparo Izq");
                //Debug.Log("Rotación cadena: " + cadena.transform.localRotation);

            }
        }
        //Debug.Log("DireccionSprite: " + dir);
        //Debug.Log("Cadena Tamaño" + cadena.size);
       
    }
    public void SetDir(Vector2 direction)
    {
        dir = direction; 
    }
}
