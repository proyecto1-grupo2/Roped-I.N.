using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    Rigidbody2D Rigidbody;
    public float speed;

    // Use this for initialization
    void Start () {
        Rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Rigidbody.velocity = transform.right * speed;
    }
}
