using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour {

    private PlayerController player;
	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
	}

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo")
        {
            player.grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Suelo")
        {
            player.grounded = false;
        }
    }
}
