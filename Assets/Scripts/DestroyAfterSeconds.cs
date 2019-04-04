using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {
    public float seconds;

	//destruye el objeto pasado seconds segundos
	void Start () {
        Destroy(gameObject, seconds);
	}
}
