using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * speed es la velocidad del enemigo y range el rango máximo a partir del cual empieza a perseguirte
 * target es el GO al que se persigue
 * generated sirve para saber si ha sido spawneado y spawner es el spawner que le ha spawneado (si es el caso)
 */
public class PursuitTarget : MonoBehaviour {
    Rigidbody2D rb;
    public float speed, range;
    private GameObject target;
    Spawner spawner;
    private bool generated = false;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        //Si no tiene Rigidbody
        if (rb == null)
        {
            Debug.LogError("Falta RigidBody");
        }
        target = GameObject.Find("Player");
        if (target == null)
        {
            Debug.Log("No encuentro al jugador");
        }
    }

    // Update is called once per frame
    //Si el jugador esta dentro del rango, el enemigo le seguirá
    private void Update()
    {
        if (target != null && Vector2.Distance(transform.position, target.transform.position) <= range)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    //Estos métodos se utilizan cuando el objeto ha sido spawneado
    public void DecreaseCount()
    {
        spawner.ResetSpawn();
    }
    public void SetSpawner(Spawner spawn)
    {
        spawner = spawn;
    }
    public bool GetGenerated() { return generated; }
}
