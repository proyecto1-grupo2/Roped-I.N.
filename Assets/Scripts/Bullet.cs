using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//mueve la bala a velocidad constante
public class Bullet : MonoBehaviour {
    Rigidbody2D Rigidbody;
    public float speed;

    void Start () {
        Rigidbody = GetComponent<Rigidbody2D>();
        Rigidbody.velocity = transform.right * speed;
    }
}
