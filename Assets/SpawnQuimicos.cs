using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnQuimicos : MonoBehaviour {

    public GameObject Quimico;
    public GameObject ObjetivoDelQuimico;
    GameObject Quim;


    // Use this for initialization
    void Start () {
        Quim = Instantiate(Quimico, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, Quim.transform.position) > 5 && ObjetivoDelQuimico.activeSelf == true)
        {
            Quim = Instantiate(Quimico, transform.position, transform.rotation);
        }

    }
}
