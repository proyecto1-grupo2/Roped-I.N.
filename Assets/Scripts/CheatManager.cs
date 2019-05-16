using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour {
    public GameObject player;

	void Start () {

		if (GameManager.instance.GetImmuneCheat())
        {
            player.GetComponent<Vida>().InmuneCheat();
        }

        if (GameManager.instance.GetNoGravityCheat())
        {

            player.GetComponent<Rigidbody2D>().gravityScale = 0.3f;
        }
    }
	

}
