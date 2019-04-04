using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot2 : MonoBehaviour {
    public GameObject Bullet;
    public float bulletPerSecond;
    public Transform pool;
    public int dir;
    Vector2 dirBullet;

    //llamada a invokerepating e inversion de balas/seg a tiempo entre disparos
    void Start()
    {
        float bulletTime = 3 / bulletPerSecond;
        InvokeRepeating("ShootBullet1", 1.5f, bulletTime);
    }


    void ShootBullet1()
    {
        if (dir == 4)
            transform.rotation = new Quaternion(45, -90, transform.rotation.z, 0);
        else if (dir == 5)
            transform.rotation = new Quaternion(45, 90, transform.rotation.z, 0);
        else if (dir == 6)
            transform.rotation = new Quaternion(-90, -45, transform.rotation.z, 0);
        else if (dir == 7)
            transform.rotation = new Quaternion(90, -45, transform.rotation.z, 0);
        Instantiate(Bullet, transform.position, transform.rotation, pool);
    }
}
