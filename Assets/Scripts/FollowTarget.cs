using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{

    public Transform target;
    Vector3 posicion;

    void Start()
    {
        posicion = new Vector3(0, 0, -5);
    }

    void LateUpdate()
    {
        
       
        {
            posicion = new Vector3(target.position.x, target.position.y, -5);
            transform.position = posicion;
        }
        

    }
}