using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Script para que la camara siga al jugador
public class FollowTarget : MonoBehaviour
{

    public Transform target;//target=player
    Vector3 posicion;
    void Start()
    {
        posicion = new Vector3(0, 0, -5);//una posicion por si no encuentra al player
    }
    //mueve la camara hacia el jugador
    void LateUpdate()
    {
        if (target != null)
        {           
            posicion = new Vector3(target.position.x, target.position.y, -5);
            transform.position = posicion;
        }
    }
}