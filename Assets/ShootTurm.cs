using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTurm : MonoBehaviour {

    public GameObject Bullet;
    GameObject Jugador;
    public float timeBetweenShots;
    public Transform pool;
    public float alcance;
    float distancia;

    // Use this for initialization
    void Start () {
        float bulletTime = timeBetweenShots;
        InvokeRepeating("ShootBullet", 2f, timeBetweenShots);
        Jugador = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void ShootBullet () {

        distancia = Vector3.Distance(Jugador.transform.position, transform.position);
        Debug.Log(distancia);
        if (distancia <= alcance)
        {
            Instantiate(Bullet, transform.position, transform.rotation, pool);
        }
        
    }
}
