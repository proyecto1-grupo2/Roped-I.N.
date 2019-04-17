using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("Posicion de "+transform.name +" : "+transform.position);
        Debug.Log("Rotacion de " + transform.name + " : " + transform.rotation);
    }
}
