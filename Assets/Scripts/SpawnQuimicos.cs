using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuimicos : MonoBehaviour {

    public GameObject QuimicoQueRespawnea;//el quimico que hay en el nivel
    public GameObject ObjetivoDelQuimico;//telaraña o puerta
    public GameObject prefabQuim;//el prefab del quimico
    int veces = 0;
  

    void Update()
    {

        /*if (QuimicoQueRespawnea == null  && ObjetivoDelQuimico !=null && ObjetivoDelQuimico.activeSelf == true)
        {
            QuimicoQueRespawnea = Instantiate(prefabQuim, transform.position, transform.rotation);
        }*/
        if (QuimicoQueRespawnea == null && veces==0)
        {
            veces = 1;
            Invoke("InstanciaQuimico",3);
        }

    }

    void InstanciaQuimico()
    {
        if ( ObjetivoDelQuimico != null && ObjetivoDelQuimico.activeSelf == true )
        {
            QuimicoQueRespawnea = Instantiate(prefabQuim, transform.position, transform.rotation);
            veces = 0;
        }
    }
}
