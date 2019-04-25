using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;
    Vector3 posicion;
    public int mueveCamara;
    Vector3 pos;

    void Start()
    {
        posicion = new Vector3(0, 0, -5);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if(Input.GetKey("left shift") && Input.GetButton("Arriba"))
            {
                if (transform.position.y < pos.y + mueveCamara)
                {
                    transform.Translate(0, 15 * Time.deltaTime, 0);
                }
            }
            else if (Input.GetKey("left shift") && Input.GetButton("Abajo"))
            {
                if (transform.position.y > pos.y - mueveCamara)
                {
                    transform.Translate(0, -15 * Time.deltaTime, 0);
                }
            }
            /*else if (Input.GetAxisRaw("MueveCamara")==-1)//arriba
            {
                if (transform.position.y < pos.y + mueveCamara)
                {
                    transform.Translate(0, 15 * Time.deltaTime, 0);
                }
            }
            else if (Input.GetAxisRaw("MueveCamara") == 1)//abajo
            {
                if (transform.position.y > pos.y - mueveCamara)
                {
                    transform.Translate(0, -15 * Time.deltaTime, 0);
                }
            }*/
            else
            {

                posicion = new Vector3(target.position.x, target.position.y, -5);
                transform.position = posicion;
                pos = transform.position;
            }
        }
     

    }
}