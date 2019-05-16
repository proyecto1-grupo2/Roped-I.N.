using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destruye las telarañas
public class Telaraña : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.CompareTag("QuimicoFuego"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            MovGancho mov = other.GetComponentInParent<MovGancho>();
            if (mov != null) mov.cambiaEstado(HookState.Vuelta);
            
        }

       

    }
    
}
