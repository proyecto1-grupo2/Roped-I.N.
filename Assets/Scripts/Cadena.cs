using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadena : MonoBehaviour {
    SpriteRenderer cadena;
    public Transform gancho;
    public Transform ptoLanzamiento;
    Vector2 scale;
    Vector2 dir;
    Quaternion rot;
	void Start () {
        cadena = GetComponent<SpriteRenderer>();
        dir = Vector2.right;
        scale = cadena.size;
        rot = transform.rotation;
	}
	

	void Update () {
        
        if (gancho.GetComponent<MovGancho>().daEstado() == HookState.Quieto)
        {
            dir = GetComponentInParent<PlayerController>().DevuelveDireccion();
            cadena.size = scale;
            transform.rotation = rot;
        }
        else { 
            if (dir == Vector2.up)
            {
                transform.rotation = new Quaternion(rot.x, rot.y, 0, rot.w);
                cadena.size = new Vector2(cadena.size.x,- Vector2.Distance(transform.position, gancho.position) );
            }
            else if (dir == Vector2.down)
            {
                transform.rotation = new Quaternion(rot.x, rot.y, 0, rot.w);
                cadena.size = new Vector2(cadena.size.x, Vector2.Distance(transform.position, gancho.position));
            }
            else if (dir == Vector2.right)
            {
                if (transform.parent.transform.rotation.y == 0)
                {
                    cadena.size = new Vector2(cadena.size.x, Vector2.Distance(transform.position, gancho.position));
                }
                else
                {
                    cadena.size = new Vector2(cadena.size.x, -Vector2.Distance(transform.position, gancho.position));
                }
            }


        }

        //cadena.size = new Vector2(Vector2.Distance(transform.position, gancho.position) * dir.x, Vector2.Distance(transform.position, gancho.position * dir.y));

    }
}
