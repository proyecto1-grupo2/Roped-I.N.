using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuitTarget : MonoBehaviour
{
    /*
     * speed es la velocidad del enemigo y range el rango máximo a partir del cual empieza a perseguirte
     * target es el GO al que se persigue
     * generated sirve para saber si ha sido spawneado y spawner es el spawner que le ha spawneado (si es el caso)
     */
    Rigidbody2D rb;
    public float speed, range;
    private GameObject target;
    Spawner spawner;
    private bool generated = false;
    bool lookingRight = true;

    void Start()
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

    //Si el jugador esta dentro del rango, el enemigo le seguirá
    private void Update()
    {
        CheckPosition();
        if (lookingRight)
            transform.localRotation = new Quaternion(transform.rotation.x, 180, transform.rotation.z, transform.rotation.w);
        else
            transform.localRotation = new Quaternion(transform.rotation.x, 0, transform.rotation.z, transform.rotation.w);

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
        generated = true;
    }
    public bool GetGenerated() {
        return generated; }

    private void CheckPosition()
    {
        if (transform.position.x < target.transform.position.x)
            lookingRight = true;
        else if (transform.position.x >= target.transform.position.x)
            lookingRight = false;
    }

    public void Deactivate()
    {
        this.enabled = false;
    }
}