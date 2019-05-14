using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eks : MonoBehaviour
{

    public float bulletSpeed;
    public float spawnTime;
    public Transform BulletSpawner;
    public GameObject bulletPrefab;

    private float counter;

    void Update()
    {
        counter += Time.deltaTime;
        if (counter > spawnTime)
        {

            counter = 0;
        }
    }

}
