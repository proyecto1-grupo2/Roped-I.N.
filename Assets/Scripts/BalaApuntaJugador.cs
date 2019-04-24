using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaApuntaJugador : MonoBehaviour {

    public float velBala;
    GameObject Jugador;

    // Use this for initialization
    void Start()
    {
        Jugador = GameObject.FindWithTag("Player");
        Vector3 dir = Jugador.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * velBala * Time.deltaTime;
    }

}
