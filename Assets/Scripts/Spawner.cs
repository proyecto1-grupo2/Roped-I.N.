using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
     * cont lleva la cuenta de enemigos spawneados y spawnLimit es el numero máximo de estos enemigos
     * spawnRate es el tiempo que hay entre spawn de enemigos y range es la distancia máxima que tiene que haber entre el jugador y el spawner para que se active
     * pool es el objeto vacío donde se crean los enemigos, target es la referencia del jugador
     * oga es el prefab que instanciamos
     * isSpawning indica si el spawner está spawneando enemigos (para controlar cuando spawnearlos)
     */
    int cont;
    public int spawnLimit;
    public float spawnRate, range;
    GameObject pool, target;
    public PursuitTarget oga;
    bool isSpawning;

    void Start()
    {
        pool = GameObject.Find("ogaPool");
        if (pool == null)
        {
            //Debug.Log("No hay pool para los ogas");
        }
        target = GameObject.Find("Player");
        if (target == null)
        {
            //Debug.Log("No encuentro al jugador");
        }
        cont = 0;
        isSpawning = false;
    }

    /*
     * Solo se spawnean enemigos si el jugador está dentro del rango
     * Cuando se alcanza el límite de spawns, se detiene
     */
    private void Update()
    {
        if (cont >= spawnLimit)
        {
            CancelInvoke("instanciaOga");
            isSpawning = false;
        }

        if (!isSpawning && Vector2.Distance(transform.parent.position, target.transform.position) < range)
        {
            InvokeRepeating("instanciaOga", 0, spawnRate);
            isSpawning = true;
        }
        //Debug.Log(cont);
    }

    //Instancia un oga y lo relaciona con este spawner
    private void instanciaOga()
    {
        if (transform.parent.GetComponent<ImpQuimicos>().daEstado() != EnemyState.Congelado) {
            PursuitTarget newOga = Instantiate(oga, transform.position, transform.rotation, pool.transform);
            newOga.SetSpawner(this);
            cont++;
        }
        
    }

    //Disminuye el contador y reinicia el spawn
    public void ResetSpawn()
    {
        cont--;
        isSpawning = true;
    }

}