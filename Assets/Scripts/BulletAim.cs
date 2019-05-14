using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAim : MonoBehaviour {

    public float velBala;
    GameObject Player;

    //Coge la rotacion de la bala segun la posicion del player 
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Vector3 dir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    //mueve la bala
    void Update()
    {
        transform.position += transform.right * velBala * Time.deltaTime;
    }

}
