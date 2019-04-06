using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    PursuitTarget oga;
    //Distingue el caso de la muerte de un enemigo spawneado y hace que los otros objetos mueran
    public void OnDead()
    {
        oga = this.GetComponent<PursuitTarget>();
        if (oga != null && oga.GetGenerated()) //Podria especificarse que el padre es la ogaPool
        {
            //Debug.Log("Soy un oga spawneado");
            oga.DecreaseCount();
            Destroy(this.gameObject);
        }
        else
        {
            //Debug.Log("Soy otra cosa");
            Destroy(this.gameObject);
        }
    }
}