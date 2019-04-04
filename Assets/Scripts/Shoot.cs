using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject Bullet;
    public float bulletPerSecond;
    public Transform pool;
    public int dir;
    Vector2 dirBullet;
   
    //llamada a invokerepating e inversion de balas/seg a tiempo entre disparos
    void Start () {
        float bulletTime = 3 / bulletPerSecond;
        InvokeRepeating("ShootBullet", 3f, bulletTime);
    }


    void ShootBullet()
    {
        if (dir == 0)
        {
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, 0);
        }
        else if (dir == 1)
        {
            transform.rotation = new Quaternion(90, 90, transform.rotation.z, 0);
        }

        else if (dir == 2)
        {
            transform.rotation = new Quaternion(transform.rotation.x, -180, transform.rotation.z, 0);
        }
        else if (dir == 3)
        {
            transform.rotation = new Quaternion(45, -45, transform.rotation.z, 0);
        }
        Instantiate(Bullet, transform.position, transform.rotation , pool);    
    }
}
