using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJuego : MonoBehaviour {

    public bool pausado;
    UIManager UImanager;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown (KeyCode.P))
        {
            pausado = !pausado;
        }

        if (pausado)
        {
            Time.timeScale = 0;
            UImanager.ModifyMenu(true);
        }
        else if (!pausado)
        {
            Time.timeScale = 1;
            UImanager.ModifyMenu(false);
        }
	}
}
