using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuimicos : MonoBehaviour {

    public GameObject QuimicoQueRespawnea;//el quimico que hay en el nivel
    public GameObject ObjetivoDelQuimico;//telaraña o puerta
    public GameObject prefabQuim;//el prefab del quimico

  

    // Update is called once per frame
    void Update()
    {
        
        if (QuimicoQueRespawnea == null  && ObjetivoDelQuimico !=null && ObjetivoDelQuimico.activeSelf == true)
        {
            QuimicoQueRespawnea = Instantiate(prefabQuim, transform.position, transform.rotation);
        }

    }
}
