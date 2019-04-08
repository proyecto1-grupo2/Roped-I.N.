using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadena : MonoBehaviour {
    public float speed;
    Vector3 temp;
    Transform cadena;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void CrecerCadena()
    {
        temp = transform.localScale;

        temp.y += Time.deltaTime;

        transform.localScale = temp;
    }
}
