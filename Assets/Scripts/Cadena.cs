using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadena : MonoBehaviour {
    SpriteRenderer cadena;
    //public SpriteRenderer ganchoSprite;
    public Transform gancho;
    public Transform ptoLanzamiento;
    public RelativeJoint2D joint2d;
    Vector2 scale;
    Vector2 dir;
    Quaternion rot;
    bool izquierda;
	void Start () {
        cadena = GetComponent<SpriteRenderer>();
        dir = Vector2.right;
        scale = cadena.size;
        rot = transform.rotation;
	}


    void Update()
    {
        transform.position = ptoLanzamiento.position;
        if (gancho.GetComponent<MovGancho>().daEstado() == HookState.Quieto)
        {
            dir = GetComponentInParent<PlayerController>().DevuelveDireccion();
            cadena.size = scale;
            //transform.rotation = transform.parent.transform.rotation;
            transform.rotation = rot;
        }
        //cadena.size = new Vector2(cadena.size.x, Vector2.Distance(transform.position, joint2d.connectedBody.transform.position));
        else
        {
            if (dir == Vector2.up)
            {
                //gancho.transform.GetChild(0).rotation = transform.rotation;//Rotacion del spritegancho
                cadena.transform.localRotation = new Quaternion(0, 0, 0, 0);
                cadena.size = new Vector2(cadena.size.x, -Vector2.Distance(transform.position, gancho.position));
                //Debug.Log(transform.rotation);
            }
            else if (dir == Vector2.down)
            {
                //gancho.transform.GetChild(0).rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 180, transform.rotation.w);//rotacion del sprite gancho
                cadena.transform.localRotation = new Quaternion(0, 0, 0, 1);
                transform.rotation = new Quaternion(rot.x, rot.y, transform.parent.transform.rotation.z, rot.w);
                cadena.size = new Vector2(cadena.size.x, Vector2.Distance(transform.position, gancho.position));
                //Debug.Log("gdf"+transform.rotation);

            }
            else if (dir == Vector2.right)
            {
                if (transform.parent.transform.rotation.y == 0 )
                {
                    //gancho.transform.GetChild(0).rotation = new Quaternion(transform.rotation.x, transform.rotation.y, -transform.rotation.z, transform.rotation.w);//rotacion del sprite gancho
                    cadena.transform.localRotation = new Quaternion(0, 0, 0.7f, 0.7f);
                    cadena.size = new Vector2(cadena.size.x, Vector2.Distance(transform.position, gancho.position));
                }
                else
                {
                    //gancho.transform.GetChild(0).rotation = transform.rotation;//Rotacion del spritegancho
                    cadena.transform.localRotation = new Quaternion(0, 0, -0.7f, 0.7f);

                    cadena.size = new Vector2(cadena.size.x, -Vector2.Distance(transform.position, gancho.position));
                }
            }
            

        }



        //cadena.size = new Vector2(Vector2.Distance(transform.position, gancho.position) * dir.x, Vector2.Distance(transform.position, gancho.position * dir.y));

    }
    public Quaternion daRot()
    {
        return transform.rotation;
    }
}
