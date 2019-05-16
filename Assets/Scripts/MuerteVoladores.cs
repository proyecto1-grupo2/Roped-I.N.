using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteVoladores : MonoBehaviour {

    Death death;
    bool dead;
    Transform t;
    Vector2 caida;
    public int speed;

	// Use this for initialization
	void Start () {
        death = GetComponent<Death>();
        dead = death.GetDead();
        t = GetComponent<Transform>();
        caida = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
	  if (dead == true)
        {
            caida = new Vector2(0, speed * Time.deltaTime);
        }
	}
}
