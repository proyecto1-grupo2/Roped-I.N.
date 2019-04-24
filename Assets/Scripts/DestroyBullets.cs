using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour {
    public float seconds;

	//destruye el objeto pasado seconds segundos
	void Start () {
        Destroy(gameObject, seconds);
	}

    void OnTriggerEnter2D()
    {
        Destroy(gameObject, 0.05f);
    }
}
